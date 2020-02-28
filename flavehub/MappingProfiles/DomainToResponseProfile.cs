using AutoMapper;
using flavehub.Contracts.RequestObjs;
using flavehub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flavehub.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {

            CreateMap<Post, PostResponse>().
            ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
 
            CreateMap<Comment, CommentObj>();
            CreateMap<Comment, CommentResponse>();
            CreateMap<Category, CategoryResponseObj>();
        }
    }
}

                
