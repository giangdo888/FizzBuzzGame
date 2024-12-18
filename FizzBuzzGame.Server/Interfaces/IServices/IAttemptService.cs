using FizzBuzzGame.Server.DTOs;

namespace FizzBuzzGame.Server.Interfaces.IServices
{
    public interface IAttemptService
    {
        public Task<AttemptQuestionDTO> CreateAndStartAttemptAsync(InitialAttemptDTO initialAttemptDTO);
        public KeyValuePair<bool, AttemptQuestionDTO> HandleAttemptAsnwer(AttemptAnswerDTO attemptAnswerDTO);
        public Task<AttemptResultDTO> FinalizeAttemptAsync(int attemptId);
        public Task<AttemptResultDTO> GetAttemptResultAsync(int attemptId);
    }
}
