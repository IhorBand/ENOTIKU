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
        private IUserService userService;

        public SettingsController(IGlobalSettingsService GlobalSettingsService,
            IUserService userService)
        {
            this.GlobalSettingsService = GlobalSettingsService;
            this.userService = userService;
        }

        [Route("api/settings/moviedb/key")]
        [HttpGet]
        public string getMovieDbApiKey()
        {
            var gs = GlobalSettingsService.Get();
            return gs.MovieDBKey;
        }

        [Route("api/settings/themes/change")]
        [HttpGet]
        public string changeTheme()
        {
            if(User != null && User.Identity != null && User.Identity.IsAuthenticated && !string.IsNullOrEmpty(User.Identity.Name))
            {
                var user = userService.Find(User.Identity.Name);
                if (user != null && user.id > 0)
                {
                    user.isDarkTheme = !user.isDarkTheme;
                    try
                    {
                        userService.Update(user);
                    }
                    catch(Exception ex)
                    {
                        return "false";
                    }
                    return "true";
                }
            }
            return "false";
        }
    }
}
