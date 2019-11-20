using CustomJsonFormatter.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace CustomJsonFormatter.Api.Formatters
{
    public static class CustomTextJsonOutputFormatterExtension
    {
        public static IMvcBuilder AddCustomJsonOutputFormatter(this IServiceCollection service, string hostAddress, JsonSerializerOptions jsonOptions = null)
        {
            return service.AddMvc(options => {
                options.ReturnHttpNotAcceptable = true;

                var customJsonFormatter = new CustomTextJsonOutputFormatter(new LinkService(hostAddress), jsonOptions);
                customJsonFormatter.SupportedMediaTypes.Clear();
                customJsonFormatter.SupportedMediaTypes.Add("application/json+custom");
                options.OutputFormatters.Add(customJsonFormatter);
            });
        }
    }
}
