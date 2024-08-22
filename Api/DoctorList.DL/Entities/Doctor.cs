using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.DL.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public int PromotionLevel { get; set; }
        public List<Clinic> Clinics { get; set; }
        public Reviews Reviews { get; set; }
        public List<string> MainSpecialtiesIds { get; set; }
        public List<Phone> Phones { get; set; }
        public List<string> SubSpecialtiesIds { get; set; }
        public List<string> LanguageIds { get; set; }
        public string FullName { get; set; }
        public List<string> AreaIds { get; set; }
        public bool IsActive { get; set; }
    }
}
