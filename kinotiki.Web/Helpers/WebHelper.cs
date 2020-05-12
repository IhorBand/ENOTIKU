using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kinotiki.Web.Helpers
{
    public class WebHelper
    {
        public static string GetDefaultUserImageSrc(BLL.Entity.Enums.GenderType gender)
        {
            return GetDefaultUserImageSrc((int)gender);
        }

        public static string GetDefaultUserImageSrc(int gender)
        {
            if (gender == (int)kinotiki.BLL.Entity.Enums.GenderType.Male)
                return "~/Content/Images/Users/defaultMaleImg.png";
            else if (gender == (int)kinotiki.BLL.Entity.Enums.GenderType.Female)
                return "~/Content/Images/Users/defaultFemaleImg.jpg";
            else
                return "~/Content/Images/Users/defaultAlienImg.png";
        }
    }
}