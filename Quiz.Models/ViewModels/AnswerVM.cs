using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Models.ViewModels;

namespace Quiz.Models.ViewModels
{
    public class AnswerVM
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("QuestionID")]
        public int? QuestionId { get; set; }

        [DisplayName("Answer")]
        public string? Answer1 { get; set; }

        [DisplayName("Character")]
        public char Character { get; set; }
        public List<AnswerVM> Answers { get; set; }

    }
}
