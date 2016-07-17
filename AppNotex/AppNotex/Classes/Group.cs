using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNotex.Classes
{
   public class Group
    {
        public int GroupId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }


        public override string ToString()
        {
            return Description;
        }

    }
}
