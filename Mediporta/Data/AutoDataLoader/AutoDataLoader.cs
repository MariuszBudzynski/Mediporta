public class AutoDataLoader<T> where T : IEntity
{
    private readonly IFirstLoadDataSaveUseCase<T> _saveDataAfterLoad;
    private readonly IForceLoadDataUseCase<T> _forceLoadDataUseCase;
    private readonly HttpClient _client = new HttpClient();

    public AutoDataLoader(IFirstLoadDataSaveUseCase<T> saveDataAfterLoad, IForceLoadDataUseCase<T> forceLoadDataUseCase)
    {
        _saveDataAfterLoad = saveDataAfterLoad;
        _forceLoadDataUseCase = forceLoadDataUseCase;
    }

    public async Task LoadDataJSON(bool useSaveDataAfterLoad = true)
    {
        int pageSize = 100;
        int page = 1;
        int totalTagsToFetch = 1000;
        int totalFetchedTags = 0;

        try
        {
            while (totalFetchedTags < totalTagsToFetch)
            {
                var url = $"https://api.stackexchange.com//2.3/tags?page={page}&pagesize={pageSize}&order=desc&sort=name&site=stackoverflow";
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    await ProcessResponseStream(stream, useSaveDataAfterLoad);
                }

                totalFetchedTags += pageSize;
                page++;
            }
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred while loading data: {ex.Message}");
        }
    }

    private async Task ProcessResponseStream(Stream stream, bool useSaveDataAfterLoad)
    {
        try
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

                if (useSaveDataAfterLoad)
                    await _saveDataAfterLoad.ExecuteAsync(data);
                else
                    await _forceLoadDataUseCase.ExecuteAsync(data);
            }
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred while processing response stream: {ex.Message}");
        }
    }

    public async Task ReloadData(bool useSaveDataAfterLoad = true)
    {
        await LoadDataJSON(useSaveDataAfterLoad);
    }
}
