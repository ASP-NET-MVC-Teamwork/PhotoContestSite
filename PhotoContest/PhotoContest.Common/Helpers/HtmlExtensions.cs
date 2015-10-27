namespace PhotoContest.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;

    public static class HtmlExtensions
    {
        public static MvcHtmlString Submit(this HtmlHelper helper, object htmlAttributes = null)
        {
            var input = new TagBuilder("input");
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;
            input.MergeAttributes(attributes);
            input.Attributes.Add("type", "submit");
            return new MvcHtmlString(input.ToString());
        }

        public static MvcHtmlString voteAjax(this AjaxHelper ajaxHelper, string linkText, string actionName,
            string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);

            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }
    }
}
