using ShareMe.WebApplication.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ShareMe.WebApplication.Models.Grid
{
    [DataContract]
    public class PostGridModel : GridModel<PostApiModel>
    {
        [DataMember(Name = "category")]
        public Guid? CategoryId { get; set; } = null;

        [DataMember(Name = "tags")]
        public IList<Guid> TagIds { get; set; } = null;
    }
}
