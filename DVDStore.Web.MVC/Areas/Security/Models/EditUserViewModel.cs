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

/*
                                   __ 1      1 __        _.xxxxxx.
                   [xxxxxxxxxxxxxx|##|xxxxxxxx|##|xxxxxxXXXXXXXXX|
   ____            [XXXXXXXXXXXXXXXXXXXXX/.\||||||XXXXXXXXXXXXXXX|
  |::: `-------.-.__[=========---___/::::|::::::|::::||X O^XXXXXX|
  |::::::::::::|2|%%%%%%%%%%%%\::::::::::|::::::|::::||X /
  |::::,-------|_|~~~~~~~~~~~~~`---=====-------------':||  5
   ~~~~                       |===|:::::|::::::::|::====:\O
                              |===|:::::|:.----.:|:||::||:|
                              |=3=|::4::`'::::::`':||__||:|
                              |===|:::::::/  ))\:::`----':/
  BlasTech Industries'        `===|::::::|  // //~`######b
  DL-44 Heavy Blaster Pistol      `--------=====/  ######B
                                                   `######b
  1 .......... Sight Adjustments                    #######b
  2 ............... Stun Setting                    #######B
  3 ........... Air Cooling Vent                    `#######b
  4 ................. Power Pack                     #######P
  5 ... Power Pack Release Lever             LS      `#####B

 */