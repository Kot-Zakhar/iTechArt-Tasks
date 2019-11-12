using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using RateLimit.WebApp.Models;

namespace RateLimit.WebApp.Services
{
    public class ProfileService
    {
        private List<Profile> profiles = new List<Profile>();
        public readonly string FileName;

        public ProfileService(string fileName = "./Data/profiles.json")
        {
            FileName = fileName;
            string serializedProfiles = File.ReadAllText(Path.GetFullPath(FileName), System.Text.Encoding.UTF8);
            profiles.AddRange(JsonSerializer.Deserialize<IList<Profile>>(serializedProfiles));
        }

        public ProfileService(int amount)
        {
            profiles.AddRange(Enumerable.Range(0, amount).Select(index => new Profile()));
        }

        public IQueryable<Profile> GetAll()
        {
            return profiles.AsQueryable();
        }

        public IQueryable<Profile> GetPage(int pageIndex = 0, int profilesPerPage = 10)
        {
            Thread.Sleep(1000);
            return profiles.GetRange(pageIndex * profilesPerPage, profilesPerPage).AsQueryable();
        }
    }
}
