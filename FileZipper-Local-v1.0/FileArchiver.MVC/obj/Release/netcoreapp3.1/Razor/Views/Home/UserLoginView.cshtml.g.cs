#pragma checksum "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2155fef261e0b4e6728fc6b904e032c79c016885"
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
#line 1 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\_ViewImports.cshtml"
using FileArchiver.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
using FileArchiverCommon.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2155fef261e0b4e6728fc6b904e032c79c016885", @"/Views/Home/UserLoginView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ff2ad1fd4c3897b540e3e4762a62e8d8ceb4664e", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UserLoginView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"container mt-4\">\r\n    <div class=\"row mb-5\">\r\n        <div class=\"col-md-5\">\r\n            <h5 class=\"ml-3 text-left position-fixed\" style=\"top: 60px\">Logged in admin: ");
#nullable restore
#line 6 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
                                                                                    Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        </div>\r\n        <div class=\"col-md-5\">\r\n            ");
#nullable restore
#line 9 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
       Write(Html.ActionLink(linkText: "Edit user details", actionName: "UpdateUser", routeValues: null, htmlAttributes: new { @class = "btn btn-success btn-sm text-center mr-5", @style = "position: fixed; top: 60px;" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-md-2\">\r\n            ");
#nullable restore
#line 12 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
       Write(Html.ActionLink(linkText: "Log out", actionName: "Logout", routeValues: null, htmlAttributes: new { @class = "btn btn-danger btn-sm text-center mr-5", @style = "position: fixed; top: 60px;" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-4\"></div>\r\n        <div class=\"col-md-5\">\r\n");
#nullable restore
#line 18 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
              
                for (var i = 1; i < Model.Files.Count + 1; i++)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"row border-bottom\">\r\n                        <div class=\"col-md-5 mt-1\">\r\n                            <label");
            BeginWriteAttribute("id", " id=\"", 1143, "\"", 1155, 2);
            WriteAttributeValue("", 1148, "file_", 1148, 5, true);
#nullable restore
#line 23 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
WriteAttributeValue("", 1153, i, 1153, 2, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"text-left d-inline-block\">");
#nullable restore
#line 23 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
                                                                            Write(Model.Files[i - 1].FileName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label>\r\n                        </div>\r\n                        <div class=\"col-md-2\"></div>\r\n                        <div class=\"col-md-5\">\r\n                            <a");
            BeginWriteAttribute("id", " id=\"", 1392, "\"", 1399, 1);
#nullable restore
#line 27 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
WriteAttributeValue("", 1397, i, 1397, 2, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" href=\"#\" class=\"nav-link  d-inline-block text-right text-success\">Download</a>\r\n                        </div>\r\n                    </div>\r\n");
#nullable restore
#line 30 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </div>
        <div class=""col-md-3""></div>
    </div>
    <div id=""message-box"" style=""position: fixed; right: 30px; bottom: 60px; width: 200px; height: 100px; background-color: green; padding: 5px; "">
        <p id=""error-message""></p>
    </div>
</div>
");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n        $(\'#message-box\').hide();\r\n        let fileId;\r\n        let buttonId;\r\n        let fileName;\r\n        let fileViewModel;\r\n        let zipPassword;\r\n        let username = \'");
#nullable restore
#line 48 "C:\Projects\FileArchiver\FileArchiver\FileArchiver.MVC\Views\Home\UserLoginView.cshtml"
                   Write(Model.Username);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
        $('a').click((e) => {
            buttonId = e.target.id;
            fileId = 'file_' + buttonId;
            fileName = $(`#${fileId}`).text();
            if (fileName === """" || fileName === null)
                return;
            $.ajax({
                headers: {
                    'Content-Type': 'application/json'
                },
                dataType: 'json',
                async: false,
                type: 'GET',
                url: 'http://localhost:58506/api/files/getFileByFileName/' + username + '/' + fileName,
                success: (data) => {
                    fileViewModel = data;
                },
                error: function () {
                    console.log(""error get file"");
                }
            });

            $.ajax({
                dataType: 'json',
                type: 'POST',
                async: false,
                data: JSON.stringify(fileViewModel),
                url: 'http://localhost:31882/Home/dow");
                WriteLiteral(@"nloadFile',
                contentType: 'application/json',
                success: (data) => {
                    if (data.success) {
                        $('#message-box').show();
                        $('#error-message').text(data.message + ` at your Desktop`);
                        setTimeout(() => {
                            $('#error-message').text('');
                            $('#message-box').hide();
                        }, 3000)
                    }
                    else {
                        $('#message-box').css(""background-color"", ""red"");
                        $('#message-box').show();
                        $('#error-message').text(data.message);
                        setTimeout(() => {
                            $('#error-message').text('');
                            $('#message-box').hide();
                        }, 3000)
                    }
                },
                error: function (data) {
                    $('#message-bo");
                WriteLiteral(@"x').css(""background-color"", ""red"");
                    $('#message-box').show();
                    $('#error-message').text(data.message);
                    setTimeout(() => {
                        $('#error-message').text('');
                        $('#message-box').hide();
                    }, 3000)
                }
            });
        })
    </script>
");
            }
            );
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