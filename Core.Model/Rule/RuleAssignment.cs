using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class RuleAssignment
    {
        public long Id { get; set; }
        public int RuleId { get; set; }
        public int TestGroupId { get; set; }
        public long UserId { get; set; }
    }
     public class RuleAssignmentView
    {
        public long Id { get; set; }
        public string RuleTitle { get; set; }
        public string TestGroupTitle { get; set; }
        public string UserFullName { get; set; }
    }
}
