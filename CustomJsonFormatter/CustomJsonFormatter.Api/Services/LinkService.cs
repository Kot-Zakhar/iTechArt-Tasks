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
            var links = new Dictionary<string, string>();
            links["self"] = _host + "/api/article/" + article.Id.ToString();
            links["get-author"] = _host + "/api/profile/" + article.AuthorId.ToString();
            return links;
        }
        public IDictionary<string, string> GetLinks(Profile profile)
        {
            var links = new Dictionary<string, string>();
            links["self"] = _host + "/api/profile/" + profile.Id;
            return links;
        }

        public IDictionary<string, string> GetLinks(object obj)
        {
            if (obj is Profile)
                return GetLinks(obj as Profile);
            if (obj is Article)
                return GetLinks(obj as Article);
            return new Dictionary<string, string>();
        }
    }
}
