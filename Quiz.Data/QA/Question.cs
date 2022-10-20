using System;
using System.Collections.Generic;

namespace Quiz.Data.QA
{
    public partial class Question
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string? Question1 { get; set; }
        public string? Answer { get; set; }
    }
}
