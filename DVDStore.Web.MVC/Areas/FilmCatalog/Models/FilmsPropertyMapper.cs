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
**  FileName: FilmsPropertyMapper.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the FilmsPropertyMapper class for the DVDStore web application.
**
**  The FilmsPropertyMapper class provides property mapping for sorting and filtering film data.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-28		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.DAL;
using DVDStore.Web.MVC.Common.Exceptions;
using DVDStore.Web.MVC.Common.PropertyMapping;
using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;

namespace DVDStore.Web.MVC.Areas.FilmCatalog.Common
{
    public class FilmsPropertyMapper
    {
        private readonly Dictionary<string, PropertyMappingValue> _filmsPropertyMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "Filmid", new PropertyMappingValue(new List<string> { "Filmid" }) },
                { "Title", new PropertyMappingValue(new List<string> { "Title" }) },
                { "Releaseyear", new PropertyMappingValue(new List<string> { "Releaseyear" }) },
                { "Rentalrate", new PropertyMappingValue(new List<string> { "Rentalrate" }) },
                { "Rating", new PropertyMappingValue(new List<string> { "Rating" }) }
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public FilmsPropertyMapper()
        {
            _propertyMappings.Add(new PropertyMapping<Film, Film>(_filmsPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new PropertyMappingException($"Cannot find exact property mapping instance for <{typeof(TSource)}, {typeof(TDestination)}>");
        }

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
    }
}