using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class Branch
    {
        public long Id { get; set; }
        public int CityId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

    }
    public class BranchView
    {
        public long Id { get; set; }
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

    }

    public class BranchPair
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
