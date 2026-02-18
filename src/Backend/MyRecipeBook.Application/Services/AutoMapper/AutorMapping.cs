using AutoMapper;
using MyRecipeBook.Communication.Request;

namespace MyRecipeBook.Application.Services.AutoMapper
{
    public class AutorMapping : Profile
    {
         public AutorMapping() {
            RequestToDomain();
        }

         private void RequestToDomain()
         {
             CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }


}
