using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.DL.Entities
{
    public class Reviews
    {
        public int ProfessionalismRate { get; set; }
        public int AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public int WaitingTimeRate { get; set; }
        public int ServiceRate { get; set; }
    }
}
