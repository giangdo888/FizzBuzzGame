using FizzBuzzGame.Server.DTOs;

namespace FizzBuzzGame.Server.Interfaces.IServices
{
    public interface IAttemptService
    {
        public Task<InittialAttemptQuestionDTO> CreateAndStartAttemptAsync(InitialAttemptDTO initialAttemptDTO);
        public AttemptResultAndNewQuestionDTO HandleAttemptAsnwer(AttemptAnswerDTO attemptAnswerDTO);
        public Task<AttemptResultDTO> FinalizeAttemptAsync(int attemptId);
        public Task<AttemptResultDTO> GetAttemptResultAsync(int attemptId);
    }
}
