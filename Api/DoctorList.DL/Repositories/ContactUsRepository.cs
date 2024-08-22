using DoctorList.DL.Entities;
using DoctorList.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.DL.Repositories
{
    public class ContactUsRepository : IContactUsRepository
    {
        private readonly List<ContactUs> _contactUsData = new List<ContactUs>();

        public bool SaveContactUsData(ContactUs contactUs)
        {
            try
            {
                _contactUsData.Add(contactUs);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
            
        }
    }
}
