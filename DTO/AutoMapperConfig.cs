using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DigitalSignage.Domain;

namespace DigitalSignage.DTO
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => {

                cfg.CreateMap<BannerSource, BannerSourceDTO>().ReverseMap();
                cfg.CreateMap<RSSSource, RSSSourceDTO>()
                    .IncludeBase<BannerSource, BannerSourceDTO>()
                    .ReverseMap();
                cfg.CreateMap<Text, TextDTO>()
                    .IncludeBase<BannerSource, BannerSourceDTO>()
                    .ReverseMap();
                cfg.CreateMap<Campaign, CampaignDTO>().ReverseMap();
                cfg.CreateMap<Image, ImageDTO>().ReverseMap();
                cfg.CreateMap<Banner, BannerDTO>();
                cfg.CreateMap<BannerDTO, Banner>();
                cfg.CreateMap<RSSItem, RSSItemDTO>().ReverseMap();

            });
        }
    }
}
