using ECOM.DOMAIN.Entities;
using ECOM.SHARED.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.APPLICATION.Interfaces
{
    public interface IMenuService
    {
        Task<MethodResult<List<DM_MenuGroup>>> GetMenuGroupAsync();
    }
}
