using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuRo.Model.DTO
{
    public abstract class Request
    {
        public string  Code { get; set; }
        public string  CodeType { get; set; }
    }
}
