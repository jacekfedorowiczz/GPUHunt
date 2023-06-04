using AutoMapper;
using GPUHunt.Application.GraphicCard.Commands.CrawlGraphicCards;
using GPUHunt.Application.GraphicCard.Commands.UpdateGraphicCards;
using GPUHunt.Application.GraphicCard.Queries.GetPaginatedGraphicCards;
using GPUHunt.Application.GraphicCard.Queries.IsDatabaseNotEmpty;
using GPUHunt.Application.GraphicCard.Queries.ScrapGraphicCards;
using GPUHunt.Application.Models.DTOs;
using GPUHunt.Domain.Interfaces;
using GPUHunt.Domain.Models;
using GPUHunt.MVC.Extensions;
using GPUHunt.MVC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GPUHunt.MVC.Controllers
{
    public class GraphicCardController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GraphicCardController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var isDatabaseNotEmpty = await _mediator.Send(new IsDatabaseNotEmptyQuery());

            if (isDatabaseNotEmpty == true)
            {
                return RedirectToAction("GetCards");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCards(int? page, string sortBy, string sortOrder, string searchPhrase)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            ViewBag.SearchPhrase = searchPhrase;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentSort = sortBy;
            ViewBag.SortOrder = sortOrder == "desc" ? "desc" : "asc";

            var pagedResult = await _mediator.Send(new GetPaginatedGraphicCardsQuery(new GraphicCardQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchPhrase = searchPhrase,
                SortBy = sortBy,
                SortDirection = ViewBag.SortOrder == "desc" ? SortDirection.DESC : SortDirection.ASC
            }));
            var results = new PaginationModel<GraphicCardDto>(pagedResult.Items, pagedResult.TotalItemsCount, pageSize, pageNumber);

            if (ViewBag.TotalCount == 0)
            {
                return RedirectToAction("Index", NoContent());
            }

            return View(results);
        }

        [HttpGet]
        public async Task<IActionResult> Crawl()
        {
            var isDatabaseNotEmpty = await _mediator.Send(new IsDatabaseNotEmptyQuery());

            if (isDatabaseNotEmpty == true)
            {
                return RedirectToAction("GetCards");
            }

            await _mediator.Send(new CrawlGraphicCardsCommand());
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Scrap()
        {
            try
            {
                if (User.Identity == null || !User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("/Account/Login", new { area = "Identity " });
                }
                var result = await _mediator.Send(new ScrapGraphicCardsQuery());

                return Json(result, Ok());
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            try
            {
                await _mediator.Send(new UpdateGraphicCardsCommand());

                this.SetNotification("success", "Pomyślnie zaktualizowano karty w bazie danych.");

                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw new Exception("Something went wrong");
            }
        }
    }
}
