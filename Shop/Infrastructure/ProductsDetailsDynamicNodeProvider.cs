using MvcSiteMapProvider;
using Shop.DAL;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Infrastructure
{
    public class ProductsDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ProductsContext db = new ProductsContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Product Product in db.Products)
            {
                DynamicNode node = new DynamicNode();
                node.Title = Product.Title;
                node.Key = "Product_" + Product.ProductId;
                node.ParentKey = "Category_" + Product.CategoryId;
                node.RouteValues.Add("id", Product.ProductId);
                returnValue.Add(node);
            }

            return returnValue;
        }
    }
}