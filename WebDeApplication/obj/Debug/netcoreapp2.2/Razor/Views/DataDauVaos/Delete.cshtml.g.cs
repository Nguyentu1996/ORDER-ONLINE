#pragma checksum "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9bdddc779eaccc4b1e441d96599dfac8285fa1d7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_DataDauVaos_Delete), @"mvc.1.0.view", @"/Views/DataDauVaos/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/DataDauVaos/Delete.cshtml", typeof(AspNetCore.Views_DataDauVaos_Delete))]
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
#line 1 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\_ViewImports.cshtml"
using WebDeApplication;

#line default
#line hidden
#line 2 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\_ViewImports.cshtml"
using WebDeApplication.Models;

#line default
#line hidden
#line 3 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#line 4 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\_ViewImports.cshtml"
using WebDeApplication.Models.ViewModel;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9bdddc779eaccc4b1e441d96599dfac8285fa1d7", @"/Views/DataDauVaos/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e4bfdd08a47741a5bbaafeb04032a3993339c1a2", @"/Views/_ViewImports.cshtml")]
    public class Views_DataDauVaos_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebDeApplication.Models.DataDauVao>
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
            BeginContext(43, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden
            BeginContext(87, 191, true);
            WriteLiteral("\r\n\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <h4>DataDauVao</h4>\r\n    <hr />\r\n  \r\n            <dl class=\"row\">\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(279, 43, false);
#line 16 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.NgayGui));

#line default
#line hidden
            EndContext();
            BeginContext(322, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(407, 39, false);
#line 19 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.NgayGui));

#line default
#line hidden
            EndContext();
            BeginContext(446, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(531, 40, false);
#line 22 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.MaSP));

#line default
#line hidden
            EndContext();
            BeginContext(571, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(656, 36, false);
#line 25 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.MaSP));

#line default
#line hidden
            EndContext();
            BeginContext(692, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(777, 47, false);
#line 28 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.LinkSanPham));

#line default
#line hidden
            EndContext();
            BeginContext(824, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(909, 43, false);
#line 31 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.LinkSanPham));

#line default
#line hidden
            EndContext();
            BeginContext(952, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(1037, 39, false);
#line 34 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.Mau));

#line default
#line hidden
            EndContext();
            BeginContext(1076, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(1161, 35, false);
#line 37 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.Mau));

#line default
#line hidden
            EndContext();
            BeginContext(1196, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(1281, 40, false);
#line 40 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.Size));

#line default
#line hidden
            EndContext();
            BeginContext(1321, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(1406, 36, false);
#line 43 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.Size));

#line default
#line hidden
            EndContext();
            BeginContext(1442, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(1527, 42, false);
#line 46 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.CanMua));

#line default
#line hidden
            EndContext();
            BeginContext(1569, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(1654, 38, false);
#line 49 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.CanMua));

#line default
#line hidden
            EndContext();
            BeginContext(1692, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(1777, 41, false);
#line 52 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.DaMua));

#line default
#line hidden
            EndContext();
            BeginContext(1818, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(1903, 37, false);
#line 55 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.DaMua));

#line default
#line hidden
            EndContext();
            BeginContext(1940, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(2025, 42, false);
#line 58 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.GiaUSD));

#line default
#line hidden
            EndContext();
            BeginContext(2067, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(2152, 38, false);
#line 61 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.GiaUSD));

#line default
#line hidden
            EndContext();
            BeginContext(2190, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(2275, 43, false);
#line 64 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.GiaSale));

#line default
#line hidden
            EndContext();
            BeginContext(2318, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(2403, 39, false);
#line 67 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.GiaSale));

#line default
#line hidden
            EndContext();
            BeginContext(2442, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(2527, 45, false);
#line 70 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.ShipOrTax));

#line default
#line hidden
            EndContext();
            BeginContext(2572, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(2657, 41, false);
#line 73 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.ShipOrTax));

#line default
#line hidden
            EndContext();
            BeginContext(2698, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(2783, 45, false);
#line 76 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.ShipOrTax));

#line default
#line hidden
            EndContext();
            BeginContext(2828, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(2913, 39, false);
#line 79 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.TongVND));

#line default
#line hidden
            EndContext();
            BeginContext(2952, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(3037, 40, false);
#line 82 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.Debt));

#line default
#line hidden
            EndContext();
            BeginContext(3077, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(3162, 36, false);
#line 85 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.Debt));

#line default
#line hidden
            EndContext();
            BeginContext(3198, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(3283, 42, false);
#line 88 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.GhiChu));

#line default
#line hidden
            EndContext();
            BeginContext(3325, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(3410, 38, false);
#line 91 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.GhiChu));

#line default
#line hidden
            EndContext();
            BeginContext(3448, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(3533, 47, false);
#line 94 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.ItemInTrack));

#line default
#line hidden
            EndContext();
            BeginContext(3580, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(3665, 43, false);
#line 97 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.ItemInTrack));

#line default
#line hidden
            EndContext();
            BeginContext(3708, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(3793, 45, false);
#line 100 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.LinkTrack));

#line default
#line hidden
            EndContext();
            BeginContext(3838, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(3923, 41, false);
#line 103 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.LinkTrack));

#line default
#line hidden
            EndContext();
            BeginContext(3964, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(4049, 44, false);
#line 106 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.ODNumber));

#line default
#line hidden
            EndContext();
            BeginContext(4093, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(4178, 40, false);
#line 109 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.ODNumber));

#line default
#line hidden
            EndContext();
            BeginContext(4218, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(4303, 43, false);
#line 112 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.Payment));

#line default
#line hidden
            EndContext();
            BeginContext(4346, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(4431, 39, false);
#line 115 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.Payment));

#line default
#line hidden
            EndContext();
            BeginContext(4470, 84, true);
            WriteLiteral("\r\n                </dd>\r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(4555, 40, false);
#line 118 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.Rate));

#line default
#line hidden
            EndContext();
            BeginContext(4595, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(4680, 36, false);
#line 121 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.Rate));

#line default
#line hidden
            EndContext();
            BeginContext(4716, 95, true);
            WriteLiteral("\r\n                </dd>\r\n         \r\n                <dt class=\"col-sm-2\">\r\n                    ");
            EndContext();
            BeginContext(4812, 43, false);
#line 125 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayNameFor(model => model.TongVND));

#line default
#line hidden
            EndContext();
            BeginContext(4855, 84, true);
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-sm-4\">\r\n                    ");
            EndContext();
            BeginContext(4940, 39, false);
#line 128 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
               Write(Html.DisplayFor(model => model.TongVND));

#line default
#line hidden
            EndContext();
            BeginContext(4979, 54, true);
            WriteLiteral("\r\n                </dd>\r\n            </dl>\r\n    \r\n    ");
            EndContext();
            BeginContext(5033, 206, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9bdddc779eaccc4b1e441d96599dfac8285fa1d723227", async() => {
                BeginContext(5059, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(5069, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "9bdddc779eaccc4b1e441d96599dfac8285fa1d723620", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 133 "C:\Users\NDM\Desktop\ORDER-ONLINE\ORDER-ONLINE\WebDeApplication\Views\DataDauVaos\Delete.cshtml"
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
                BeginContext(5105, 83, true);
                WriteLiteral("\r\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" /> |\r\n        ");
                EndContext();
                BeginContext(5188, 38, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9bdddc779eaccc4b1e441d96599dfac8285fa1d725543", async() => {
                    BeginContext(5210, 12, true);
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
                BeginContext(5226, 6, true);
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
            BeginContext(5239, 10, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebDeApplication.Models.DataDauVao> Html { get; private set; }
    }
}
#pragma warning restore 1591
