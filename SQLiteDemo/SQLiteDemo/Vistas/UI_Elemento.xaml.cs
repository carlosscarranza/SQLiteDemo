using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using SQLiteDemo.Data;
using SQLiteDemo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLiteDemo.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UI_Elemento : ContentPage
	{
        public int IdSeleccionado;
        private SQLiteAsyncConnection _conn;
        IEnumerable<Product> _resultadoDelete;
        IEnumerable<Product> _resultadoUpdate;
        public UI_Elemento (int id)
		{
			InitializeComponent ();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
            IdSeleccionado = id;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mensaje.Text = "Se afectará al ID ["+IdSeleccionado+"]";
        }
        private void btn_actualizar(object sender,EventArgs e)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NorthWind.db");
            var db = new SQLiteConnection(databasePath);
            _resultadoUpdate =Update(db,Producto.Text,Precio.Text,Unidades.Text,IdSeleccionado);
            Navigation.PushAsync(new UI_ConsultaRegistro());
        }
        private void btn_eliminar(object sender, EventArgs e)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NorthWind.db");
            var db = new SQLiteConnection(databasePath);
            _resultadoDelete = Delete(db,IdSeleccionado);
            Navigation.PushAsync(new UI_ConsultaRegistro());
        }

        public static IEnumerable<Product> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Product>("DELETE FROM Product where Id = ?", id);
        }
        public static IEnumerable<Product> Update(SQLiteConnection db, string productName,string unitPrice,string unitsInStock,int id)
        {
            return db.Query<Product>("UPDATE Product SET ProductName = ?, UnitPrice = ?, UnitsInStock = ? where Id = ?", productName, unitPrice, unitsInStock, id);
        }
    }
}