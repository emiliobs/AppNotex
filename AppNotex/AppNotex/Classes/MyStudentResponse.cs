using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNotex.Classes
{
   public class MyStudentResponse
    {
        public int GroupDetailId { get; set; }
        public int GroupId { get; set; }
        public User Student { get; set; }

        public float Note { get; set; }

        public override string ToString()
        {
            return Student.FullName;
        }

    }
}
