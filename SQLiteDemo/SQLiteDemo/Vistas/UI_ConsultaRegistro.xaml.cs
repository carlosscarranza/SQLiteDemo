using System;
using System.Collections.ObjectModel;
using SQLite;
using SQLiteDemo.Data;
using SQLiteDemo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLiteDemo.Vistas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UI_ConsultaRegistro : ContentPage
	{
        private readonly SQLiteAsyncConnection _conn;
        private ObservableCollection<Product> _tablaRegistro;
        public UI_ConsultaRegistro ()
		{
			InitializeComponent ();
            _conn = DependencyService.Get<ISQLiteDB>().GetConnection();
            NavigationPage.SetHasBackButton(this,false);
        }

        protected override async void OnAppearing()
        {
            var resulRegistros = await _conn.Table<Product>().ToListAsync();
            _tablaRegistro = new ObservableCollection<Product>(resulRegistros);
            ListProductos.ItemsSource = _tablaRegistro;
            base.OnAppearing();
        }

        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Product)e.SelectedItem;
            var item = obj.Id.ToString();
            int id = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new UI_Elemento(id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}