﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class Response <T>
    {
        public Response()
        {
            
        }
        public Response(string message)
        {
            Succeded = false;
            Message = message;
        }
        public Response(T data, string message = null) 
        {
            Succeded = true;
            Message = message;
            Data = data;
        }
        public bool Succeded { get; set; }
        public string Message { get; set; }
        public List<string> Errores { get; set; }
        public T Data { get; set; }

    }
}
