namespace FizzBuzzGame.Server.DTOs
{
    public class AttemptDTO
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }

    public class QuestionDTO
    {
        public int Id { get; set; } //attemp Id
        public int Question { get; set; }
    }

    public class AnswerDTO
    {
        public int Id { get; set; } //attemp Id
        public int Question { get; set; }
        public int Answer { get; set; }
    }

    public class ResultDTO
    {
        public int Id { get; set; } //attempt Id
        public int CorrectNumber { get; set; }
        public int IncorrectNumber { get; set; }
        public int Score { get; set; }
    }
}
