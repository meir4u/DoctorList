using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.DL.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public List<Phone> Phones { get; set; }
        public bool IsActive { get; set; }
    }
}
