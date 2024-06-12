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
**  FileName: SecurityUnitOfWork.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the SecurityUnitOfWork class for the DVDStore web application.
**
**  The SecurityUnitOfWork class provides a unit of work pattern for security-related data access.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-08		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

namespace DVDStore.Web.MVC.Areas.Security.Repositories
{
    public class SecurityUnitOfWork : ISecurityUnitOfWork
    {
        public IUserRepository User { get; }
        public IRoleRepository Role { get; }

        public SecurityUnitOfWork(IUserRepository user, IRoleRepository role)
        {
            User = user;
            Role = role;
        }
    }
}

/*
              _,.-"T
        _.--{~    :l
      c"     `.    :I
      |  .-"~-.\    l     .--.
      | Y_r--. Y) ___I ,-"(~\ Y
      |[__L__/ j"~=__]~_~\." _/
   ___|  \.__.r--<~__.T T/ "~/
  '--cl___/\ ( () ).,_L_]}--{
     `--'   `-^--^\ /___"(~\ Y
                   "~7/ \ " `/
                    // //]--[
                   /> oX |: L
                  //  /  `| o\
                 //. /    I  [
                / \]/     l: |
               Y.//       `|_I
               I_Z         L :]
              /".-7        [n]l
             Y / /         I //
             |] /         /]"/
             L:/         //./
            [_7      _  // /
              _  ,-="_"^K_/  -Row
             [ ][.-~" ~"-.]           Imperial AT-ST (2)
     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

 */