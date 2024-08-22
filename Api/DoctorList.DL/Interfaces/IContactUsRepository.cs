using DoctorList.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.DL.Interfaces
{
    public interface IContactUsRepository
    {
        bool SaveContactUsData(ContactUs contactUs);
    }
}
