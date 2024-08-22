using DoctorList.BL.DTOs;
using DoctorList.BL.Interface;
using DoctorList.DL.Entities;
using DoctorList.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.BL.Service
{
    public class DoctorService: IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctors()
        {
            var doctors = new List<DoctorDto>();

            var allDoctors = await _doctorRepository.GetAllActiveAndPayedDoctors();
            var allArticles = await _doctorRepository.GetAllSponsorshipArticles();
            var allLanguages = await _doctorRepository.GetAllLanguages();

            var allOrderedDoctors = orderDoctors(allDoctors);
            foreach (var doctor in allOrderedDoctors)
            {
                var doctorDto = new DoctorDto()
                {
                    FullName = doctor.FullName,
                    Phone = getDoctorPhone(doctor.Phones),
                    Rating = doctor.Reviews?.AverageRating ?? 0,
                    SupportedLanguages = getSupportedLanguages(doctor.LanguageIds, allLanguages),
                    HasArticles = hasDoctorArticles(doctor.Id, allArticles),                    
                };

               doctors.Add(doctorDto);
            }

            return doctors;
        }

        private bool hasDoctorArticles(int doctorId, IEnumerable<Article> allArticles)
        {
            if (allArticles == null)
            {
                throw new ArgumentNullException(nameof(allArticles), "The articles collection cannot be null.");
            }

            var hasArticles = allArticles.Any(a => a.Sponsorships != null && a.Sponsorships.Any(x => x.SponsorshipId == doctorId));
            return hasArticles;
            
        }

        private string getDoctorPhone(List<Phone> phones)
        {
            var phone = phones.FirstOrDefault()?.Number ?? string.Empty;
            if (string.IsNullOrEmpty(phone) == false) 
            {
                phone = normalizePhoneNumber(phone);
            }
            return phone;
        }

        private string normalizePhoneNumber(string phone)
        {
            phone = phone.Replace("-", "");

            if (phone.Length == 9)
            {
                // 2 digit prefix
                phone = phone.Insert(2, "-");
            }
            else if (phone.Length == 10)
            {
                // 3 digit prefix
                phone = phone.Insert(3, "-");
            }
            else
            {
                phone = string.Empty;
            }

            return phone;
        }

        private IEnumerable<string> getSupportedLanguages(List<string> languageIds, Languages allLanguages)
        {
            if (languageIds == null || allLanguages == null)
            {
                throw new ArgumentNullException("LanguageIds and AllLanguages cannot be null.");
            }

            var languageIdSet = new HashSet<string>(languageIds);

            var supportedLang = allLanguages.Language
                                .Where(lang => languageIdSet.Contains(lang.Key))
                                .Select(lang => lang.Value);

            return supportedLang;
        }

        private IEnumerable<Doctor> orderDoctors(IEnumerable<Doctor> doctors)
        {
            var ordered = doctors.OrderByDescending(x => x.Reviews.AverageRating)
                .ThenByDescending(d => d.Reviews.TotalRatings)
                .ThenBy(d => d.PromotionLevel).ToList();

            return ordered;
        }
    }
}
