namespace KSS.HorseRacing.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RaceViewModel
    {
        [Display(Name = "#")]
        public int RaceId { get; set; }

        [Display(Name = "���� � ����� ����������")]
        public string RaceDateTime { get; set; }

        [Display(Name = "����� ����� � ���� ���")]
        public string RaceNumberInDay { get; set; }

        public IList<ParticipantViewModel> Participants { get; set; }
    }
}