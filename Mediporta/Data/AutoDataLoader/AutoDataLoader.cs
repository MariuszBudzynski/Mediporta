namespace Mediporta.Data.AutoDataLoader
{
    public class AutoDataLoader<T> where T : IEntity
    {
        private readonly IFirstLoadDataSaveUseCase<T> _saveDataAfterLoad;
        private readonly HttpClient _client = new HttpClient();

        public AutoDataLoader(IFirstLoadDataSaveUseCase<T> saveDataAfterLoad)
        {
            _saveDataAfterLoad = saveDataAfterLoad;
        }

        public async Task LoadDataJSON()
        {
            int pageSize = 100;
            int page = 1;
            int totalTagsToFetch = 1000;
            int totalFetchedTags = 0;

            while (totalFetchedTags < totalTagsToFetch)
            {
                var url = $"https://api.stackexchange.com//2.3/tags?page={page}&pagesize={pageSize}&order=desc&sort=name&site=stackoverflow";
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    await ProcessResponseStream(stream);
                }

                totalFetchedTags += pageSize;
                page++;
            }
        }

        private async Task ProcessResponseStream(Stream stream)
        {
            using (var decompressedStream = new GZipStream(stream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(decompressedStream))
            {
                string decompressedResponse = await streamReader.ReadToEndAsync();
                JObject jsonObject = JsonConvert.DeserializeObject<JObject>(decompressedResponse);
                JArray itemsArray = (JArray)jsonObject["items"];

                if (itemsArray.Count == 0)
                    return;

                IEnumerable<T> data = itemsArray.ToObject<IEnumerable<T>>();
                await _saveDataAfterLoad.ExecuteAsync(data);
            }
        }
    }
}

