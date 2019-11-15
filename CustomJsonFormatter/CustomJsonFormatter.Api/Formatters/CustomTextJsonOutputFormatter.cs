using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace CustomJsonFormatter.Api.Formatters
{
    public class CustomTextJsonOutputFormatter : SystemTextJsonOutputFormatter
    {
        protected class OutputObject
        {
            public object Data { get; set; }
            public OutputObject(object obj)
            {
                Data = obj;
            }
        }

        public CustomTextJsonOutputFormatter(JsonSerializerOptions jsonSerializerOptions) : base(jsonSerializerOptions ?? new JsonSerializerOptions())
        {}

        public CustomTextJsonOutputFormatter() : base(new JsonSerializerOptions())
        {}

        public override Task WriteAsync(OutputFormatterWriteContext context)
        {
            
            return base.WriteAsync(new OutputFormatterWriteContext(context.HttpContext, context.WriterFactory, typeof(OutputObject), new OutputObject(context.Object)));
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
