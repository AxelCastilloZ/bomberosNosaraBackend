﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace Services.Auth
{
    public interface IAuthService
    {
        Task<TokenResponse> LoginAsync(UserCredential credentials);
    }
}
