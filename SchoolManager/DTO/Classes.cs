using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.DTO
{
    public class Classes
    {
        public int id_Class { get; set; }
        public string name_Class { get; set; }

        // Assigned teacher id (nullable). Null means unassigned.
        public int? AssignedTeacherId { get; set; }

    }
}
