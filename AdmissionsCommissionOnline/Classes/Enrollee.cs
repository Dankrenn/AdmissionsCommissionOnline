using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmissionsCommissionOnline.Classes
{
    public class Enrollee
    {
        public string FIO { get; set; }
        public string FIOParent { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public int SNILS { get; set; }
        public int Seria { get; set; }
        public int Number { get; set; }
        public string EducationalInstitution { get; set; }
        public string Document { get; set; }
        public double Point { get; set; }

    }
}
