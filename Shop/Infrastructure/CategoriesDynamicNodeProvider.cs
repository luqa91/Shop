using MvcSiteMapProvider;
using Shop.DAL;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Infrastructure
{
    public class CategoriesDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ProductsContext db = new ProductsContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Category Category in db.Categories)
            {
                DynamicNode node = new DynamicNode();
                node.Title = Category.NameCategory;
                node.Key = "Category_" + Category.CategoryId;
                node.RouteValues.Add("nameCategory", Category.NameCategory);
                returnValue.Add(node);
            }

            return returnValue;
        }
    }
}