namespace Quiz.Data
{
    public class CheckAnswerDto
    {
        public bool IsCorrect { get; set; }
        public int NextCategory { get; set; }
        public string? Error { get; set; }
    }
}
