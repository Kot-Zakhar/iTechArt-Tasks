using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CustomJsonFormatter.Api.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CustomJsonFormatter.Api.Formatters
{
    public class CustomTextJsonOutputFormatter : SystemTextJsonOutputFormatter
    {
        private class OutputObject
        {
            public object Data { get; set; }
            public string Self { get; set; }
            public OutputObject(object obj)
            {
                Data = obj;
            }
            public OutputObject(OutputObject outputObject) : this(outputObject.Data)
            {
                Self = outputObject.Self;
            }
        }

        private class OutputArticle : OutputObject
        {
            public OutputArticle(Article article) : base(article) { }
            public OutputArticle(OutputObject outputObject) : base(outputObject) { }

            [JsonPropertyName("get-author")]
            public string AuthorLink { get; set; }
        }

        public CustomTextJsonOutputFormatter(JsonSerializerOptions jsonSerializerOptions)
            : base(jsonSerializerOptions ?? new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase } )
        {}

        public CustomTextJsonOutputFormatter() : base(new JsonSerializerOptions())
        {}

        public override Task WriteAsync(OutputFormatterWriteContext context)
        {
            OutputObject data = new OutputObject(context.Object);
            data.Self = context.HttpContext.Request.Host + context.HttpContext.Request.Path;
            if (context.Object is Article)
            {
                var article = context.Object as Article;
                var outputArticle = new OutputArticle(data);
                // I don't know where to get path to Profile-api, so i hardcoded it
                outputArticle.AuthorLink = context.HttpContext.Request.Host + "/api/profile/" + article.AuthorId.ToString();
                data = outputArticle; 
            }
            return base.WriteAsync(new OutputFormatterWriteContext(context.HttpContext, context.WriterFactory, typeof(OutputObject), data));
        }
    }

    public static class CustomTextJsonOutputFormatterExtension
    {
        public static IMvcBuilder AddCustomJsonOutputFormatter(this IServiceCollection service, JsonSerializerOptions jsonOptions = null)
        {
            return service.AddMvc(options => {
                options.ReturnHttpNotAcceptable = true;

                var customJsonFormatter = new CustomTextJsonOutputFormatter(jsonOptions);
                customJsonFormatter.SupportedMediaTypes.Clear();
                customJsonFormatter.SupportedMediaTypes.Add("application/json+custom");
                options.OutputFormatters.Add(customJsonFormatter);
            });
        }
    }
}
