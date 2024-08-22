using DoctorList.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.BL.Interface
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorDto>> GetAllDoctors();
    }
}
