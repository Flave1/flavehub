using AutoMapper;
using flavehub.Contracts.Commands;
using flavehub.Contracts.Queries;
using flavehub.Contracts.RequestObjs;
using flavehub.Contracts.V1;
using flavehub.CustomError;
using flavehub.Repository.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly ICategoryServices _categoryServices;
        public CategoryController(IMediator mediator, IUriService uriService, IMapper mapper, ILoggerFactory loggerFactory, ICategoryServices categoryServices)
        {
            _mediator = mediator;
            _uriService = uriService;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger(typeof(CategoryController));
            _categoryServices = categoryServices;
        }

        [HttpGet(ApiRoutes.Category.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //var query = new GetAllCategorQuery();
                //var result = _mediator.Send(query);
                var categoryList = await _categoryServices.GetAllCategoriesAsync();// _categoryService.GetAllCategoriesAsync();
                var response = _mapper.Map<List<CategoryResponseObj>>(categoryList);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Unable to process request", ex.InnerException.Message);
                return NotFound(new NotFoundError("Unable to process request", ex.InnerException.Message));
            }
           
        }

        [HttpPost(ApiRoutes.Category.Create)]
        public async Task<IActionResult> Create([FromBody] AddCategoryReqObj reqObj)
        {
            try
            {
                AddCategoryCommand command = _mapper.Map<AddCategoryCommand>(reqObj);
                var result = await _mediator.Send(command);
                var locationUri = _uriService.GetCategoryUri(result.CategoryId.ToString());
                return Created(locationUri, result);
            } 
            catch (Exception ex)
            {
                _logger.LogInformation("Unable to process request", ex.InnerException.Message);
                return BadRequest(new BadRequestError("Unable to process request", ex.InnerException.Message));
            }
    
        }
    }
}
