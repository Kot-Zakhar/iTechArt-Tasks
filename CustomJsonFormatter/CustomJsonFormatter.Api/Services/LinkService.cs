using CustomJsonFormatter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomJsonFormatter.Api.Services
{
    public class LinkService
    {
        private string _host;
        public LinkService(string host)
        {
            _host = host;
        }
        public IDictionary<string, string> GetLinks(Article article)
        {
            return new Dictionary<string, string>
            {
                ["get-author"] = _host + "/api/profile/" + article.AuthorId,
                ["self"] = _host + "/api/article/" + article.Id
            };
        }
        public IDictionary<string, string> GetLinks(Profile profile)
        {
            return new Dictionary<string, string>
            {
                ["self"] = _host + "/api/profile/" + profile.Id
            };
        }

        public IDictionary<string, string> GetLinks(object obj)
        {
            if (obj is Profile profile)
                return GetLinks(profile);
            if (obj is Article article)
                return GetLinks(article);
            return new Dictionary<string, string>();
        }
    }
}
