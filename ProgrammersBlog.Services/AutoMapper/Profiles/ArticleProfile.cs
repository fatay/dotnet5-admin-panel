using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.AutoMapper.Profiles
{
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            // Automapper dönüştürme işlemlerini gerçekleştirir.
            // Neyi neye dönüştüreceğiz? ArticleAddDto => Article
            CreateMap<ArticleAddDto, Article>().ForMember(a => a.CreatedDate, opt => opt.MapFrom(x=>DateTime.Now));
            CreateMap<ArticleUpdateDto, Article>().ForMember(a => a.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
            
            // Not: Dönüştürürken DateTime kısmını autoset ile DateTime.Now yani bugünün tarihine ayarlamış olduk.
        }
    }
}
