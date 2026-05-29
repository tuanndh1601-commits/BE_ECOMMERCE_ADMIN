using ECOM.APPLICATION.Interfaces;
using ECOM.DOMAIN.Entities;
using ECOM.INFRASTRUCTURE.Persistence.Dapper;
using ECOM.INFRASTRUCTURE.Persistence.EntityFramework;
using ECOM.SHARED.Interface;
using ECOM.SHARED.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.INFRASTRUCTURE.Services
{
    public class MenuService : IMenuService, IScopeDependency
    {
        private readonly BaseDbContext _context;

        public MenuService(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<MethodResult<List<DM_MenuGroup>>> GetMenuGroupAsync()
        {
            return await _context.DM_MenuGroup.Where(x => !(x.IsDeleted ?? false)).ToListAsync();
        }
    }
}
