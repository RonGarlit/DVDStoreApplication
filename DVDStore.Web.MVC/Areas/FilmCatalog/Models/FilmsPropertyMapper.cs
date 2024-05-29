using System;
using System.Collections.Generic;
using DVDStore.Web.MVC.Common.Exceptions;
using DVDStore.Web.MVC.Common.PropertyMapping;
using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;
using DVDStore.DAL;

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
