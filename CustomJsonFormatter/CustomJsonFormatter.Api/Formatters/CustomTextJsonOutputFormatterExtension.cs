using CustomJsonFormatter.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace CustomJsonFormatter.Api.Formatters
{
    public static class CustomTextJsonOutputFormatterExtension
    {
        public static IMvcBuilder AddCustomJsonOutputFormatter(this IMvcBuilder binder, string hostAddress,
            JsonSerializerOptions jsonOptions = null)
        {
            return binder.AddMvcOptions(options =>
            {
                options.ReturnHttpNotAcceptable = true;

                var customJsonFormatter = new CustomTextJsonOutputFormatter(new LinkService(hostAddress), jsonOptions);
                customJsonFormatter.SupportedMediaTypes.Clear();
                customJsonFormatter.SupportedMediaTypes.Add("application/json+custom");
                options.OutputFormatters.Add(customJsonFormatter);
            });
        }
    }
}
