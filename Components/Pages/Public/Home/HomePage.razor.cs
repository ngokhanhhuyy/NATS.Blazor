using Microsoft.AspNetCore.Components;
using NATS.Helpers;
using NATS.Models;
using NATS.Services.Dtos.RequestDtos;
using NATS.Services.Dtos.ResponseDtos;
using NATS.Services.Enums;
using NATS.Services.Interfaces;

namespace NATS.Components.Pages.Public;

public partial class HomePage : ComponentBase
{
    #region DependencyInjection
    private readonly ISliderItemService _sliderItemService;
    private readonly ISummaryItemService _summaryItemService;
    private readonly IGeneralSettingsService _generalSettingsService;
    private readonly IAboutUsIntroductionService _aboutUsIntroductionService;
    private readonly ICatalogItemService _catalogItemService;
    private readonly IRouteHelper _routeHelper;
    #endregion

    #region States
    private HomePageModel _model;
    #endregion

    public HomePage(
            ISliderItemService sliderItemService,
            ISummaryItemService summaryItemService,
            IGeneralSettingsService generalSettingsService,
            IAboutUsIntroductionService aboutUsIntroductionService,
            ICatalogItemService catalogItemService,
            IRouteHelper routeHelper)
    {
        _sliderItemService = sliderItemService;
        _summaryItemService = summaryItemService;
        _generalSettingsService = generalSettingsService;
        _aboutUsIntroductionService = aboutUsIntroductionService;
        _catalogItemService = catalogItemService;
        _routeHelper = routeHelper;
    }

    #region LifeCycles
    protected override async Task OnInitializedAsync()
    {
        Task<GeneralSettingsResponseDto> generalSettingsResponseDtoTask;
        generalSettingsResponseDtoTask = _generalSettingsService.GetAsync();

        Task<List<SliderItemResponseDto>> sliderItemResponseDtosTask;
        sliderItemResponseDtosTask = _sliderItemService.GetListAsync();
        
        Task<List<SummaryItemResponseDto>> summaryItemResponseDtosTask;
        summaryItemResponseDtosTask = _summaryItemService.GetListAsync();

        Task<AboutUsIntroductionResponseDto> aboutUsIntroductionResponseDtoTask;
        aboutUsIntroductionResponseDtoTask = _aboutUsIntroductionService.GetAsync();

        CatalogItemListRequestDto catalogItemListRequestDto;
        catalogItemListRequestDto = new CatalogItemListRequestDto();
        Task<List<CatalogItemBasicResponseDto>> catalogItemResponseDtosTask;
        catalogItemResponseDtosTask = _catalogItemService
            .GetListAsync(catalogItemListRequestDto);

        await Task.WhenAll(
            generalSettingsResponseDtoTask,
            sliderItemResponseDtosTask,
            summaryItemResponseDtosTask,
            catalogItemResponseDtosTask);

        _model = new HomePageModel
        {
            SliderItems = sliderItemResponseDtosTask.Result
                .Select(dto => new SliderItemDetailModel(dto))
                .ToList(),
            SummaryItems = summaryItemResponseDtosTask.Result
                .Select(dto => new SummaryItemDetailModel(dto))
                .ToList(),
            AboutUsIntroduction = new AboutUsIntroductionDetailModel(
                aboutUsIntroductionResponseDtoTask.Result),
            Services = catalogItemResponseDtosTask.Result
                .Where(dto => dto.Type == CatalogItemType.Service)
                .Select(dto => new CatalogItemBasicModel(dto))
                .ToList(),
            Courses = catalogItemResponseDtosTask.Result
                .Where(dto => dto.Type == CatalogItemType.Course)
                .Select(dto => new CatalogItemBasicModel(dto))
                .ToList(),
            Products = catalogItemResponseDtosTask.Result
                .Where(dto => dto.Type == CatalogItemType.Product)
                .Select(dto => new CatalogItemBasicModel(dto))
                .ToList(),
            GeneralSettings = new GeneralSettingsDetailModel(
                generalSettingsResponseDtoTask.Result)
        };
    }
    #endregion

    #region ModelStructure
    private class HomePageModel
    {
        public List<SliderItemDetailModel> SliderItems { get; set; }
        public List<SummaryItemDetailModel> SummaryItems { get; set; }
        public AboutUsIntroductionDetailModel AboutUsIntroduction { get; set; }
        public List<CatalogItemBasicModel> Services { get; set; }
        public List<CatalogItemBasicModel> Courses { get; set; }
        public List<CatalogItemBasicModel> Products { get; set; }
        public GeneralSettingsDetailModel GeneralSettings { get; set; }
    }
    #endregion
}