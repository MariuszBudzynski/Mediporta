namespace Mediporta.Services
{
    public class ResponseHandlerService
    {
       
        private readonly IGetAllDataUseCase<Tag> _getAllDataUseCase;
        private readonly SortingService<ITagDTO> _sortingService;
        private readonly PaginationService<ITagDTO> _paginationService;
        private readonly StatisticsCalculator<Tag> _statisticsCalculator;

        public ResponseHandlerService(IGetAllDataUseCase<Tag> getAllDataUseCase, SortingService<ITagDTO> sortingService,
            PaginationService<ITagDTO> paginationService,StatisticsCalculator<Tag> statisticsCalculator)
        {
 
            _getAllDataUseCase = getAllDataUseCase;
            _sortingService = sortingService;
            _paginationService = paginationService;
            _statisticsCalculator = statisticsCalculator;
        }

        public async Task<IResult> ReturnResponse(int page,int pageSize,string sortBy,string sortOrder)
        {

            var tags = await _getAllDataUseCase.ExecuteAsync();
            var tagPercentages = await _statisticsCalculator.CalculatePercentagesAsync();
            var tagDTOs = tags.Select(tag => tag.Mapp(t => TagToDTO(t, tagPercentages)));

            tagDTOs = _sortingService.SortTags(tagDTOs, sortBy, sortOrder);
            tagDTOs = _paginationService.PaginateTags(tagDTOs, page, pageSize);

            return Results.Ok(tagDTOs);
        }

        private static ITagDTO TagToDTO(Tag tag, IDictionary<string, double> tagPercentages)
        {
            double percentage = Math.Round(tagPercentages[tag.Name],4);
            return new TagDTO(tag.HasSynonyms, tag.IsModeratorOnly, tag.IsRequired, percentage, tag.Name);
        }


    }
}
