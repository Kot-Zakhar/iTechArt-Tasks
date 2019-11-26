using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Models.ApiModels
{
    [Serializable]
    public abstract class ApiModel
    {
        public Guid Id { get; set; }

        public ApiModel() { }
        public ApiModel(Entity dbEntity)
        {
            Id = dbEntity.Id;
        }

    }
}
