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
**  FileName: IntentionalException.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the IntentionalException class for the DVDStore web application.
**
**  The IntentionalException class is a custom exception used for testing and demonstration purposes.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-04-06		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

namespace DVDStore.Web.MVC.Common.Exceptions
{
    public class IntentionalException : Exception
    {
        public IntentionalException()
        {
        }

        public IntentionalException(string message)
            : base(message)
        {
        }

        public IntentionalException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}