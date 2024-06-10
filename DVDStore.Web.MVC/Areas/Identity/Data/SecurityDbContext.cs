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
**  FileName: SecurityDbContext.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**
**  Description:
**  This file contains the SecurityDbContext class, which extends the IdentityDbContext
**  class provided by ASP.NET Core Identity. This class is used for managing the security
**  and identity-related operations within the DVDStore application. The OnModelCreating
**  method can be used to customize the ASP.NET Identity model and override the defaults
**  if needed. This method is called when the model for a derived context is being created.
**  Overriding this method allows the configuration of the model that was discovered by convention
**  from the entity types exposed in DbSet properties on your derived context.
**
**  The OnModelCreating method is typically used to:
**  - Configure relationships between entities.
**  - Customize the table names and schema.
**  - Configure entity properties and data types.
**  - Apply data annotations and constraints.
**  - Seed initial data.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-05-31      RGARLIT      STARTED DEVELOPMENT
***********************************************************************************/

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DVDStore.Web.MVC.Areas.Identity.Data
{
    public class SecurityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
            : base(options)
        {
        }
    }
}

/*
 *
                            <xeee..
                        ueeeeeu..^"*$e.
                 ur d$$$$$$$$$$$$$$Nu "*Nu
               d$$$ "$$$$$$$$$$$$$$$$$$e."$c
           u$$c   ""   ^"^**$$$$$$$$$$$$$b.^R:
         z$$#"""           `!?$$$$$$$$$$$$$N.^
       .$P                   ~!R$$$$$$$$$$$$$b
      x$F                 **$b. '"R).$$$$$$$$$$
     J^"                           #$$$$$$$$$$$$.
    z$e                      ..      "**$$$$$$$$$
   :$P           .        .$$$$$b.    ..  "  #$$$$
   $$            L          ^*$$$$b   "      4$$$$L
  4$$            ^u    .e$$$$e."*$$$N.       @$$$$$
  $$E            d$$$$$$$$$$$$$$L "$$$$$  mu $$$$$$F
  $$&            $$$$$$$$$$$$$$$$N   "#* * ?$$$$$$$N
  $$F            '$$$$$$$$$$$$$$$$$bec...z$ $$$$$$$$
 '$$F             `$$$$$$$$$$$$$$$$$$$$$$$$ '$$$$E"$
  $$                  ^""""""`       ^"*$$$& 9$$$$N
  k  u$                                  "$$. "$$P r
  4$$$$L                                   "$. eeeR
   $$$$$k                                   '$e. .@
   3$$$$$b                                   '$$$$
    $$$$$$                                    3$$"
     $$$$$  dc                                4$F
      RF** <$$                                J"
       #bue$$$LJ$$$Nc.                        "
        ^$$$$$$$$$$$$$r
          `"*$$$$$$$$$
  $. .$ $~$ $~$ ~$~  $  $    $ $ $~$ $. .$ $~$  $  ~$~
  $$ $$ $ $ $ $  $  $.$ $    $$  $ $ $$ $$ $.$ $.$  $
  $`$'$ $ $ $~k  $  $~$ $    $$  $ $ $`$'$ $ $ $~$  $
  $ $ $ $o$ $ $  $  $ $ $oo  $ $ $o$ $ $ $ $o$ $ $  $

*/