using AutoMapper;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Communication.Resopnses;

namespace MyRecipeBook.Application.Services.AutoMapper
{
    public class AutorMapping : Profile
    {
         public AutorMapping() {
            RequestToDomain();
            DomainToResponse();
        }

         private void RequestToDomain()
         {
             CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore());
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
        }
    }


}
