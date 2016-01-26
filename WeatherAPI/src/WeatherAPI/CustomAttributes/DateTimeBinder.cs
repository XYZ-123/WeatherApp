using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.CustomAttributes
{
    using System.Globalization;

    using Microsoft.AspNet.Mvc.ModelBinding;
    public class DateTimeBinder: IModelBinder
    {
        public async Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(DateTime))
            {
                return ModelBindingResult.NoResult;
            }

            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName,value);

            DateTime result;

            if (DateTime.TryParseExact(
                value.FirstValue,
                "dd-MM-yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result))
                return ModelBindingResult.Success(bindingContext.FieldName, result);

            return ModelBindingResult.NoResult;
        }
    }
}
