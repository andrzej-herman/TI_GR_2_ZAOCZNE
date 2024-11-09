using Newtonsoft.Json;
using Quiz.Updater;
using System.Data.SqlClient;

var path = "S:\\ZIMA 2024\\TECHNOLOGIE_INTERNETOWE\\questions.json";
var text = File.ReadAllText(path);
var questions = JsonConvert.DeserializeObject<List<Question>>(text);


var connectionString = "Server=.\\HERMANLOCAL;Database=QuizDb2;Integrated Security=True";
var connection = new SqlConnection(connectionString);
connection.Open();
var count = 1;
foreach (var question in questions!)
{
    var properContent = question.Content!.Replace("'", "\"");
    var query = $"INSERT INTO Questions VALUES ({question.Category}, '{properContent}')";
    var command = new SqlCommand(query, connection);
    var rows = command.ExecuteNonQuery();
    if (rows == 1)
    {
        var questionId = 0;
        query = "SELECT QuestionId FROM Questions ORDER BY QuestionId DESC";
        var idCommand = new SqlCommand(query, connection);
        var idReader = idCommand.ExecuteReader();
        while (idReader.Read())
        {
            questionId = idReader.GetInt32(0);
            break;
        }
        idReader.Close();

        foreach (var answer in question.Answers!)
        {
            properContent = answer.Content!.Replace("'", "\"");
            var correct = answer.IsCorrect ? 1 : 0;
            query = $"INSERT INTO Answers VALUES ('{properContent}', {correct}, {questionId})";
            var aCommand = new SqlCommand(query, connection);
            var rowAnswers = aCommand.ExecuteNonQuery();
            if (rowAnswers == 1)
            {
                Console.WriteLine($"Wrzucono odpowiedź do pytania nr {count}");
            }
        }

        Console.WriteLine($"Wrzucono pytanie nr {count} z {questions.Count}");
        count++;

    }
}

connection.Close();
Console.ReadLine();

