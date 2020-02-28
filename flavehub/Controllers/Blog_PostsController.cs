using flavehub.Contracts.RequestObjs; 
using flavehub.Extensions;
using flavehub.Repository.Services;
using flavehub.Contracts.V1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using flavehub.Contracts.ApiCore;
using AutoMapper;
using flavehub.Cache;
using Microsoft.AspNetCore.Http;  
using flavehub.CustomError;
using MediatR;
using flavehub.Contracts.Queries;
using flavehub.Contracts.Commands;
using Microsoft.Extensions.Logging;

namespace flavehub.Controllers
{ 
    
    public class Blog_PostsController : Controller
    { 
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        private readonly ILogger _logger;

        public Blog_PostsController(IMapper mapper, IMediator mediator, IUriService uriService, ILoggerFactory loggerFactory)
        { 
            _mapper = mapper;
            _mediator = mediator;
            _uriService = uriService;
            _logger = loggerFactory.CreateLogger(typeof(Blog_PostsController));
        } 

        [HttpGet(ApiRoutes.Posts.GetAll)]
        [Cached(600)]
        public async Task<ActionResult<IList<PostResponse>>>GetAllPostAsync()
        {
            var query = new GetAllPostsQuery();
            var result = await _mediator.Send(query); 
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] int postId)
        {
            var query = new GetPostByIdQuery(postId);
            var result = await _mediator.Send(query); 
            return result != null ?(IActionResult) Ok(result) : NotFound(new NotFoundError("Post not available")); 
        }




        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] AddPostReqObj postRequest)
        {
            try
            {
                AddPostCommand postCommand = _mapper.Map<AddPostCommand>(postRequest); 
                postCommand.UserId = HttpContext.GetUserId();
                var result = await _mediator.Send(postCommand);
                var locatioUri = _uriService.GetPostUri(result.PostId.ToString());
                _logger.LogInformation("Hello !", result);
                return Created(locatioUri, _mapper.Map<PostResponse>(result));
            }
            catch (Exception ex) { 
                _logger.LogInformation("Unable to process request", ex.InnerException.Message);
                return BadRequest(new BadRequestError("Unable to process request", ex.InnerException.Message));
            }
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromBody] EditPostReqObj postToUpdate)
        {
            try
            {  
                EditPostCommand postCommand = _mapper.Map<EditPostCommand>(postToUpdate);
                var result = await _mediator.Send(postCommand);
                if(result == null) { new NotImplementedErrors("Unable to process request"); }
                var locatioUri = _uriService.GetPostUri(result.PostId.ToString());
                return Created(locatioUri, _mapper.Map<PostResponse>(result));  
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Unable to process request", ex.InnerException.Message);
                return BadRequest(new BadRequestError("Unable to process request", ex.InnerException.Message));
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(ApiRoutes.Posts.Delete)] 
        [Authorize(Policy = AppPolicy.Must_Be_Ultimate)]
        public async Task<IActionResult> Delete([FromRoute] int postId)
        {
            try
            {
                var query = new DeletePostByIdQuery(postId);
                var result = await _mediator.Send(query);
                if (!result) return NotFound(new NotFoundError("Unable to process request"));
                return Ok(new OKMessage("Post Deleted"));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Unable to process request", ex.InnerException.Message);
                return BadRequest(new BadRequestError("Unable to process request", ex.InnerException.Message));
            }
        }
    }
}