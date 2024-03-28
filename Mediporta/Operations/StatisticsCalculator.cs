namespace Mediporta.Operations
{
    public class StatisticsCalculator<T> : IStatisticsCalculator where T : class, IEntity
    {
        private readonly IGetAllDataUseCase<T> _getAllDataUseCase;

        public StatisticsCalculator(IGetAllDataUseCase<T> getAllDataUseCase)
        {
            _getAllDataUseCase = getAllDataUseCase;
        }

        public StatisticsCalculator(){}

        public async Task<IDictionary<string, double>> CalculatePercentagesAsync()
        {
            var tags = await _getAllDataUseCase.ExecuteAsync();

            int totalTagCount = tags.Sum(tag => tag.Count);

            var tagPercentages = tags.ToDictionary(
                tag => tag.Name,
                tag => (double)tag.Count / totalTagCount * 100);

            return tagPercentages;
        }
    }
}
