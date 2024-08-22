using DoctorList.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.DL.Interfaces
{
    public interface  IDoctorRepository
    {
        Task<List<Article>> GetAllArticles();
        Task<IEnumerable<Article>> GetAllSponsorshipArticles();
        Task<List<Doctor>> GetAllDoctors();
        Task<IEnumerable<Doctor>> GetAllActiveAndPayedDoctors();
        Task<Languages> GetAllLanguages();
    }
}
