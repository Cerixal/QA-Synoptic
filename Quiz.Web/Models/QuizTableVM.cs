using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Models.ViewModels;

namespace Quiz.Web.Models
{
    public class QuizTableVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Title")]
        public string? Title { get; set; }

        public List<QuizVM> Quizzes { get; set; }
    }
}
