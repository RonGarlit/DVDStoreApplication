/**********************************************************************************
**
**  DVDStore Application v1.0
**
**  Copyright 2024
**  Developed by:
**     Ronald Garlit.
**
**  This software was created for educational purposes.
**
**  Use is subject to license terms.
***********************************************************************************
**
**  FileName: ArrayModelBinder.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the ArrayModelBinder class for the DVDStore web application.
**
**  The ArrayModelBinder class provides a custom model binder for binding arrays from query parameters.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-03-31		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Reflection;

namespace DVDStore.Web.MVC.Common.Extensions
{
    public class ArrayModelBinder : IModelBinder
    {
        private static readonly string[] separator = [","];

        #region Public Methods

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Our binder works only on enumerable types
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            // Get the inputted value through the value provider
            var value = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName).ToString();

            // If that value is null or whitespace, we return null
            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // The value isn't null or whitespace,
            // and the type of the model is enumerable.
            // Get the enumerable's type, and a converter
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            var converter = TypeDescriptor.GetConverter(elementType);

            // Convert each item in the value list to the enumerable type
            var values = value.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => converter.ConvertFromString(x.Trim()))
                .ToArray();

            // Create an array of that type, and set it as the Model value
            var typedValues = Array.CreateInstance(elementType, values.Length);
            values.CopyTo(typedValues, 0);
            bindingContext.Model = typedValues;

            // return a successful result, passing in the Model
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }

        #endregion Public Methods
    }
}