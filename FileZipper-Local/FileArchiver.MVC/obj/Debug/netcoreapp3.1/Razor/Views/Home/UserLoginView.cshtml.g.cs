#pragma checksum "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59c0ad4584cebcfdf6ed793608d4d8d378d4c003"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_UserLoginView), @"mvc.1.0.view", @"/Views/Home/UserLoginView.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\_ViewImports.cshtml"
using FileArchiver.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
using FileArchiverCommon.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59c0ad4584cebcfdf6ed793608d4d8d378d4c003", @"/Views/Home/UserLoginView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"596e9572b2f80b6ff26b05bf29d56ef710b5a883", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UserLoginView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"container mt-4\">\n    <div class=\"row mb-5\">\n        <div class=\"col-md-5\">\n            <h5 class=\"ml-3 text-left\" style=\"top: 60px\">Logged in user: ");
#nullable restore
#line 6 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
                                                                    Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\n        </div>\n        <div class=\"col-md-5\">\n            ");
#nullable restore
#line 9 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
       Write(Html.ActionLink(linkText: "Edit user details", actionName: "UpdateUser", routeValues: null, htmlAttributes: new { @class = "btn btn-success btn-sm text-center mr-5", @style = "top: 60px;" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n        </div>\n        <div class=\"col-md-2\">\n            ");
#nullable restore
#line 12 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
       Write(Html.ActionLink(linkText: "Log out", controllerName: "Login", actionName: "Logout", routeValues: null, htmlAttributes: new { @class = "btn btn-danger btn-sm text-center mr-5", @style = "top: 60px;" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </div>
    </div>
    <div class=""row"">
        <div class=""col-md-1""></div>
        <div class=""col-md-10 ml-4"">
            <div class=""row border-bottom border-dark mb-3"">
                <div class=""col-md-5"">
                    <label class=""text-left d-inline-block font-weight-bold"">Document name</label>
                </div>
                <div class=""col-md-5 text-left"">
                    <label class=""d-inline-block font-weight-bold"">Date Created</label>
                </div>
                <div class=""col-md-2 text-right"">
                    <label class=""d-inline-block text-right""></label>
                </div>
            </div>
");
#nullable restore
#line 29 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
               for (var i = 0; i < Model.Files.Count; i++)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"row border-bottom\">\n                    <div class=\"col-md-5\">\n                        <label");
            BeginWriteAttribute("id", " id=\"", 1621, "\"", 1633, 2);
            WriteAttributeValue("", 1626, "file_", 1626, 5, true);
#nullable restore
#line 33 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
WriteAttributeValue("", 1631, i, 1631, 2, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"text-left d-inline-block\">");
#nullable restore
#line 33 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
                                                                        Write(Model.Files[i].FileName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                    </div>\n                    <div class=\"col-md-5 text-left\">\n                        <label class=\"d-inline-block\">");
#nullable restore
#line 36 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
                                                 Write(Model.Files[i].Created.ToString("dd-MM-yyyy HH:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\n                    </div>\n                    <div class=\"col-md-2 text-right\">\n                        <a");
            BeginWriteAttribute("href", " href=\"", 2003, "\"", 2087, 1);
#nullable restore
#line 39 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
WriteAttributeValue("", 2010, Url.Action("DownloadFile", "Home", new { fileId = Model.Files[@i].FileId  }), 2010, 77, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Download</a>\n                    </div>\n                </div> ");
#nullable restore
#line 41 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
                       } 

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\n        <div class=\"col-md-1\"></div>\n    </div>\n</div>\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
