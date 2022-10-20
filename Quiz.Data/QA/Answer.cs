using System;
using System.Collections.Generic;

namespace Quiz.Data.QA
{
    public partial class Answer
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string? Answer1 { get; set; }
        public char? Character { get; set; }
    }
}
