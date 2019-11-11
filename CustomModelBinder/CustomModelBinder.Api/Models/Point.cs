using CustomModelBinder.Api.Binders;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBinder.Api.Models
{
    [ModelBinder(BinderType = typeof(PointEntityBinder))]
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }
}
