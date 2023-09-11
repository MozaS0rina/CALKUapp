using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CALKU.Models;

namespace CALKU.Services
{
    public interface ILoginRepository
    {
        Task<UserInfo> Login(string userName, string password);
    }
}
