using gemography_backend_coding_challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VDream.Bll.Token
{
 
    public interface ITokenService
    {
        string CreateToken( User user);

    }
}
