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
**  FileName: FilmlistPropertyMapper.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description:
**  This file contains the FilmlistPropertyMapper class which is responsible for
**  mapping the properties of the Filmlist model to their corresponding property
**  mapping values. It provides methods to retrieve the property mapping dictionary
**  and to validate the existence of valid mappings for the specified fields.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.DAL;
using DVDStore.Web.MVC.Common.Exceptions;
using DVDStore.Web.MVC.Common.PropertyMapping;
using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;

namespace DVDStore.Web.MVC.Areas.Store.Models
{
    /// <summary>
    /// FilmlistPropertyMapper class is responsible for mapping the properties of the Filmlist model
    /// </summary>
    public class FilmlistPropertyMapper
    {
        #region Private Fields

        private readonly Dictionary<string, PropertyMappingValue> _filmlistPropertyMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                { "Fid", new PropertyMappingValue(new List<string> { "Fid" }) },
                { "Title", new PropertyMappingValue(new List<string> { "Title" }) },
                { "Description", new PropertyMappingValue(new List<string> { "Description" }) },
                { "Category", new PropertyMappingValue(new List<string> { "Category" }) },
                { "Price", new PropertyMappingValue(new List<string> { "Price" }) },
                { "Length", new PropertyMappingValue(new List<string> { "Length" }) },
                { "Rating", new PropertyMappingValue(new List<string> { "Rating" }) },
                { "Actors", new PropertyMappingValue(new List<string> { "Actors" }) }
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// FilmlistPropertyMapper constructor
        /// </summary>
        public FilmlistPropertyMapper()
        {
            _propertyMappings.Add(new PropertyMapping<Filmlist, Filmlist>(_filmlistPropertyMapping));
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// GetPropertyMapping method returns the property mapping dictionary for the specified source and destination types
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        /// <exception cref="PropertyMappingException"></exception>
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
        /// ValidMappingExistsFor method validates the existence of valid mappings for the specified fields
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
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

/*
           #      #
          ##     #
         ###    ###
        #####   ###
         ####__####
        #####   ~~~)
        (c      * *_
         |         _)
         |      _  |
        _|_________|
       /    \\/ \__/\
      /      \\  \~\ \
     /   |  | \\  \~\ \
    /    |  |  \\  \~\ \
   /     |  |   \\  \~\ \            Dilbert's Boss
  /      |  |    \\  \~\ \
 {       |__|     \\  \~\ \
 |_______(__)____  \\  \~\ \
     |_____o_____|  \\  "" |
     |           |__||_____|
     |           |         |
     |           |    |    |   teb

*/