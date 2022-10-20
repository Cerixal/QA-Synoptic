using AutoMapper;
using Quiz.Data.QA;
using Quiz.Models.ViewModels;

namespace QA_Synoptic.Maps.Properties
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<User, UserVM>().ReverseMap();
            CreateMap<Question, QuestionVM>().ReverseMap();
            CreateMap<Quiz.Data.QA.Quiz, QuizVM>().ReverseMap();
            CreateMap<Answer, AnswerVM>().ReverseMap();

        }
    }
}
