using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomModelBinder.Api.Models;
using System.Linq;

namespace CustomModelBinder.Api.Binders
{
    public class PointEntityBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            string modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult =
                bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName,
                valueProviderResult);


            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }


            List<string> stringCoord = value.Split(",").Where(strCoord => int.TryParse(strCoord, out int result)).ToList();

            if (stringCoord.Count() != 3)
            {
                bindingContext.ModelState.TryAddModelError(
                    modelName,
                    "Location should be provided as row of 3 comma-separeted values");
                return Task.CompletedTask;
            }

            List<int> Coordinates = stringCoord.Select(strCoord => int.Parse(strCoord)).ToList();

            var model = new Point()
            {
                x = Coordinates[0],
                y = Coordinates[1],
                z = Coordinates[2]
            };
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
