using AutoMapper;
using flavehub.Contracts.Commands;
using flavehub.Contracts.RequestObjs;
using flavehub.Domain;
using flavehub.Repository.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace flavehub.Handlers.Category
{
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, CategoryResponseObj>
    {


        private readonly ICategoryServices _categoryService; 
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public AddCategoryHandler(ICategoryServices categoryServices, IMapper mapper, ILoggerFactory loggerFactory)
        {
            _categoryService = categoryServices;
            _mapper = mapper;
            _logger = loggerFactory.CreateLogger(typeof(AddCategoryHandler));
        }
        public async Task<CategoryResponseObj> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Domain.Category
                {   
                   CategoryName = request.CategoryName
                };
                if (!await _categoryService.CreateCategoryAsync(category))
                {
                    throw new NotImplementedException("Coull not process request");
                }
                return _mapper.Map<CategoryResponseObj>(category);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coull not process request", ex);
                throw new NotImplementedException("Internal error", ex);
            }

        }

    }
}
