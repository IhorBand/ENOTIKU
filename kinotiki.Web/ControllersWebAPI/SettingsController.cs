using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using kinotiki.BLL;
using kinotiki.Domain.Concrete;
using kinotiki.BLL.Abstract;

namespace kinotiki.Web.ControllersWebAPI
{
    public class SettingsController : ApiController
    {
        private IGlobalSettingsService GlobalSettingsService { get; set; }

        public SettingsController(IGlobalSettingsService GlobalSettingsService)
        {
            this.GlobalSettingsService = GlobalSettingsService;
        }

        [Route("api/settings/moviedb/key")]
        [HttpGet]
        public string getMovieDbApiKey()
        {
            var gs = GlobalSettingsService.Get();
            return gs.MovieDBKey;
        }
    }
}
