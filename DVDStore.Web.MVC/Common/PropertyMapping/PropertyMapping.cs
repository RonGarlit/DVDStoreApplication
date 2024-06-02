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
**  FileName: PropertyMapping.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the PropertyMapping class for the DVDStore web application.
**
**  The PropertyMapping class is used to map properties for sorting and filtering.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-04-06		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode;

namespace DVDStore.Web.MVC.Common.PropertyMapping
{
    public class PropertyMapping<TSource, TDestination> : IPropertyMapping
    {
        #region Public Constructors

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            MappingDictionary = mappingDictionary ??
                throw new ArgumentNullException(nameof(mappingDictionary));
        }

        #endregion Public Constructors

        #region Public Properties

        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }

        #endregion Public Properties
    }
}