public class AutoDataLoader<T> where T : IEntity
{
    private readonly IFirstLoadDataSaveUseCase<T> _saveDataAfterLoad;
    private readonly IForceLoadDataUseCase<T> _forceLoadDataUseCase;
    private readonly IConfiguration _configuration;
    private readonly IGetAllDataUseCase<T> _getAllDataUseCase;
    private readonly HttpClient _client = new HttpClient();
    private bool _useSaveDataAfterLoad = true;

    public AutoDataLoader(IFirstLoadDataSaveUseCase<T> saveDataAfterLoad, IForceLoadDataUseCase<T> forceLoadDataUseCase,
            IConfiguration configuration, IGetAllDataUseCase<T> getAllDataUseCase)
    {
        _saveDataAfterLoad = saveDataAfterLoad;
        _forceLoadDataUseCase = forceLoadDataUseCase;
        _configuration = configuration;
        _getAllDataUseCase = getAllDataUseCase;
    }

    public AutoDataLoader() { }

    public async Task LoadDataJSON()
    {
        int pageSize = 100;
        int page = 1;
        int totalTagsToFetch = 1000;
        int totalFetchedTags = 0;

        try
        {
            if (!_useSaveDataAfterLoad && !await IsDatabaseEmpty())
            {
                Log.Information("Database is not empty. Skipping auto loading data from API.");
                return;
            }

            while (totalFetchedTags < totalTagsToFetch)
            {
                var apiUrl = _configuration["EndpointSetup:ApiUrl"];
                var url = $"{apiUrl}?page={page}&pagesize={pageSize}&order=desc&sort=name&site=stackoverflow";
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
        catch (Exception ex)
        {
            Log.Error($"An error occurred while loading data: {ex.Message}");
        }
    }

    private async Task ProcessResponseStream(Stream stream)
    {
        try
        {
            using (var decompressedStream = new GZipStream(stream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(decompressedStream))
            {
                string decompressedResponse = await streamReader.ReadToEndAsync();
                JObject? jsonObject = JsonConvert.DeserializeObject<JObject>(decompressedResponse);
                JArray? itemsArray = (JArray?)jsonObject?["items"];

                if (itemsArray.Count == 0)
                    return;

                IEnumerable<T>? data = itemsArray.ToObject<IEnumerable<T>>();

                if (_useSaveDataAfterLoad)
                    await _saveDataAfterLoad.ExecuteAsync(data);
                else
                    await _forceLoadDataUseCase.ExecuteAsync(data);
                    _useSaveDataAfterLoad = true;
            }
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred while processing response stream: {ex.Message}");
        }
    }

    public async Task ReloadData()
    {
        _useSaveDataAfterLoad = false;
        await LoadDataJSON();
    }

    private async Task<bool> IsDatabaseEmpty()
    {
        var data = await _getAllDataUseCase.ExecuteAsync();
        return !data.Any();
    }
}
