using CALKU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALKU.Services
{
    public interface IRegisterRepository
    {
        Task<UserInfo> Register(string userName, string password, string email);
    }
}
