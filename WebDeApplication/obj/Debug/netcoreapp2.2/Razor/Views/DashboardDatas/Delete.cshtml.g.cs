#pragma checksum "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9145d43ed234d467605f066c0e1618508b0b8d28"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DashboardDatas_Delete), @"mvc.1.0.view", @"/Views/DashboardDatas/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DashboardDatas/Delete.cshtml", typeof(AspNetCore.Views_DashboardDatas_Delete))]
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
#line 1 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\_ViewImports.cshtml"
using WebDeApplication;

#line default
#line hidden
#line 2 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\_ViewImports.cshtml"
using WebDeApplication.Models;

#line default
#line hidden
#line 3 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 4 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\_ViewImports.cshtml"
using WebDeApplication.Models.ViewModel;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9145d43ed234d467605f066c0e1618508b0b8d28", @"/Views/DashboardDatas/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4bfdd08a47741a5bbaafeb04032a3993339c1a2", @"/Views/_ViewImports.cshtml")]
    public class Views_DashboardDatas_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebDeApplication.Models.DashboardData>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(46, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
  
    ViewData["Title"] = "Delete";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
            BeginContext(137, 183, true);
            WriteLiteral("\r\n<h1>Delete</h1>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <h4>DashboardData</h4>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(321, 40, false);
#line 16 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Year));

#line default
#line hidden
            EndContext();
            BeginContext(361, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(425, 36, false);
#line 19 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Year));

#line default
#line hidden
            EndContext();
            BeginContext(461, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(524, 41, false);
#line 22 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Month));

#line default
#line hidden
            EndContext();
            BeginContext(565, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(629, 37, false);
#line 25 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Month));

#line default
#line hidden
            EndContext();
            BeginContext(666, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(729, 47, false);
#line 28 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.TotalProfit));

#line default
#line hidden
            EndContext();
            BeginContext(776, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(840, 43, false);
#line 31 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.TotalProfit));

#line default
#line hidden
            EndContext();
            BeginContext(883, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(946, 49, false);
#line 34 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.PercentProfit));

#line default
#line hidden
            EndContext();
            BeginContext(995, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1059, 45, false);
#line 37 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.PercentProfit));

#line default
#line hidden
            EndContext();
            BeginContext(1104, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1167, 46, false);
#line 40 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.TotalOrder));

#line default
#line hidden
            EndContext();
            BeginContext(1213, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1277, 42, false);
#line 43 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.TotalOrder));

#line default
#line hidden
            EndContext();
            BeginContext(1319, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1382, 48, false);
#line 46 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.PercentOrder));

#line default
#line hidden
            EndContext();
            BeginContext(1430, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1494, 44, false);
#line 49 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.PercentOrder));

#line default
#line hidden
            EndContext();
            BeginContext(1538, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1601, 47, false);
#line 52 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.TotalCancel));

#line default
#line hidden
            EndContext();
            BeginContext(1648, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1712, 43, false);
#line 55 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.TotalCancel));

#line default
#line hidden
            EndContext();
            BeginContext(1755, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(1818, 49, false);
#line 58 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.PercentCancel));

#line default
#line hidden
            EndContext();
            BeginContext(1867, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(1931, 45, false);
#line 61 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.PercentCancel));

#line default
#line hidden
            EndContext();
            BeginContext(1976, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(2039, 46, false);
#line 64 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.TotalDelay));

#line default
#line hidden
            EndContext();
            BeginContext(2085, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(2149, 42, false);
#line 67 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.TotalDelay));

#line default
#line hidden
            EndContext();
            BeginContext(2191, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(2254, 48, false);
#line 70 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.PercentDelay));

#line default
#line hidden
            EndContext();
            BeginContext(2302, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(2366, 44, false);
#line 73 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.PercentDelay));

#line default
#line hidden
            EndContext();
            BeginContext(2410, 62, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt class = \"col-sm-2\">\r\n            ");
            EndContext();
            BeginContext(2473, 44, false);
#line 76 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.SiteName));

#line default
#line hidden
            EndContext();
            BeginContext(2517, 63, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd class = \"col-sm-10\">\r\n            ");
            EndContext();
            BeginContext(2581, 40, false);
#line 79 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
       Write(Html.DisplayFor(model => model.SiteName));

#line default
#line hidden
            EndContext();
            BeginContext(2621, 38, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    \r\n    ");
            EndContext();
            BeginContext(2659, 206, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9145d43ed234d467605f066c0e1618508b0b8d2815320", async() => {
                BeginContext(2685, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(2695, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9145d43ed234d467605f066c0e1618508b0b8d2815713", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 84 "D:\Source_n_tu\ASP-NET\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DashboardDatas\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2731, 83, true);
                WriteLiteral("\r\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" /> |\r\n        ");
                EndContext();
                BeginContext(2814, 38, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9145d43ed234d467605f066c0e1618508b0b8d2817640", async() => {
                    BeginContext(2836, 12, true);
                    WriteLiteral("Back to List");
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2852, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2865, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebDeApplication.Models.DashboardData> Html { get; private set; }
    }
}
#pragma warning restore 1591