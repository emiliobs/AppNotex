using AppNotex.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Interop;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AppNotex.Droid.Clases.Config))]
 
namespace AppNotex.Droid.Clases
{
    public class Config : IConfig
    {
        private string directoryDB;
        private ISQLitePlatform platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {

                    directoryDB = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                }

                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }

                return platform;
            }
        }
    }
}
