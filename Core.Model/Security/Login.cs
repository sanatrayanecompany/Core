using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class Login
    {
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}