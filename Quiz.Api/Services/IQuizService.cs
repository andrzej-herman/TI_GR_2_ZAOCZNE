using Quiz.Data;

namespace Quiz.Api.Services
{
    public interface IQuizService
    {
        Task<QuestionDto> GetQuestion(int category);
        Task<CheckAnswerDto> GetCheckAnswerAndGetNextCategory(Guid answerId, int category);
    }
}
