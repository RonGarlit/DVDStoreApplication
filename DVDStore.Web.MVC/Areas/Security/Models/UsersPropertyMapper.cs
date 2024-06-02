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
**  FileName: UsersPropertyMapper.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the UsersPropertyMapper class for the DVDStore web application.
**
**  The UsersPropertyMapper class provides property mapping for sorting and filtering user data.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-08		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Common.Exceptions;
using DVDStore.Web.MVC.Common.PropertyMapping;
using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;

namespace DVDStore.Web.MVC.Areas.Security.Models
{
    /// <summary>
    /// The UsersPropertyMapper class is responsible for mapping property names from source types
    /// to destination types, facilitating data transformations especially suited for situations
    /// where property names do not match between data transfer objects and domain models.
    /// </summary>
    public class UsersPropertyMapper

    {
        #region Private Fields

        /// <summary>
        /// Holds the mapping definitions between property names of different models. This dictionary
        /// is case insensitive for property name lookups.
        /// </summary>
        private readonly Dictionary<string, PropertyMappingValue> _usersPropertyMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                // Examples of property mappings:
                { "Id", new PropertyMappingValue(["Id"]) }, // Used collection initializers or expressions (IDE0028) change new PropertyMappingValue(new List<string> { "Id" })  to new PropertyMappingValue(["Id"]
                { "LastName", new PropertyMappingValue(["LastName"]) } // Used collection initializers or expressions (IDE0028) change new PropertyMappingValue(new List<string> { "LastName" })  to new PropertyMappingValue(["LastName"])
            };

        private readonly IList<IPropertyMapping> _propertyMappings = []; // Used collection initializers or expressions (IDE0028) changed new List<IPropertyMapping>() to []

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the UsersPropertyMapper class.
        /// Adds predefined property mappings to the internal list.
        /// </summary>
        public UsersPropertyMapper()
        {
            _propertyMappings.Add(new PropertyMapping<Areas.Identity.Data.ApplicationUser, Areas.Identity.Data.ApplicationUser>(_usersPropertyMapping));
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Retrieves the property mapping dictionary for a specific source and destination type.
        /// </summary>
        /// <typeparam name="TSource">The source type from which to map properties.</typeparam>
        /// <typeparam name="TDestination">The destination type to which properties are mapped.</typeparam>
        /// <returns>A dictionary of property mappings.</returns>
        /// <exception cref="PropertyMappingException">Thrown when no exact mapping is found.</exception>
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new PropertyMappingException($"Cannot find exact property mapping instance for <{typeof(TSource)}, {typeof(TDestination)}>");
        }

        /// <summary>
        /// Validates whether the specified fields in a query string can be mapped correctly between the source and destination types.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TDestination">The destination type.</typeparam>
        /// <param name="fields">A comma-separated list of fields to validate.</param>
        /// <returns>True if all fields can be mapped; otherwise, false.</returns>
        public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var trimmedField = field.Trim();
                int indexOfFirstSpace = trimmedField.IndexOf(' ');
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion Public Methods
    }
}