#pragma checksum "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d1c2c3b0bf792f8879299d50a1eaf3ab2da1c2ae"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\_ViewImports.cshtml"
using SoftNET_Project;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\_ViewImports.cshtml"
using SoftNET_Project.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d1c2c3b0bf792f8879299d50a1eaf3ab2da1c2ae", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"63b54c1895ec47c2e12874a3ab4da1f48615da69", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SoftNET_Project.Models.ProductSolded>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Products", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";
    int totalSales = 0;
    int totalIncome = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n");
#nullable restore
#line 14 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
 foreach (var i in Model)
{
    totalSales += i.sales;
    totalIncome += i.income;
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"card mb-4 mt-2 text-center\">\r\n    <div class=\"card-body text-center\">\r\n        <h1>SoftNET Dashboard</h1>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"card-header mb-4\">\r\n    <h4><span class=\"font-italic font-weight-bold\">");
#nullable restore
#line 28 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
                                              Write(totalSales);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> products was sold</h4>\r\n    <h4>Total Income: <span class=\"font-italic font-weight-bold\">");
#nullable restore
#line 29 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
                                                            Write(totalIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" TL</span></h4>
</div>

<div class=""card mb-4"">
    <div class=""card-header"">
        <i class=""fas fa-table me-1""></i>
        Product Reports
    </div>
    <div class=""card-body"">
        <table id=""sample_table"" class=""table"">
            <thead>
                <tr>
                    <th>
                        Product Name
                    </th>
                    <th>
                        Total Sales
                    </th>
                    <th>
                        Total Income
                    </th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 53 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d1c2c3b0bf792f8879299d50a1eaf3ab2da1c2ae6389", async() => {
#nullable restore
#line 57 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
                                                                                                        Write(Html.DisplayFor(modelItem => item.productName));

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 57 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
                                                                                WriteLiteral(item.productId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 60 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.sales));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 63 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.income));

#line default
#line hidden
#nullable disable
            WriteLiteral(" TL\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 66 "C:\Users\doguk\Desktop\CENG\Jobs\SoftNET_Project\Views\Home\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n\r\n        </table>\r\n\r\n    </div>\r\n</div>\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SoftNET_Project.Models.ProductSolded>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
