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
**  FileName: PropertyMappingValue.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the PropertyMappingValue class for the DVDStore web application.
**
**  The PropertyMappingValue class holds the mapping configuration for properties.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-04-06		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

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