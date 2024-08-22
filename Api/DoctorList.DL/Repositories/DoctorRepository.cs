using DoctorList.DL.Entities;
using DoctorList.DL.Interfaces;
using DoctorList.DL.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DoctorList.DL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IOptions<DoctorFileDataSettings> _settings;
        private readonly IMemoryCache _cache;
        private JsonSerializerOptions _jsonSerializerOptions;
        private const string _languagesCacheKey = "LanguagesCache";
        private const int _languagesCacheExpiration = 1;

        public DoctorRepository(IOptions<DoctorFileDataSettings> settings, IMemoryCache cache)
        {
            this._settings = settings;
            this._cache = cache;

            this._jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<Doctor>> GetAllActiveAndPayedDoctors()
        {
            var allDoctors = await GetAllDoctors();
            var filtered = allDoctors.Where(x => x.IsActive && x.PromotionLevel <= 5).ToList();
            return filtered;
        }

        public async Task<List<Article>> GetAllArticles()
        {
            try
            {
                var filePath = getAbsolutePath(Path.Combine(_settings.Value.DirectoryPath, _settings.Value.ArticlesFile));
                var jsonString = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<List<Article>>(jsonString, _jsonSerializerOptions);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Doctor>> GetAllDoctors()
        {
            try
            {
                var filePath = getAbsolutePath(Path.Combine(_settings.Value.DirectoryPath, _settings.Value.DoctorsFile));
                var jsonString = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<List<Doctor>>(jsonString, _jsonSerializerOptions);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Languages> GetAllLanguages()
        {
            if (!_cache.TryGetValue(_languagesCacheKey, out Languages languages))
            {
                try
                {
                    var filePath = getAbsolutePath(Path.Combine(_settings.Value.DirectoryPath, _settings.Value.LanguagesFile));
                    var jsonString = await File.ReadAllTextAsync(filePath);
                    languages = JsonSerializer.Deserialize<Languages>(jsonString, _jsonSerializerOptions);

                    // Cache the languages data
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(_languagesCacheExpiration)); 

                    _cache.Set(_languagesCacheKey, languages, cacheOptions);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return languages;
        }

        public async Task<IEnumerable<Article>> GetAllSponsorshipArticles()
        {
            try
            {
                var allArticles = await GetAllArticles();
                var filtered = allArticles.Where(x => x.Sponsorships != null && x.Sponsorships.FirstOrDefault()?.SponsorshipId > 0).ToList();
                return filtered;
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        private string getAbsolutePath(string relativePath)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(basePath, relativePath);
        }
    }
}
