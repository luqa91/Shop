using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Shop.Infrastructure
{
    public class AppConfig
    {
        private static string _iconsCategoryFolderRelative = ConfigurationManager.AppSettings["IconsCategoriesFolder"];

        public static string IconsCategoriesFolderRelative
        {
            get
            {
                return _iconsCategoryFolderRelative;
            }
        }

        private static string _picturesFolderRelative = ConfigurationManager.AppSettings["PicturesFolder"];

        public static string PicturesFolderRelative
        {
            get
            {
                return _picturesFolderRelative;
            }
        }
    }
}