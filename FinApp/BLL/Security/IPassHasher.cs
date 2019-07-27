using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Security
{
    public interface IPassHasher
    {
        string HashPassword(string password);
        bool CheckPassWithHash(string password, string hash);
    }
}
