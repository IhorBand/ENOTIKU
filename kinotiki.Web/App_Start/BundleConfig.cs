using System.Web;
using System.Web.Optimization;

namespace kinotiki.Web
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-validate").Include(
                        "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-cookie").Include(
                        "~/Scripts/jquery.cookie.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.8.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                        "~/Scripts/umd/popper.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/budles/scripts/moviedbhelper").Include("~/Content/Scripts/Helpers/TheMovieDbHelper.js"));
            bundles.Add(new ScriptBundle("~/budles/scripts/registerValidation").Include("~/Content/Scripts/Helpers/RegisterValidation.js"));
            bundles.Add(new ScriptBundle("~/budles/scripts/Home/Index").Include("~/Content/Scripts/Controllers/Home/Index.js"));


            // Styles

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/styles/fontAwesome/css/font-awesome.min.css",
                      "~/Content/styles/social-buttons/social-buttons.css",
                      "~/Content/styles/reset.css"));
            
            bundles.Add(new StyleBundle("~/Content/css/DarkTheme").Include("~/Content/Styles/DarkTheme.css"));
            bundles.Add(new StyleBundle("~/Content/css/LightTheme").Include("~/Content/Styles/LightTheme.css"));

            bundles.Add(new StyleBundle("~/Content/css/slick").Include("~/Content/Slick/slick.css", 
                "~/Content/Slick/slick-theme.css"));

            bundles.Add(new StyleBundle("~/Content/css/main").Include("~/Content/styles/main.css"));

            bundles.Add(new StyleBundle("~/Content/Styles/login").Include("~/Content/Styles/login.css"));
        }
    }
}
