using AutoMapper;
using MyRecipeBook.Communication.Resopnses;
using MyRecipeBook.Domain.Services.LoggedUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRecipeBook.Application.UseCases.User.Profile
{
    
    public class GetUserProfileUseCase : IGetUserProfileUseCase
    {

        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;

        public GetUserProfileUseCase(ILoggedUser loggedUser,
                                     IMapper mapper)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
        }


        public async Task<ResponseUserProfileJson> Execute()
        {
            var user = await _loggedUser.User();
            return _mapper.Map< ResponseUserProfileJson>(user);
         
        }
    }
}
