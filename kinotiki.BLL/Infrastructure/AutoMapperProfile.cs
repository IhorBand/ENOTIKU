using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace kinotiki.BLL.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Domain.Entity.GlobalSettings, BLL.Entity.GlobalSettings>()
            //    .ForMember(d => d.id, opt => opt.MapFrom(s => s.id));

            CreateMap<Domain.Entity.GlobalSettings, BLL.Entity.GlobalSettings>()
                .ForMember(
                    cmt => cmt.id,
                    cmtm => cmtm.MapFrom(m =>
                        new BLL.Entity.GlobalSettings
                        {
                            id = m.id,
                            smtpIP = m.smtpIP,
                            smtpMail = m.smtpMail,
                            smtpPassword = m.smtpPassword,
                            smtpPort = m.smtpPort
                        }
                    )
                );
            CreateMap<BLL.Entity.GlobalSettings, Domain.Entity.GlobalSettings>()
                .ReverseMap();

            CreateMap<Domain.Entity.User, BLL.Entity.User>()
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
                            Birthday = m.Birthday
                        }
                    )
                )
                .ForMember(d => d.role, opt => opt.MapFrom(s => s.role >= 0 ? (Entity.Enums.RoleType)s.role : Entity.Enums.RoleType.NonAuthorize))
                .ForMember(d => d.sex, opt => opt.MapFrom(s => s.sex >= 0 ? (Entity.Enums.GenderType)s.sex : Entity.Enums.GenderType.Alien));
            CreateMap<BLL.Entity.User, Domain.Entity.User>()
                .ReverseMap();
        }
    }
}
