using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using flavehub.Contracts.Commands;
using flavehub.Contracts.ErrorResponses;
using flavehub.Contracts.Queries;
using flavehub.Contracts.RequestObjs;
using flavehub.Contracts.V1;
using flavehub.CustomError;
using flavehub.Domain;
using flavehub.Extensions;
using flavehub.Repository.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace flavehub.Controllers
{ 
    
    public class CommentsController : Controller
    { 
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;

        public CommentsController(IMapper mapper, IMediator mediator, IUriService uriService)
        { 
            _mapper = mapper;
            _mediator = mediator;
            _uriService = uriService;
        }

        
        [HttpGet(ApiRoutes.Comment.GetAll)] 
        public async Task<ActionResult<IList<CommentResponse>>> GetAll()
        { 
                var query = new GelAllCommentQuery();
                var result = await _mediator.Send(query);
                return Ok(result);  
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(ApiRoutes.Comment.Create)]
        public async Task<IActionResult> Create([FromBody] AddCommentReqObj comtReq)
        {
            try
            {
                AddCommentCommand command = _mapper.Map<AddCommentCommand>(comtReq);
                command.UserId = HttpContext.GetUserId();
                var result = await _mediator.Send(command);
                if (result == null) { new NotImplementedErrors("Unable to process request"); }
                var locationUri = _uriService.GetCommentUri(result.CommentId.ToString());
                return Created(locationUri, _mapper.Map<CommentResponse>(result));
            }
            catch (Exception ex)
            { 
                return BadRequest(new BadRequestError("Unable to process request", ex.InnerException.Message));
            }

        }
    }
}