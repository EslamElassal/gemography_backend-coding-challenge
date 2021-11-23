using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessLayer.Token
{
 
    public interface ITokenService
    {
        string CreateToken( UserModel user);

    }
}
