using System.IO;
using System.Web.Mvc;

namespace Shop.Infrastructure
{
    public static class UrlHelpers
    {
        public static string IconsCategoriesPath(this UrlHelper helper, string nameIconsCategories)
        {
            var IkonyKategoriiFolder = AppConfig.IconsCategoriesFolderRelative;
            var path = Path.Combine(IkonyKategoriiFolder, nameIconsCategories);
            var pathAbsolute = helper.Content(path);
            return pathAbsolute;
        }


        public static string PicturesPath(this UrlHelper helper, string namePicture)
        {
            var ObrazkiFolder = AppConfig.PicturesFolderRelative;
            var path = Path.Combine(ObrazkiFolder, namePicture);
            var pathAbsolute = helper.Content(path);
            return pathAbsolute;
        }





    }
}