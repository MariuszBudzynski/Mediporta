namespace Mediporta.Common.Data.UseCases
{
    public class GetAllDataUseCase<T> : IGetAllDataUseCase<T> where T : class, IEntity
    {
        private readonly IDataRepository<T> _repository;

        public GetAllDataUseCase(IDataRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> ExecuteAsync()
        {
            try
            {
                return await _repository.GetAllDataAsync();
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred while getting all data: {ex.Message}");
                throw;
            }
        }
    }
}
