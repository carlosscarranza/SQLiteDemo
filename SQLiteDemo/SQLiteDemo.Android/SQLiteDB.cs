using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteDemo.Data;
using SQLiteDemo.Droid;
using SQLiteDemo.Models;
using Xamarin.Forms;
[assembly: Dependency(typeof(SQLiteDB))]
namespace SQLiteDemo.Droid
{
    //Accedemos a la interfaz que se creo en el proyecto compartido
    public class SQLiteDB : ISQLiteDB
    {
        private  SQLite.SQLiteAsyncConnection _database;

        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //Se crea la Base de datos
            var path = Path.Combine(documentsPath, "NorthWind.db");
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<Product>().Wait();

            return _database;
        }
    }
}