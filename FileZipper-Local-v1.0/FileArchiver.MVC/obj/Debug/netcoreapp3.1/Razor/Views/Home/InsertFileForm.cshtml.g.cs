#pragma checksum "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\InsertFileForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2d1db4babd63e94aea99f01b06156b8ef931c461"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_InsertFileForm), @"mvc.1.0.view", @"/Views/Home/InsertFileForm.cshtml")]
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
#line 1 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\InsertFileForm.cshtml"
using FileArchiverCommon.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d1db4babd63e94aea99f01b06156b8ef931c461", @"/Views/Home/InsertFileForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"596e9572b2f80b6ff26b05bf29d56ef710b5a883", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_InsertFileForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UploadFileViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "2d1db4babd63e94aea99f01b06156b8ef931c4614384", async() => {
                WriteLiteral(@"
        <div class=""container"">
            <div class=""row"">
                <div class=""col-md-12"">
                    <h4 class=""text-center"">Fill the form to insert a file</h4>
                </div>
            </div>
            <div class=""form-group"">
                ");
#nullable restore
#line 12 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\InsertFileForm.cshtml"
           Write(Html.Label("Username", "Please select a username", new { @class = "ml-4 mb-4" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                ");
#nullable restore
#line 13 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\InsertFileForm.cshtml"
           Write(Html.DropDownListFor(x => x.Username, new SelectList(Model.Users.Select(x => x.Username)), null, new { @class = "dropdown mt-4", id = "username" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n            </div>\n            <div class=\"form-group\">\n                ");
#nullable restore
#line 16 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\InsertFileForm.cshtml"
           Write(Html.Label("Username", "Please select a document", new { @class = "ml-4 mb-4" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                ");
#nullable restore
#line 17 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\InsertFileForm.cshtml"
           Write(Html.DropDownListFor(x => x.DocumentName, new SelectList(Model.Documents.Select(x => x.DocumentName)), null, new { @class = "dropdown mt-4", id = "documentName" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n            </div>\n            <div class=\"form-group\">\n                ");
#nullable restore
#line 20 "C:\Project-FileZipper\FileArchiver-Deployed-Local\FileZipper-Local\FileArchiver.MVC\Views\Home\InsertFileForm.cshtml"
           Write(Html.TextBoxFor(x => x.File.FileData, null, htmlAttributes: new { type = "file", @class = "text-center", id = "fileId" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                <label id=""errorMessage"" style=""font-size: small; color: red;""></label>
            </div>
            <div class=""form-group mt-4"">
                <div class=""col-md-12 text-center"">
                    <button class=""btn btn-success mt-4"" type=""submit"" id=""uploadButton"">Upload file</button>
                </div>
            </div>
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
        <script>
            let regexForFile;
            let documentName;
            $('#documentName').change(() => {
                documentName = $('#documentName').val();
                $.ajax({
                    'url': 'http://localhost:8082/api/documentTypes/getMaskForDocument/' + documentName + '/',
                    'type': 'GET',
                    'data': documentName,
                    'dataType': 'text',
                    'async': false,
                    'success': function (data) {
                        regexForFile = data;
                    },
                    'error': function (request, error) {
                        alert(""Request: "" + JSON.stringify(request));
                    }
                });
            });

            $(""#fileId"").change(function () {
                let fileName = this.value.replace(/^.*[\\\/]/, '').replace(/\.[^/.]+$/, """");
                let regex = new RegExp(regexForFile);
                let isFileNameValid = regex.test(fileName)");
                WriteLiteral(@";
                let fullFileNameWithExtenstion = this.value.replace(/^.*[\\\/]/, '');
                if (!isFileNameValid) {
                    $('#errorMessage').show();
                    $('#errorMessage').text('Sorry document name not valid!');
                }
                else {
                    $('#errorMessage').hide();
                    $('#uploadButton').prop('disabled', false);
                }
                let allFilesNames;
                let username = $('#username').val();
                $.ajax({
                    'url': 'http://localhost:8082/api/files/getAllFiles/' + username + '/',
                    'type': 'GET',
                    'data': username,
                    'dataType': 'text',
                    'async': false,
                    'success': function (response) {
                        allFilesNames = JSON.parse(response);
                        let fileWithSamename = allFilesNames.filter(file => file.fileName === fullFileNameWithExtenstion).length;
 ");
                WriteLiteral(@"                       if (fileWithSamename > 0) {
                            $('#errorMessage').show();
                            $('#errorMessage').text('A file with that name alerady exists! Please choose an other file or rename the file');
                            $('#fileId').val('');
                            $(""#uploadButton"").prop('disabled', true);
                        }
                    },
                    'error': function (request, error) {
                        console.log(allFilesNames);
                    }
                });
            });

            $('#uploadButton').click(() => {
                let fileId = $('#fileId').val();
                if (fileId === null || fileId === '') {
                    $('#errorMessage').show();
                    $('#errorMessage').text('You must choose a file!');
                }
            });
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UploadFileViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
