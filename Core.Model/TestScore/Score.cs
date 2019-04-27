using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class ScoreOneTest
    {
        public int ApplicantTestRegistrationId { get; set; }
        public int UserApplicantId { get; set; }
        public string TestTitle { get; set; }
        public string Address { get; set; }
        public string BranchName { get; set; }
        public int BranceId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public int TestItemId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime HoldingDate { get; set; }
        public string PersianRegistrationDate { get; set; }
        public string PersianHoldingDate { get; set; }
        public string HoldingTime { get; set; }
        public decimal Cost { get; set; }
        public float? TotalScore { get; set; }
        public float? WritingScore { get; set; }
        public float? ReadingScore { get; set; }
        public float? SpeakingScore { get; set; }
        public float? ListeningScore { get; set; }
        public long UserId { get; set; }
    }


    public class ScoreOneApplicant
    {
        public int Id { get; set; }
        public string TestTitle { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime HoldingDate { get; set; }
        public string PersianRegistrationDate { get; set; }
        public string PersianHoldingDate { get; set; }
        public float? TotalScore { get; set; }
        public float? WritingScore { get; set; }
        public float? ReadingScore { get; set; }
        public float? SpeakingScore { get; set; }
        public float? ListeningScore { get; set; }
    }

}
