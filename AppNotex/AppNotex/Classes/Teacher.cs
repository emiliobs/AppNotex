using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNotex.Classes
{
   public class Teacher
    {
        public int GroupId { get; set; }
        public string Description { get; set; }

        public float Note { get; set; }
        public User teacher { get; set; }

        public override string ToString()
        {
            return Description;
        }

    }
}
