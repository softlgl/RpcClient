﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcClient.Test.Model
{
    public class School
    {
        public string Name { get; set; }
        public List<Class> Classes { get; set; }
    }
}
