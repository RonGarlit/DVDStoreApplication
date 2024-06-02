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
**  FileName: ObjectExtensions.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the ObjectExtensions class for the DVDStore web application.
**  
**  The ObjectExtensions class provides extension methods for shaping data objects.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-03-31		RGARLIT     STARTED DEVELOPMENT 
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace DVDStore.Web.MVC.Common.Extensions
{
    public static class ObjectExtensions
    {
        #region Public Methods

        public static ExpandoObject ShapeData<TSource>(this TSource source,
             string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var dataShapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                // all public properties should be in the ExpandoObject
                var propertyInfos = typeof(TSource)
                        .GetProperties(BindingFlags.IgnoreCase |
                        BindingFlags.Public | BindingFlags.Instance);

                foreach (var propertyInfo in propertyInfos)
                {
                    // get the value of the property on the source object
                    var propertyValue = propertyInfo.GetValue(source);

                    // add the field to the ExpandoObject
                    ((IDictionary<string, object>)dataShapedObject)
                        .Add(propertyInfo.Name, propertyValue);
                }

                return dataShapedObject;
            }

            // the field are separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                // trim each field, as it might contain leading
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var.
                var propertyName = field.Trim();

                // use reflection to get the property on the source object
                // we need to include public and instance, b/c specifying a
                // binding flag overwrites the already-existing binding flags.
                var propertyInfo = typeof(TSource)
                    .GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new Exception($"Property {propertyName} wasn't found " +
                        $"on {typeof(TSource)}");
                }

                // get the value of the property on the source object
                var propertyValue = propertyInfo.GetValue(source);

                // add the field to the ExpandoObject
                ((IDictionary<string, object>)dataShapedObject)
                    .Add(propertyInfo.Name, propertyValue);
            }

            // return the list
            return dataShapedObject;
        }

        #endregion Public Methods
    }
}