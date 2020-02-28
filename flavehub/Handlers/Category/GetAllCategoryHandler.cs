using AutoMapper;
using flavehub.Contracts.Queries;
using flavehub.Contracts.RequestObjs;
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
    public class GetAllCategoryHandler : IRequestHandler<GetAllCategorQuery, List<CategoryResponseObj>>
    {
        private readonly ICategoryServices _categoryService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public GetAllCategoryHandler(ICategoryServices categoryService, ILoggerFactory loggerFactory, IMapper mapper)
        {
            _categoryService = categoryService;
            _logger = loggerFactory.CreateLogger(typeof(GetAllCategoryHandler));
            _mapper = mapper;
        }

        public async Task<List<CategoryResponseObj>> Handle(GetAllCategorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categoryList = await _categoryService.GetAllCategoriesAsync();
                var response = _mapper.Map<List<CategoryResponseObj>>(categoryList);
                return await Task.Run(() => response);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Coull not process request", ex.Message);
                throw new NotImplementedException("Internal error", ex);
            }
        }
    }


}
