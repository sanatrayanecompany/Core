using System;

namespace Core.Model
{
    public class MeetingInfoInsert
    {
        public long MeetingId { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool ShowMember { get; set; }
    }
}
