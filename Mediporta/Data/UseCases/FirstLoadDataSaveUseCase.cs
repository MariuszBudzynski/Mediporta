namespace Mediporta.Data.UseCases
{
    public class FirstLoadDataSaveUseCase<T> : IFirstLoadDataSaveUseCase<T> where T : class, IEntity
    {
        private readonly IDataRepository<T> _repository;

        public FirstLoadDataSaveUseCase(IDataRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(IEnumerable<T> data)
        {
            var existingTagNames = (await _repository.GetAllDataAsync()).Select(tag => tag.Name).ToList();

            var newTags = data.Where(tag => !existingTagNames.Contains(tag.Name)).ToList();

            await _repository.SaveDataAsync(newTags);
        }

    }
}
