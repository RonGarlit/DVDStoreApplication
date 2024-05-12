//*****************************************************************************
//  AppStarterTemplate v1.0
//
//  Copyright 2022
//  Developed by:
//     Ronald Garlit.
//
//
//  Use is subject to license terms.
//*****************************************************************************
//
//  FileName: PropertyMapping.cs (Common.PropertyMapping)
//  Version: 0.1
//  Author: Ronald Garlit
//
//  Description:
//
//  PropertyMapping Class.
//
//  Change History
//
//  WHEN			WHO        WHAT
//-----------------------------------------------------------------------------
//  2022-06-27		RGARLIT     STARTED DEVELOPMENT
//****************************************************************************/
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