namespace Mediporta.Data.UseCases
{
    public class ForceLoadDataUseCase<T> : IForceLoadDataUseCase<T> where T : class, IEntity
    {
        private readonly IDataRepository<T> _repository;

        public ForceLoadDataUseCase(IDataRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(IEnumerable<T> data)
        {
            try
            {
                List<T> tagsToUpdate = new();
                List<T> tagsToAdd = new();

                var dataToCheck = await _repository.GetAllDataAsync();

                foreach (var tag in data)
                {
                    if (dataToCheck.FirstOrDefault(x => x.Name == tag.Name) != null)
                    {
                        tagsToAdd.Add(tag);
                    }
                    else
                    {
                        tagsToUpdate.Add(tag);
                    }
                }

                if (tagsToAdd.Any())
                {
                    await _repository.SaveDataAsync(tagsToAdd);
                }

                if (tagsToUpdate.Any())
                {
                    await _repository.UpdateDataAsync(tagsToUpdate);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while executing ForceLoadDataUseCase: {ex.Message}");
                throw;
            }
        }
    }
}
