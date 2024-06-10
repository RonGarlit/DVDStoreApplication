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
**  FileName: ApplicationUser.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description:
**  This file contains the ApplicationUser class which extends the IdentityUser class
**  provided by ASP.NET Core Identity. This class includes additional profile data
**  for application users by adding properties for FirstName and LastName.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/

using Microsoft.AspNetCore.Identity;

namespace DVDStore.Web.MVC.Areas.Identity.Data;

/// <summary>
/// ApplicationUser class extends the IdentityUser class provided by ASP.NET Core Identity.
/// </summary>
public class ApplicationUser : IdentityUser
{
    //==========================================================================================
    // Add profile data for application users by adding properties to the ApplicationUser class
    //==========================================================================================
    /// <summary>
    /// FirstName property
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// LastName property
    /// </summary>
    public string? LastName { get; set; }
}

/*
 *
     ,----.    ,-.   ,----.,------. ,-.   ,-.,-. ,-.
    / ,-,_/  ,'  |  / /"P /`-, ,-','  |  / //  |/ /
   / / __  ,' ,| | / ,---'  / / ,' ,| | / // J P /
  / '-' /,' ,--. |/ /      / /,' ,--. |/ // /|  /
  `----''--'   `-'`'.--""""--.--'   `-'`' `' `-'
  nnnnnnnnnnnnnnnn,'.n*""""*N.`.#######################
  NNNNNNNNNNNNNNN/ J',n*""*n.`L \##### ### ### ### ####
                : J J___/\___L L :#####################
  nnnnnnnnnnnnnn{ [{ `.    ,' }] }## ### ### ### ### ##
  NNNNNNNNNNNNNN: T T /,'`.\ T J :#####################
                 \ L,`*n,,n*',J /
  nnnnnnnnnnnnnnnn`. *n,,,,n* ,'nnnnnnnnnnnnnnnnnnnnnnn
  NNNNNNNNNNNNNNNNNN`-..__..-'NNNNNNNNNNNNNNNNNNNNNNNNN
  ,-.    ,-.  ,-. ,----. ,----.,-. ,----.   ,-.
  |  `.  \  `.|  \\  .--`\ \"L \\ \\ .-._\  |  `. o!0
  | |. `. \ \ ` L \\  __\ \ .  < \ \\ \  __ | |. `.
  | .--. `.\ \`-'\ \\ `---.\ \L `.\ \\ `-` \| .--. `.
  `-'   `--``'    `-'`----' `-'`-' `' `----'`-'   `--'

*/