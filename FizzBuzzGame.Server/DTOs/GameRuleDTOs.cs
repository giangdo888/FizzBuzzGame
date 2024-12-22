namespace FizzBuzzGame.Server.DTOs
{
    //use when bind with GameDTO
    public class GameRuleDTO
    {
        public int? Divisor { get; set; }
        public string Word { get; set; }
        public string Description { get; set; }
    }

    //use when stand alone
    public class StandAloneGameRuleDTO
    {
        public int? GameId { get; set; }
        public int? Divisor { get; set; }
        public string Word { get; set; }
        public string Description { get; set; }
    }

    //use for identify rule
    public class GameRuleIdentifierDTO
    {
        public int? GameId { get; set; }
        public int? Divisor { get; set; }
    }
}