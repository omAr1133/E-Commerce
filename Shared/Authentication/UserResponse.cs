﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
    public record UserResponse(string Email, string DisplayName, string Token)
    {
    }
}
