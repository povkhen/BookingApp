using BookingApp.WEB_MVC.Models;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace BookingApp.WEB_MVC.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.DisplayName).Replace("\n", "<br />\r\n");

            if (String.IsNullOrEmpty(model))
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(model);
        }


    }
}