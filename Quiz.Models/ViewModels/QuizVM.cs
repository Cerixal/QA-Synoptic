using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.ViewModels
{
    public class QuizVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Title")]
        public string? Title { get; set; }

        [DisplayName("Questions")]
        public string? Questions { get; set; }

        public List<QuizVM> Quizzes { get; set; }
    }
}
