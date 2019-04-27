using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class Rule
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int Code { get; set; }
        public byte[] FileData { get; set; }
        public string FileDataBase64 { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}
