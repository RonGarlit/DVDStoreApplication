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
**  FileName: EditUserViewModel.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the EditUserViewModel class for the DVDStore web application.
**
**  The EditUserViewModel class is a view model used for editing user information.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-08		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DVDStore.Web.MVC.Areas.Security.Models
{
    public class EditUserViewModel
    {
        public ApplicationUser? User { get; set; }

        public IList<SelectListItem>? Roles { get; set; }
    }
}