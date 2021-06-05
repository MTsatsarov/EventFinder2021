#pragma checksum "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7fdd9bb470ab257de93a46dea2b21c2d897edfba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Event_EventView), @"mvc.1.0.view", @"/Views/Event/EventView.cshtml")]
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
#line 1 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\_ViewImports.cshtml"
using EventFinder2021.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\_ViewImports.cshtml"
using EventFinder2021.Web.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
using EventFinder2021.Web.ViewModels.EventViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
using EventFinder2021.Data.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7fdd9bb470ab257de93a46dea2b21c2d897edfba", @"/Views/Event/EventView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef009110671167b2a90a1bb5f2ba662c998843f8", @"/Views/_ViewImports.cshtml")]
    public class Views_Event_EventView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EventViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("table-responsive"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 10 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
  
    var currentUserId = this.UserManager.GetUserId(this.User);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7fdd9bb470ab257de93a46dea2b21c2d897edfba4529", async() => {
                WriteLiteral("\r\n    <div class=\"justify-content-xl-center\">\r\n        <h2 class=\"text-center\">");
#nullable restore
#line 22 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                           Write(Model.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h2>\r\n        <hr />\r\n        <a class=\"event-image image-lightbox \" data-imagelightbox=\"image\" title=\"The games of our grandparents in the museum\">\r\n            <img");
                BeginWriteAttribute("src", " src=\"", 818, "\"", 839, 1);
#nullable restore
#line 25 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
WriteAttributeValue("", 824, Model.ImageUrl, 824, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("title", " title=\"", 840, "\"", 859, 1);
#nullable restore
#line 25 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
WriteAttributeValue("", 848, Model.Name, 848, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"img-fluid mx-auto\">\r\n        </a>\r\n        <div>\r\n            <p>Category: ");
#nullable restore
#line 28 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                    Write(Model.Category);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n            <p>City: ");
#nullable restore
#line 29 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                Write(Model.City);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n            <p>Date: ");
#nullable restore
#line 30 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                Write(Model.Date);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n        </div>\r\n        <div id=\"contentpics\">\r\n            <h3></h3>\r\n            <div class=\"post-gallery\">\r\n                <div class=\"row\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 40 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
         if (this.SignInManager.IsSignedIn(this.User) && currentUserId != Model.CreatorId)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"            <div>
                <div id=""btnSuccess"" style=""display:inline"">
                    <button onclick=""Going()"" class=""btn-success"">Going</button>
                </div>
                <div id=""btn-danger"" style=""display:inline"">
                    <button id=""btn"" type=""submit"" class=""btn-danger"" onclick=NotGoing()>Not Going</button>
                </div>
            </div>
");
#nullable restore
#line 50 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("    </div>\r\n\r\n    <hr />\r\n    ");
#nullable restore
#line 54 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
Write(Model.Description);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
    <script>
        function Going() {


      var xhttp = new XMLHttpRequest();
            xhttp.open('POST', ""/Event/GoingToEvent"", true);
            xhttp.setRequestHeader(""Content-Type"", ""application/json"");
            xhttp.setRequestHeader(""RequestVerificationToken"", """);
#nullable restore
#line 62 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                                           Write(GetAntiXsrfRequestToken());

#line default
#line hidden
#nullable disable
                WriteLiteral(@""");
            xhttp.onreadystatechange = function() {
                     if (this.readyState == 4 && this.status == 200) {
                         // Response
                         var responseText = JSON.parse(this.responseText);
                         var count = responseText.count
                         document.getElementById(""btnSuccess"").outerHTML = count;

                     }
    };
            var data = { userId: '");
#nullable restore
#line 72 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                             Write(currentUserId);

#line default
#line hidden
#nullable disable
                WriteLiteral("\', eventId: \'");
#nullable restore
#line 72 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                                        Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"' };
            xhttp.send(JSON.stringify(data));
        }
    </script>
    <script>
        function NotGoing() {


      var xhttp = new XMLHttpRequest();
            xhttp.open('POST', ""/Event/NotGoingToEvent"", true);
            xhttp.setRequestHeader(""Content-Type"", ""application/json"");
            xhttp.setRequestHeader(""RequestVerificationToken"", """);
#nullable restore
#line 83 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                                           Write(GetAntiXsrfRequestToken());

#line default
#line hidden
#nullable disable
                WriteLiteral(@""");
            xhttp.onreadystatechange = function() {
                     if (this.readyState == 4 && this.status == 200) {
                         // Response
                         var responseText = JSON.parse(this.responseText);
                         var count = responseText.count
                         document.getElementById(""btn-danger"").innerText = count;

                     }
    };
            var data = { userId: '");
#nullable restore
#line 93 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                             Write(currentUserId);

#line default
#line hidden
#nullable disable
                WriteLiteral("\', eventId: \'");
#nullable restore
#line 93 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                                        Write(Model.Id);

#line default
#line hidden
#nullable disable
                WriteLiteral("\' };\r\n            xhttp.send(JSON.stringify(data));\r\n        }\r\n    </script>\r\n\r\n\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
#nullable restore
#line 14 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
 
    public string GetAntiXsrfRequestToken()
    {
        return Xsrf.GetAndStoreTokens(Context).RequestToken;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<ApplicationUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<ApplicationUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public Microsoft.AspNetCore.Antiforgery.IAntiforgery Xsrf { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EventViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
