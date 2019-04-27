using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class ContactInsert
    {
        public long? UserId { get; set; }

        [Required(ErrorMessage = "کد کاربری مخاطب را وارد کنید")]
        public long? ContactUserId { get; set; }
    }

    public class ContactConfirm
    {
        [Required(ErrorMessage = "کد کاربری را وارد کنید")]
        public long? Id { get; set; }

        [Required(ErrorMessage = "وضعیت تائید را وارد کنید")]
        public bool? IsConfirm { get; set; }
    }

    public class ContactSearch
    {
        public long UserId { get; set; }

        public string Username { get; set; }
    }

    public class ContactView
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public bool Add { get; set; }
    }
}