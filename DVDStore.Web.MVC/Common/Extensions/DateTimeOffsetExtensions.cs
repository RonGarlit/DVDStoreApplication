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
**  FileName: DateTimeOffsetExtensions.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the DateTimeOffsetExtensions class for the DVDStore web application.
**
**  The DateTimeOffsetExtensions class provides extension methods for working with DateTimeOffset objects.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-03-31		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

namespace DVDStore.Web.MVC.Common.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        #region Public Methods

        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset,
            DateTimeOffset? dateOfDeath)
        {
            var dateToCalculateTo = DateTime.UtcNow;

            if (dateOfDeath != null)
            {
                dateToCalculateTo = dateOfDeath.Value.UtcDateTime;
            }

            var age = dateToCalculateTo.Year - dateTimeOffset.Year;

            if (dateToCalculateTo < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }

        #endregion Public Methods
    }
}