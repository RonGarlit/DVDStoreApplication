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
//  FileName: PropertyMappingValue.cs (Common.PropertyMapping)
//  Version: 0.1
//  Author: Ronald Garlit
//
//  Description:
//
//  PropertyMappingValue Class.
//
//  Change History
//
//  WHEN			WHO        WHAT
//-----------------------------------------------------------------------------
//  2022-06-27		RGARLIT     STARTED DEVELOPMENT
//****************************************************************************/

namespace DVDStore.Web.MVC.Common.PropertyMapping.BaseMappingCode
{
    public class PropertyMappingValue
    {
        #region Public Constructors

        public PropertyMappingValue(IEnumerable<string> destinationProperties,
            bool revert = false)
        {
            DestinationProperties = destinationProperties
                ?? throw new ArgumentNullException(nameof(destinationProperties));
            Revert = revert;
        }

        #endregion Public Constructors

        #region Public Properties

        public IEnumerable<string> DestinationProperties { get; private set; }
        public bool Revert { get; private set; }

        #endregion Public Properties
    }
}