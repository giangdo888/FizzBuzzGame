namespace FizzBuzzGame.Server.DTOs
{
    public class InitialAttemptDTO
    {
        public int Id { get; set; }
        public int? Duration { get; set; }
        public int UserId { get; set; } = 1;
        public int? GameId { get; set; }
    }

    public class AttemptResultDTO
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public int UserId { get; set; } = 1;
        public int GameId { get; set; }
        public int CorrectNumber { get; set; }
        public int IncorrectNumber { get; set; }
        public int Score { get; set; }
    }

    public class AttemptQuestionDTO
    {
        public int Id { get; set; }
        public int Question { get; set; }
    }

    public class AttemptAnswerDTO
    {
        public int Id { get; set; }
        public string Answer { get; set; }
    }

}
