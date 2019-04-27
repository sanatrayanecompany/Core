using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class TestGroup
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int? ParentId { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class TestGroupView
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public string TitleAbbreviation { get; set; }
        public string ParentTitle { get; set; }
        public string UserFullName { get; set; }
        public string Description { get; set; }
    }
    public class TestGroupWithSubSetAndItems
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TestGroupWithSubSetAndItems> subTestGroupList { get; set; }
        public List<TestItemView> subTestItemList { get; set; }


    }


    public class TestGroupPair
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int BranchId { get; set; }

    }
}
