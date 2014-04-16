Include CSS and JavaScript Resources in Sitefinity MVC Widgets
==============================================================

Sitefinity MVC helper for adding scripts and stylesheets to the page.

*Instructions:*

1. Add ~/Mvc/Helpers/ResourceExtensions.cs to your SitefinityWebApp
2. In your views, add scripts and stylesheets as so:

```
@using SitefinityWebApp.Mvc.Helpers

@{
    Html.AddScriptReference(Telerik.Sitefinity.Modules.Pages.ScriptRef.JQuery);
    Html.AddScriptReference(Telerik.Sitefinity.Modules.Pages.ScriptRef.KendoWeb);
    Html.AddCssResource("Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_common_min.css");
    Html.AddCssResource("Telerik.Sitefinity.Resources.Scripts.Kendo.styles.kendo_default_min.css");

    //OPTIONAL VARIATIONS OF ADDING SCRIPTS AND STYLES
    Html.AddCssFile("http://somedomainx.com/outthere.css");
    Html.AddCssFile("~/mycustomstyles.css");
    Html.AddScriptFile("~/mycustomscript.js");
    Html.AddScriptFile("http://somedomainx.com/overtherainbow.js", true); //ADD TO PAGE HEAD
    Html.AddScriptResource("MyPlace.Sitefinity.Web.Scripts.sitefinityhelpers.js");
}
```
