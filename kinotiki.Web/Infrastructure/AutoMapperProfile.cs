using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kinotiki.Web.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Domain.Entity.GlobalSettings, BLL.Entity.GlobalSettings>()
            //    .ForMember(d => d.id, opt => opt.MapFrom(s => s.id));

            CreateMap<BLL.Entity.User, Models.Account.RegisterUser>()
                .ForMember(
                    cmt => cmt.id,
                    cmtm => cmtm.MapFrom(m =>
                        new Models.Account.RegisterUser
                        {
                            id = m.id,
                            email = m.email,
                            imageData = m.imageData,
                            imageMimeType = m.imageMimeType,
                            login = m.login,
                            password = m.password,
                            sex = (int)m.sex
                        }
                    )
                )
                .ForMember(d => d.BirthdayDate, opt => opt.MapFrom(s => s.Birthday.ToString("yyyy-MM-dd")));
            CreateMap<Models.Account.RegisterUser, BLL.Entity.User>()
                .ForMember(
                    cmt => cmt.id,
                    cmtm => cmtm.MapFrom(m =>
                        new BLL.Entity.User
                        {
                            id = m.id,
                            email = m.email,
                            imageData = m.imageData,
                            imageMimeType = m.imageMimeType,
                            login = m.login,
                            password = m.password,
                            sex = (BLL.Entity.Enums.GenderType)m.sex
                        }
                    )
                )
                .ForMember(d => d.Birthday, opt => opt.MapFrom(s => new DateTime(s.BirthdayYear, s.BirthdayMonth, s.BirthdayDay)));
        }
    }
}