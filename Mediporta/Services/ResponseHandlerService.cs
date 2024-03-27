namespace Mediporta.Services
{
    public class ResponseHandlerService
    {
       
        private readonly IGetAllDataUseCase<Tag> _getAllDataUseCase;
        private readonly SortingService<Tag> _sortingService;
        private readonly PaginationService<Tag> _paginationService;

        public ResponseHandlerService(IGetAllDataUseCase<Tag> getAllDataUseCase, SortingService<Tag> sortingService,
            PaginationService<Tag> paginationService)
        {
 
            _getAllDataUseCase = getAllDataUseCase;
            _sortingService = sortingService;
            _paginationService = paginationService;
        }

        public async Task<IResult> ReturnResponse(int page,int pageSize,string sortBy,string sortOrder)
        {

            var tags = await _getAllDataUseCase.ExecuteAsync();

            tags = _sortingService.SortTags(tags, sortBy, sortOrder);
            tags = _paginationService.PaginateTags(tags, page, pageSize);


            return Results.Ok(tags);

        }

    }
}
