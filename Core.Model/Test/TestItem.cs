using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class TestItem
    {
        public long Id { get; set; }
        public int TestGroupId { get; set; }
        public int BranchId { get; set; }
        public long UserId { get; set; }
        public int Code { get; set; }
        public DateTime HoldingDate { get; set; }
        public DateTime HoldingTime { get; set; }
        public int Capacity { get; set; }
        public string EventPlace { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public double PercentEmployer { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public DateTime DeadlineCancelling { get; set; }
        public string Description { get; set; }


    }


    public class TestItemView
    {
        public long Id { get; set; }
        public int TestGroupId { get; set; }
        public string TestGroupTitle { get; set; }
        public int BranchId { get; set; }
        public long UserId { get; set; }
        public string BranchName { get; set; }
        public int Code { get; set; }
        public DateTime HoldingDate { get; set; }
        public string PersianHoldingDate { get; set; }
        public string HoldingTime { get; set; }
        public int Capacity { get; set; }
        public bool Active { get; set; }
        public string EventPlace { get; set; }
        public string Title { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string TestVenue { get; set; }
        public string Address { get; set; }
        public string FullTitle { get; set; }
        public decimal Cost { get; set; }
        public string CostSeparate { get; set; }
        public double PercentEmployer { get; set; }
        public string PercentEmployerStr { get; set; }
        public string PersianRegistrationDeadline { get; set; }
        public string PersianDeadlineCancelling { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public DateTime DeadlineCancelling { get; set; }
        public string TestItemStatusTitle { get; set; }
        public string Description { get; set; }

    }

    public class TestGroupWithItemsView
    {
        public long Id { get; set; }
        public int? MasterTestGroupId { get; set; }
        public string MasterTestGroupTitle { get; set; }
        public int TestGroupId { get; set; }
        public string TestGroupTitle { get; set; }
        public int BranchId { get; set; }
        public long UserId { get; set; }
        public string BranchName { get; set; }
        public int Code { get; set; }
        public DateTime HoldingDate { get; set; }
        public string PersianHoldingDate { get; set; }
        public string HoldingTime { get; set; }
        public int Capacity { get; set; }
        public string EventPlace { get; set; }
        public string Title { get; set; }
        public decimal Cost { get; set; }
        public double PercentEmployer { get; set; }
        public string PersianRegistrationDeadline { get; set; }
        public string PersianDeadlineCancelling { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public DateTime DeadlineCancelling { get; set; }
        public string TestItemStatusTitle { get; set; }
        public string Description { get; set; }

    }

}
