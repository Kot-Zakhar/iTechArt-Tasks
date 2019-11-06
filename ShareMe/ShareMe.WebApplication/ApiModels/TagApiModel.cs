using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.ApiModels
{
    public class TagApiModel : ApiModel
    {
        public string Name { get; set; }

        public TagApiModel() { }

        public TagApiModel(Tag tag) : base(tag)
        {
            Name = tag.Name;        
        }
    }
}
