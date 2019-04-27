using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class Message
    {
        public bool Success { get; set; }
        public bool Error { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}

