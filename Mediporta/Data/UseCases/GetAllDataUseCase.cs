namespace Mediporta.Data.UseCases
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
            return await _repository.GetAllDataAsync();
        }
    }
}
