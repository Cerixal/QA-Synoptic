using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class QuestionVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Quiz")]
        public int QuizID { get; set; }

        [DisplayName("Qustion")]
        public string? Question1 { get; set; }

        [DisplayName("Answer")]
        public string? Answer { get; set; }

        public List<QuestionVM> Qustions { get; set; }
    }
}
