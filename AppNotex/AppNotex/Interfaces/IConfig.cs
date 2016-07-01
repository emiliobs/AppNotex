using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLite.Net.Interop;

namespace AppNotex.Interfaces
{
    public interface IConfig
    {
         string DirectoryDB { get;  }

        ISQLitePlatform Platform { get; }
    }                                                    
}
