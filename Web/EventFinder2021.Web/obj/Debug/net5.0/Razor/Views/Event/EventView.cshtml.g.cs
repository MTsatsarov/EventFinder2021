#pragma checksum "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6dcf1d9b7c21b50ef84eafe334b0ef04a3a9b246"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6dcf1d9b7c21b50ef84eafe334b0ef04a3a9b246", @"/Views/Event/EventView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef009110671167b2a90a1bb5f2ba662c998843f8", @"/Views/_ViewImports.cshtml")]
    public class Views_Event_EventView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EventViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", "~/css/ComentariesCSS.css", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", "~/css/PostCommentary.css", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/EventView.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onload", new global::Microsoft.AspNetCore.Html.HtmlString("GoingNotGoing()"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("table-responsive"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LinkTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper;
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6dcf1d9b7c21b50ef84eafe334b0ef04a3a9b2466500", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6dcf1d9b7c21b50ef84eafe334b0ef04a3a9b2466762", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LinkTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.Href = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 14 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "6dcf1d9b7c21b50ef84eafe334b0ef04a3a9b2468819", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LinkTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.Href = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 15 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_LinkTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    <div class=\"justify-content-xl-center\">\r\n        <h2 class=\"text-center\">");
#nullable restore
#line 17 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                           Write(Model.Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h2>\r\n        <hr />\r\n        <div class=\"display-2\">\r\n            <a class=\"event-image image-lightbox modal-dialog-centered\" data-imagelightbox=\"image\" title=\"The games of our grandparents in the museum\">\r\n                <img");
                BeginWriteAttribute("src", " src=\"", 959, "\"", 980, 1);
#nullable restore
#line 21 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
WriteAttributeValue("", 965, Model.ImageUrl, 965, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("title", " title=\"", 981, "\"", 1000, 1);
#nullable restore
#line 21 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
WriteAttributeValue("", 989, Model.Name, 989, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"img-fluid mx-auto\">\r\n            </a>\r\n        </div>\r\n        <div>\r\n            <p class=\"justify-content-center\">Category: ");
#nullable restore
#line 25 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                                   Write(Model.Category);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n            <p class=\"justify-content-center\">City: ");
#nullable restore
#line 26 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                               Write(Model.City);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n            <p class=\"justify-content-center\">Date: ");
#nullable restore
#line 27 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                               Write(Model.Date);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</p>
        </div>
        <div class=""custom-control-inline"">
            <ul class=""starss"">
                <li class=""star-fill d-inline-block"" id=""1""><i class=""fas fa-star""></i></li>
                <li class=""star-fill d-inline-block"" id=""2""><i class=""fas fa-star""></i></li>
                <li class=""star-fill d-inline-block"" id=""3""><i class=""fas fa-star""></i></li>
                <li class=""star-fill d-inline-block"" id=""4""><i class=""fas fa-star""></i></li>
                <li class=""star-fill d-inline-block"" id=""5""><i class=""fas fa-star""></i></li>
                <li class=""d-inline-block""><span id=""averageVoteGrade"">");
#nullable restore
#line 36 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                                                  Write(Model.VotesAverageGrade);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"<span> / 5</span></span></li>
            </ul>
        </div>
        <div id=""contentpics"">
            <h3></h3>
            <div class=""post-gallery"">
                <div class=""row"">
                </div>
            </div>
        </div>
        <div>
            <div id=""btnSuccess"" style=""display:inline"">
                <button id=""goingButton"" class=""btn-success"">Going ");
#nullable restore
#line 48 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                                              Write(Model.GoingUsers);

#line default
#line hidden
#nullable disable
                WriteLiteral("</button>\r\n            </div>\r\n            <div id=\"btn-danger\" style=\"display:inline\">\r\n                <button id=\"notGoingButton\" type=\"submit\" class=\"btn-danger\">Not Going ");
#nullable restore
#line 51 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                                                                                  Write(Model.NotGoingUsers);

#line default
#line hidden
#nullable disable
                WriteLiteral(" </button>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n\r\n    <hr />\r\n    <div class=\"text-sm-left\">\r\n        <p class=\"text-break\"> ");
#nullable restore
#line 59 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
                          Write(Model.Description);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n\r\n    </div>\r\n    <hr />\r\n    <div>\r\n        <div>\r\n            <span>\r\n                <a id=\"displayComments\" class=\"btn btn-primary\">See comments</a>\r\n            </span>\r\n            <span");
                BeginWriteAttribute("class", " class=\"", 2917, "\"", 2925, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n                <a class=\"btn btn-primary\" id=\"WriteCommentary\">Write commentary</a>\r\n            </span>\r\n        </div>\r\n    </div>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6dcf1d9b7c21b50ef84eafe334b0ef04a3a9b24616637", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "id", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 13 "C:\Users\User\Desktop\SoftUniFinalProject\EventFinder2021\Web\EventFinder2021.Web\Views\Event\EventView.cshtml"
AddHtmlAttributeValue("", 426, Model.Id, 426, 9, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
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
