﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Core.Models
{
    public class BaseModel<T>
    {
        public T Id { get; set; }
    }
}
