namespace Mediporta.Operations.Interfaces
{
    public interface IStatisticsCalculator
    {
        Task<IDictionary<string, double>> CalculatePercentagesAsync();
    }
}