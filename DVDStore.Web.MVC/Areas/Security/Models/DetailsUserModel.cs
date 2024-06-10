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
**  FileName: DetailsUserModel.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the DetailsUserModel class for the DVDStore web application.
**  
**  The DetailsUserModel class is a view model used for displaying detailed user information.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-08		RGARLIT     STARTED DEVELOPMENT 
***********************************************************************************/

using Microsoft.AspNetCore.Mvc.Rendering;
using DVDStore.Web.MVC.Areas.Identity.Data;

namespace DVDStore.Web.MVC.Areas.Security.Models
{
    public class DetailsUserModel
    {
        public ApplicationUser? User { get; set; }

        public IList<SelectListItem>? Roles { get; set; }
    }
}
/*
                       .-.
                      |_:_|
                     /(_Y_)\
                    ( \/M\/ )
 '.               _.'-/'-'\-'._
   ':           _/.--'[[[[]'--.\_
     ':        /_'  : |::"| :  '.\
       ':     //   ./ |oUU| \.'  :\
         ':  _:'..' \_|___|_/ :   :|
           ':.  .'  |_[___]_|  :.':\
            [::\ |  :  | |  :   ; : \
             '-'   \/'.| |.' \  .;.' |
             |\_    \  '-'   :       |
             |  \    \ .:    :   |   |
             |   \    | '.   :    \  |
             /       \   :. .;       |
            /     |   |  :__/     :  \\
           |  |   |    \:   | \   |   ||
          /    \  : :  |:   /  |__|   /|
      snd |     : : :_/_|  /'._\  '--|_\
          /___.-/_|-'   \  \
                         '-'


*/