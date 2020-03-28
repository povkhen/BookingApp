using BookingApp.WEB_MVC.Models;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace BookingApp.WEB_MVC.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.DisplayName).Replace("\n", "<br />\r\n");

            if (String.IsNullOrEmpty(model))
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(model);
        }

        //< li class="seat">
        //        <input type = "checkbox" id="" onclick="func" />
        //        <label for="">1</label>
        //</li>
        public static MvcHtmlString SeatCheckButton(this HtmlHelper html,
            AllSeatsProcedureViewModel seat)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder li = new TagBuilder("li");
            li.AddCssClass("seat");

            string id = seat.SeatId.ToString();

            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("type", "checkbox");
            input.MergeAttribute("id", id);
            if (!seat.Free)
            {
                input.MergeAttribute("disabled", "");
            }

            TagBuilder label = new TagBuilder("label");
            label.MergeAttribute("for", id);
            label.SetInnerText(seat.SeatNumber.ToString());

            li.InnerHtml += input.ToString();
            li.InnerHtml += label.ToString();

            return MvcHtmlString.Create(li.ToString());
        }
    }
}