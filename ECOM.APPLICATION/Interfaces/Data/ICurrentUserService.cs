using System;
using System.Collections.Generic;
using System.Text;

namespace ECOM.APPLICATION.Interfaces.Data
{
    public interface ICurrentUserService
    {
        int Id { get; }
        string Username { get; }
    }
}
