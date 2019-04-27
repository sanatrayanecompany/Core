using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class CancellationRule
    {
        public int Id { get; set; }
        public int TestGroupId { get; set; }
        public int CancellationTime { get; set; }
        public int PercentDecrement { get; set; }
        public string StrPercentDecrement { get; set; }
        public int UserId { get; set; }
    }


    public class CalcPayableVal
    {
        public decimal Cost { get; set; }
        public string CostSeparate { get; set; }
        public decimal PayableVal { get; set; }
        public string PayableValSeparate { get; set; }

        public string PercentDecrement { get; set; }
        public int? RemainTime { get; set; }
    }

    public class CancellationRuleView
    {
        public int Id { get; set; }
        public string TestGroupTitle { get; set; }
        public int CancellationTime { get; set; }
        public string PercentDecrement { get; set; }

    }

    public class CanceledTest
    {
        public int Id { get; set; }
        public int ApplicantTestRegistrationId { get; set; }
        public long UserApplicantId { get; set; }
        public decimal RepaymentVal { get; set; }
        public int PercentDecrement { get; set; }
        public DateTime RegisteredDate { get; set; }
        public string PersianRegisteredDate { get; set; }
        public string BankCardNumber { get; set; }
        public string Description { get; set; }

    }

    public class CanceledRegisterationTest
    {
        public int ApplicantTestRegistrationId { get; set; }
        public int UserApplicantId { get; set; }
        public int UserId { get; set; }
        public string TestTitle { get; set; }
        public string BranchName { get; set; }
        public int BranchId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public int TestItemId { get; set; }
        public decimal RepaymentVal { get; set; }
        public string RepaymentValSeparate { get; set; }
        public string PercentDecrement { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime CanceledDate { get; set; }
        public string PersianRegistrationDate { get; set; }
        public string PersianCanceledDate { get; set; }
        public string BankCardNumber { get; set; }
        public string DipositTitle { get; set; }
        public string DepositTrakingCode { get; set; }
    }
}
