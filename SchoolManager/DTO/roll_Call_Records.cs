using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.DTO
{
    public class Roll_Call_Records
    {
        public int id_Record { get; set; }
        public int id_Student { get; set; }
        public int id_Class { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public string notes { get; set; }

        public string name_Student { get; set; }
    }
}
