using AutoMapper;
using flavehub.Contracts.Commands;
using flavehub.Contracts.RequestObjs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.MappingProfiles
{
    public class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            CreateMap<AddPostReqObj, AddPostCommand>();
            CreateMap<EditPostReqObj, EditPostCommand>();
            CreateMap<AddCommentReqObj, AddCommentCommand>();
            CreateMap<EditCategoryReqObj, EditCategoryCommand>();
            CreateMap<AddCategoryReqObj, AddCategoryCommand>();
        }
    }
}
