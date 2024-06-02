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
**  FileName: RoleRepository.cs (DVDStore Application)
**  Version: 0.1
**  Author: Ronald Garlit
**
**  Description: This file contains the RoleRepository class for the DVDStore web application.
**
**  The RoleRepository class handles data access for user roles.
**
**  Change History
**
**  WHEN			WHO        WHAT
**---------------------------------------------------------------------------------
**  2024-05-08		RGARLIT     STARTED DEVELOPMENT
***********************************************************************************/

using DVDStore.Web.MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace DVDStore.Web.MVC.Areas.Security.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SecurityDbContext _context;

        public RoleRepository(SecurityDbContext context)
        {
            _context = context;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}