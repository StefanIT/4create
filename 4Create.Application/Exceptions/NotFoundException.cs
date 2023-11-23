﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Create.Application.Exceptions
{
	public class NotFoundException : Exception
	{
        public NotFoundException(string message) : base(message)
		{ }

        public NotFoundException(string name, object key) : base($"Entity '{name}' with key '{key}' was not found.")
        { }
    }
}
