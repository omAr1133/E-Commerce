﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BadRequestException(List<string> Errors)
        : Exception("ValidationFailed")
    {
        public List<string> Errors { get; } = Errors;
    }
}
