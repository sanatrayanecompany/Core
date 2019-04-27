using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class Menu
    {
       public long Id { get; set; }
       public int? ParentId{ get; set; }
       public short TypeId{ get; set; }
       public string  Name{ get; set; }
       public string Title{ get; set; }
       public string Parameter{ get; set; }
       public string Icon{ get; set; }
       public short Order{ get; set; }
       public bool IsActive{ get; set; }
    }

    public class MenuView
    {
        public long Id { get; set; }
        public int? ParentId { get; set; }
        public short TypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Parameter { get; set; }
        public string Icon { get; set; }
        public short Order { get; set; }
        public bool IsActive { get; set; }
        public List<MenuView> SubMenu { get; set; }
    }

    //public class MeetingCancel
    //{
    //    [Required(ErrorMessage = "کد را وارد کنید")]
    //    public long Id { get; set; }

    //    [Required(ErrorMessage = "متن کنسل را وارد کنید")]
    //    public string CancelText { get; set; }
    //}

    //public class MeetingDTO
    //{
    //    public long Id { get; set; }

    //    [Required(ErrorMessage = "عنوان را وارد کنید")]
    //    public string Title { get; set; }
    //    public string Address { get; set; }
    //    public string Description { get; set; }

    //    [Required(ErrorMessage = "تاریخ را انتخاب کنید")]
    //    public string Date { get; set; }

    //    [Required(ErrorMessage = "ساعت را انتخاب کنید")]
    //    public string Time { get; set; }

    //    [Required(ErrorMessage = "عرض جغرافیایی را وارد کنید")]
    //    public float Latitude { get; set; }

    //    [Required(ErrorMessage = "طول جغرافیایی را وارد کنید")]
    //    public float Longitude { get; set; }
    //    public bool ShowMember { get; set; }
    //    public List<MeetingMember> Members { get; set; }
    //}

    //public class MeetingView
    //{
    //    public long Id { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Title { get; set; }
    //    public string Address { get; set; }
    //    public string Description { get; set; }
    //    public string DateFa { get; set; }
    //    public string Time { get; set; }
    //    public float Latitude { get; set; }
    //    public float Longitude { get; set; }
    //    public bool ShowMember { get; set; }
    //    public string CancelText { get; set; }
    //    public DateTime DateTime { get; set; }
    //    public List<MeetingMember> Members { get; set; }
    //}
}
