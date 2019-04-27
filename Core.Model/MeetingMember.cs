using System.Collections.Generic;

namespace Core.Model
{
    public class MeetingMemberDTO
    {
        public long MeetingId { get; set; }
        public List<long> UsersId { get; set; }
    }

    public class MeetingMemberInsert
    {
        public long MeetingId { get; set; }
        public long UserId { get; set; }
    }

    public class MeetingMemberConfirm
    {
        public long Id { get; set; }
        public bool IsConfirm { get; set; }
    }

    public class MeetingMember
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool? IsConfirm { get; set; }
    }
}