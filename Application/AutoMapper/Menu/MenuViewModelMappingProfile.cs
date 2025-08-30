using Application.ViewModels.Menu.Request;
using AutoMapper;
using Domain.Entities.MenuComposite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper.Menu
{
    public class MenuViewModelMappingProfile : Profile
    {
        public MenuViewModelMappingProfile()
        {
            CreateMap<MenuComponentRequest, Domain.Entities.MenuComposite.MenuComponent>().ReverseMap();
            CreateMap<MenuViewModelRequest, Domain.Entities.MenuComposite.Menu>().ReverseMap();
            CreateMap<MenuItemViewModelRequest, Domain.Entities.MenuComposite.MenuItem>().ReverseMap();

            CreateMap<MenuComponentResponse, Domain.Entities.MenuComposite.MenuComponent>().ReverseMap();
            CreateMap<MenuViewModelResponse, Domain.Entities.MenuComposite.Menu>().ReverseMap();
            CreateMap<MenuItemViewModelResponse, Domain.Entities.MenuComposite.MenuItem>().ReverseMap();
        }
    }
}
