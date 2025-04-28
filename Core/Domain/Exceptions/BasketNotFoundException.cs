﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException(string key)
        :NotFoundException($"Basket With Key {key} not Found !!")
    {

    }
}
