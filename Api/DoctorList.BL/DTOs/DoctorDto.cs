using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.BL.DTOs
{
    public class DoctorDto
    {
        public string FullName { get; set; }
        public int Rating { get; set; }
        public IEnumerable<string> SupportedLanguages { get; set; }
        public bool HasArticles { get; set; }
        public string? Phone { get; set; }
    }
}
