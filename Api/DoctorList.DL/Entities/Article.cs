using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.DL.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public DateTime DateModified { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int Views { get; set; }
        public List<string> SpecializationsIds { get; set; }
        public List<Sponsorship> Sponsorships { get; set; }
    }
}
