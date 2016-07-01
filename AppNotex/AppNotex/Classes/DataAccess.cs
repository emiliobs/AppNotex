using AppNotex.Interfaces;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppNotex.Classes
{
    public class DataAccess : IDisposable
    {

        private SQLiteConnection conecction;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            conecction = new SQLiteConnection(config.Platform, System.IO.Path.Combine(config.DirectoryDB, "Notes.db3"));
            conecction.CreateTable<User>();
        }

        //el t es para utilizar los metodo con todas las posibles tablas acrear:
        //Metodos CRUD:
        public void Insert<T>(T model)
        {
            conecction.Insert(model);
        }

        public void Update<T>(T model)
        {
            conecction.Update(model);
        }

        public void Delete<T>(T model)
        {
            conecction.Delete(model);
        }

        public T Find<T>( int id) where T : class
        {
            return conecction.Table<T>().FirstOrDefault(model => model.GetHashCode() == id);
        }

        public T First<T>() where T : class
        {
            return conecction.Table<T>().FirstOrDefault();
        }

        public List<T> GetList<T>() where T : class
        {
            return conecction.Table<T>().ToList();
        }

        public void Dispose()
        {
            conecction.Dispose();
        }
    }
}
