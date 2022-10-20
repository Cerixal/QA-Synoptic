using Quiz.Models.ViewModels;

namespace Quiz.Web.Models
{
    public class QuestionTableVM
    {
        public QuestionVM Question { get; set; }
        public List<AnswerVM> Answers { get; set; }
    }
}
