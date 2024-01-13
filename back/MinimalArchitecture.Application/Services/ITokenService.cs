using MinimalArchitecture.Application.Dto.Authentication;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Generate token of user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string GenerateToken(User user);
        /// <summary>
        /// Validate integrity of token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>code: TKEXPIRED when the token is expired and TKNOTVALID when was a error in the validation</returns>
        Result ValidateToken(string token);

        /// <summary>
        /// Get user info from token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Result<UserInfo> GetUser(string token);
    }
}
