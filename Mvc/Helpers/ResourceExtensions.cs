using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Modules.Pages;

namespace SitefinityWebApp.Mvc.Helpers
{
    public static class ResourceExtensions
    {
        /// <summary>
        /// Adds the CSS file.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="path">The path.</param>
        /// <param name="id">The identifier to prevent duplicates on the page.</param>
        public static void AddCssFile(this HtmlHelper html, string path, string id = null)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                //GENERATE CONTROL ID IF APPLICABLE
                if (string.IsNullOrWhiteSpace(id))
                {
                    id = GenerateSafeId(path);
                }

                //DETERMINE IF HEAD AVAILABLE AND STYLESHEET NOT ADDED BEFORE
                if (page.Header != null && page.Header.FindControl(id) == null)
                {
                    //HANDLE ANY STRANGE CHARACTERS AND RESOLVE
                    path = WebUtility.HtmlEncode(path.StartsWith("~/")
                        ? page.ResolveUrl(path) : path);

                    var csslink = new HtmlGenericControl("link");
                    csslink.ID = id;
                    csslink.Attributes.Add("href", path);
                    csslink.Attributes.Add("type", "text/css");
                    csslink.Attributes.Add("rel", "stylesheet");

                    //ADD STYLESHEET TO PAGE HEAD
                    page.Header.Controls.Add(csslink);
                }
            }
        }

        /// <summary>
        /// Adds the CSS resource.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="resource">The resource.</param>
        public static void AddCssResource(this HtmlHelper html, string resource)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                //USE LOCAL RESOURCE BY DEFAULT
                var type = typeof(ResourceExtensions);

                //HANDLE IF COMING FROM TELERIK RESOURCE ASSEMBLY
                if (resource.StartsWith("Telerik.Sitefinity.Resources."))
                {
                    type = typeof(Telerik.Sitefinity.Resources.Reference);
                }

                //ADD STYLESHEET TO PAGE HEAD
                html.AddCssFile(page.ClientScript.GetWebResourceUrl(type, resource), resource);
            }
        }

        /// <summary>
        /// Adds the script file.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="path">The path.</param>
        /// <param name="head">Specify to add to the page head.</param>
        /// <param name="id">The identifier to prevent duplicates on the page.</param>
        public static void AddScriptFile(this HtmlHelper html, string path, bool head = false, string id = null)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                //GENERATE CONTROL ID IF APPLICABLE
                if (string.IsNullOrWhiteSpace(id))
                {
                    id = GenerateSafeId(path);
                }

                //HANDLE ANY STRANGE CHARACTERS AND RESOLVE
                path = WebUtility.HtmlEncode(path.StartsWith("~/")
                    ? page.ResolveUrl(path) : path);

                //ADD SCRIPT IF APPLICABLE
                if (head)
                {
                    //DETERMINE IF HEAD AVAILABLE AND SCRIPT NOT ADDED BEFORE
                    if (page.Header != null && page.Header.FindControl(id) == null)
                    {

                        var jslink = new HtmlGenericControl("script");
                        jslink.ID = id;
                        jslink.Attributes.Add("src", path);
                        jslink.Attributes.Add("type", "text/javascript");

                        //ADD STYLESHEET TO PAGE HEAD
                        page.Header.Controls.Add(jslink);
                    }
                }
                else
                {
                    page.ClientScript.RegisterClientScriptInclude(page.GetType(), id, path);
                }
            }
        }

        /// <summary>
        /// Adds the script resource.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="head">if set to <c>true</c> [head].</param>
        /// <param name="id">The identifier.</param>
        public static void AddScriptResource(this HtmlHelper html, string resource, bool head = false, string id = null)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                //USE LOCAL RESOURCE BY DEFAULT
                var type = typeof(ResourceExtensions);

                //HANDLE IF COMING FROM TELERIK RESOURCE ASSEMBLY
                if (resource.StartsWith("Telerik.Sitefinity.Resources."))
                {
                    type = typeof(Telerik.Sitefinity.Resources.Reference);
                }

                //ADD STYLESHEET TO PAGE HEAD
                html.AddScriptFile(page.ClientScript.GetWebResourceUrl(type, resource), head, resource);
            }
        }

        /// <summary>
        /// Adds the script reference.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="reference">The reference.</param>
        public static void AddScriptReference(this HtmlHelper html, ScriptRef reference)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                PageManager.ConfigureScriptManager(page, reference);
            }
        }

        /// <summary>
        /// Generates a scrubbed name for a control id.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string GenerateSafeId(string value)
        {
            //VALIDATE INPUT
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            string pattern = @"[!""#$%&'()\*\+,\./:;<=>\?@\[\\\]^`{\|}~ ]";
            return Regex.Replace(value.ToLower(), pattern, string.Empty);
        }
    }
}