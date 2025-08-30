using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.AutoMapper.Article;
using Application.AutoMapper.ContactUs;
using Application.AutoMapper.Identity.Accounting;
using Application.AutoMapper.Identity.User;
using Application.AutoMapper.Map;
using Application.AutoMapper.Menu;
using Application.AutoMapper.News;
using Application.AutoMapper.Notification;
using Application.AutoMapper.QuickAccess;
using Application.AutoMapper.RelatedLink;
using Application.AutoMapper.Rules;
using Application.AutoMapper.ServiceDesk;
using Application.AutoMapper.Slider;
using Application.AutoMapper.Statement;

namespace Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static Type[] RegisterMappings()
        {
            return new Type[]
            {
                typeof(ViewModelMappingProfile),
                typeof(SliderViewModelSliderMappingProfile),
                typeof(ServiceDeskViewModelMappingProfile),
                typeof(NotificationViewModelMappingProfile),
                typeof(StatementViewModelMappingProfile),
                typeof(NewQuickAccessViewModelMappingProfile),
                typeof(NewRelatedLinkViewModelMappingProfile),
                typeof(MenuViewModelMappingProfile),
                typeof(ContactUsViewModelMappingProfile),
                typeof(NewsViewModelMappingProfile),
                typeof(ArticleViewModelMappingProfile),
                typeof(ProvinceViewModelMappingProfile),
                typeof(RulesViewModelMappingProfile),
                typeof(AccountingViewModelMappingProfile),
                typeof(UserViewModelMappingProfile),
            };
        }
    }
}
