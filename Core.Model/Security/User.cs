using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public int BranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public bool Sex { get; set; }
        public string Mobile { get; set; }
        public string TelegramUsername { get; set; }
        public string DeviceId { get; set; }
        public string PushId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime ModifyDate { get; set; }

    }


    public class tbUser
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordEncript { get; set; }
        public string Password { get; set; }
        public int BranchId { get; set; }
        public string Avatar { get; set; }
        public string Title { get; set; }
        public int UserTitleStatusId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class UserApplicantView
    {

        public int Id { get; set; }
        public int BranchId { get; set; }
    }

        public class UserView
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public string Avatar { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string NationalCode { get; set; }
        public DateTime? CreateDate { get; set; }
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserRoleModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
    public class UserRoleView
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string UserFullName { get; set; }
    }

    public class UserRole
    {
        public long UserId { get; set; }
        public List<int> LstRoleId { get; set; }
    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
            
    }
}
