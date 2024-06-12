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
**  FileName: PropertyMappingException.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the PropertyMappingException class for the DVDStore web application.
**
**  The PropertyMappingException class is a custom exception used to handle errors related to property mapping.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-03-31		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

namespace DVDStore.Web.MVC.Common.Exceptions
{
    public class PropertyMappingException : Exception
    {
        public PropertyMappingException()
        {
        }

        public PropertyMappingException(string message)
            : base(message)
        {
        }

        public PropertyMappingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}