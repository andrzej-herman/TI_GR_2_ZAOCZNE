using Microsoft.Data.SqlClient;
using Quiz.Data;

namespace Quiz.Api.Services
{
    public class QuizService : IQuizService
    {
        private const string connectionString = "Server=.\\HERMANLOCAL;Database=CqrsTp2;Integrated Security=True;TrustServerCertificate=True";
        SqlConnection? sqlConnection;
        Random? random;

        public QuizService()
        {
            sqlConnection = new SqlConnection(connectionString);
            random = new Random();
        }


        public async Task<QuestionDto> GetQuestion(int category)
        {
            try
            {
                var questions = new List<QuestionDto>();
                await sqlConnection!.OpenAsync();
                var query = "select * from Questions where QuestionCategory = @category";
                var cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@category", category);
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var guid = reader.GetGuid(0);
                    var cat = reader.GetInt32(1);
                    var content = reader.GetString(2);
                    var question = new QuestionDto
                    {
                        Id = guid,
                        Category = cat,
                        Content = content,
                        Answers = []
                    };
                    questions.Add(question);
                }

                await reader.CloseAsync();

                if (!questions.Any())
                    return new QuestionDto { Error = "Nieprawidłowa kategoria pytania" };

                var randomNumber = random!.Next(0, questions.Count);
                var selectedQuestion = questions[randomNumber];

                query = "select AnswerId, AnswerContent from Answers where QuestionId = @questionId";
                cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@questionId", selectedQuestion.Id);
                var answerReader = await cmd.ExecuteReaderAsync();
                while (answerReader.Read())
                {
                    var guid = answerReader.GetGuid(0);
                    var content = answerReader.GetString(1);
                    var answer = new AnswerDto
                    {
                        Id = guid,
                        Content = content
                    };
                    selectedQuestion.Answers.Add(answer);
                }

                await sqlConnection!.CloseAsync();
                return selectedQuestion;
            }
            catch (Exception)
            {
                return new QuestionDto { Error = "Błąd podczas pobierania pytania" };
            }       
        }


        public async Task<CheckAnswerDto> GetCheckAnswerAndGetNextCategory(Guid answerId, int category)
        {
            try
            {
                List<int> categories = [100, 200, 300, 400, 500, 750, 1000]; 
                var isCorrect = false;
                await sqlConnection!.OpenAsync();
                var query = "select AnswerIsCorrect from Answers where AnswerId = @answerId";
                var cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@answerId", answerId);
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                    isCorrect = reader.GetBoolean(0);          

                await reader.CloseAsync();

                var index = categories.IndexOf(category);
                var nextCategory = index != 6 ? categories[index + 1] : 0;
                return new CheckAnswerDto { IsCorrect = isCorrect, NextCategory = nextCategory };
                
            }
            catch (Exception ex)
            {
                return new CheckAnswerDto { Error = ex.Message };
            }
        }

       
    }
}
