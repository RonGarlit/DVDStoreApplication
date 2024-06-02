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
**  FileName: IQueryableExtensions.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the IQueryableExtensions class for the DVDStore web application.
**
**  The IQueryableExtensions class provides extension methods for querying data.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-03-31		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;
using System.Linq.Dynamic.Core;
using System.Text;

namespace DVDStore.Web.MVC.Common.Extensions
{
    public static class IQueryableExtensions
    {
        #region Public Methods

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
               Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(mappingDictionary);

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByStringBuilder = new StringBuilder();

            // the orderBy string is separated by ",", so we split it.
            var orderByAfterSplit = orderBy.Split(',');

            // apply each orderby clause
            foreach (var orderByClause in orderByAfterSplit)
            {
                // trim the orderBy clause, as it might contain leading
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var.
                var trimmedOrderByClause = orderByClause.Trim();

                // if the sort option ends with " desc", we order
                // descending, otherwise ascending
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                // remove " asc" or " desc" from the orderBy clause, so we
                // get the property name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(' ');
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                // find the matching property
                if (!mappingDictionary.TryGetValue(propertyName, out PropertyMappingValue? value))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                // get the PropertyMappingValue
                PropertyMappingValue propertyMappingValue = value ?? throw new ArgumentNullException(nameof(value), "Value cannot be null.");

                // Run through the property names
                // so the orderby clauses are applied in the correct order
                foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
                {
                    // revert sort order if necessary
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    // Append with comma if needed
                    if (orderByStringBuilder.Length > 0)
                    {
                        orderByStringBuilder.Append(", ");
                    }

                    // Append property name with sort direction
                    orderByStringBuilder.Append(destinationProperty + (orderDescending ? " descending" : " ascending"));
                }
            }

            return source.OrderBy(orderByStringBuilder.ToString());
        }

        #endregion Public Methods
    }
}