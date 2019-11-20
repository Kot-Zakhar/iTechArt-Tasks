using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CustomJsonFormatter.Api.Models;
using CustomJsonFormatter.Api.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace CustomJsonFormatter.Api.Formatters
{
    public class CustomTextJsonOutputFormatter : SystemTextJsonOutputFormatter
    {
        private class OutputObject
        {
            public object Data { get; set; }

            [JsonPropertyName("_links")]
            public IDictionary<string, string> Links { get; set; }
            public OutputObject(object obj)
            {
                Data = obj;
                Links = new Dictionary<string, string>();
            }
        }

        private readonly LinkService _linkService;

        public CustomTextJsonOutputFormatter(LinkService linkService, JsonSerializerOptions jsonSerializerOptions) : base(jsonSerializerOptions)
        {
            _linkService = linkService;
        }

        public CustomTextJsonOutputFormatter(LinkService linkService) : this(linkService, new JsonSerializerOptions())
        {}

        public override Task WriteAsync(OutputFormatterWriteContext context)
        {
            var data = new OutputObject(context.Object)
            {
                Links = _linkService.GetLinks(context.Object)
            };
            data.Links.Add("self", context.HttpContext.Request.Host + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
            return base.WriteAsync(new OutputFormatterWriteContext(context.HttpContext, context.WriterFactory, typeof(OutputObject), (object)data));
        }
    }
}
