using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kinotiki.BLL.Entity;
using kinotiki.BLL.Abstract;
using AutoMapper;

namespace kinotiki.Domain.Concrete
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private kinotikiDbContext context = new kinotikiDbContext();

        private IMapper mapper { get; set; }

        public GlobalSettingsService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public GlobalSettings Get()
        {
            var gs = context.GlobalSettings.FirstOrDefault();
            if(gs != null)
            {
                var gsModel = mapper.Map<BLL.Entity.GlobalSettings>(gs);
                return gsModel;
            }
            return new GlobalSettings();
        }

        public void Save(GlobalSettings newSettingsModel)
        {
            var newSettings = mapper.Map<Domain.Entity.GlobalSettings>(newSettingsModel);
            var curSettings = context.GlobalSettings.FirstOrDefault();
            context.Entry(curSettings).CurrentValues.SetValues(newSettings);
            context.SaveChanges();
        }
    }
}
