using AppNotex.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Interop;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(AppNotex.WinPhone.Clases.Config))]


namespace AppNotex.WinPhone.Clases
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
                    directoryDB = ApplicationData.Current.LocalFolder.Path;
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
                    platform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
                }

                return platform;
            }
        }
    }
}
