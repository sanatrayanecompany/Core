using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class City
    {
        public long Id { get; set; }
        public int ProvinceId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        
    }

    public class CityView
    {
        public long Id { get; set; }
        public int ProvinceId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string ProvinceName { get; set; }
        
    }
}
