using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using SQLite;
using SQLiteDemo.Data;
using SQLiteDemo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;

namespace SQLiteDemo.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UI_Registro : ContentPage
	{
        private SQLiteAsyncConnection _conn;
        public UI_Registro ()
		{
			InitializeComponent ();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
        }

        protected void btn_agregar(object sender, EventArgs e)
        {
            var datosRegistro = new Product { ProductName = Nombre.Text, UnitPrice = Convert.ToDecimal(UnitPrice.Text), UnitsInStock = Convert.ToInt32(UnitsInStock.Text), CategoryId = 1};
            _conn.InsertAsync(datosRegistro);
            LimpiarFormulario();
            Navigation.PushAsync(new UI_ConsultaRegistro());
        }

        void LimpiarFormulario()
        {
            Nombre.Text = "";
            UnitPrice.Text = "";
            UnitsInStock.Text = "";
            //DisplayAlert("App Carlos","Se agregó correctamente","Ok");
        }

        public async Task<int> AddCollectionCosmos(Product product)
        {
            using (CosmosClient cosmosClient = new CosmosClient("https://commovilapp.documents.azure.com:443/", 
                "UytzrzOnysQ5E1BfTaOPph7mw0zMCLBIeDpV7oYRmQWH6C3GuiLtdGTOkwQwJfvAMs79y1xHI5wxJYvVHZUu6A=="))
            {
                //// Read item from container
                var todoItemResponse = await cosmosClient
                    .GetContainer("DatabaseId", "ContainerId")
                    .ReadItemAsync<Product>("ItemId", new PartitionKey("partitionKeyValue"));
            }
            return 0;
        }

    }
}