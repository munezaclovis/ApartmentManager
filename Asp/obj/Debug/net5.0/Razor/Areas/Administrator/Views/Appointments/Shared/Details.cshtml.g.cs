#pragma checksum "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\Appointments\Shared\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca85364d42b2ae3b1a8b77be6457d5247f9f2d78"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Administrator_Views_Appointments_Shared_Details), @"mvc.1.0.view", @"/Areas/Administrator/Views/Appointments/Shared/Details.cshtml")]
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
#line 1 "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\_ViewImports.cshtml"
using Asp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\_ViewImports.cshtml"
using Asp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca85364d42b2ae3b1a8b77be6457d5247f9f2d78", @"/Areas/Administrator/Views/Appointments/Shared/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"295d2cd633e2455ff8e9ca4d687726e87f1b2d0a", @"/Areas/Administrator/Views/_ViewImports.cshtml")]
    public class Areas_Administrator_Views_Appointments_Shared_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Appointment>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div>\r\n\t<h4>Building</h4>\r\n\t<hr />\r\n\t<dl class=\"row\">\r\n\t\t<dt class=\"col-sm-2\">\r\n\t\t<dt class=\"col-sm-2\">\r\n\t\t\t");
#nullable restore
#line 11 "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\Appointments\Shared\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t</dt>\r\n\t\t<dd class=\"col-sm-10\">\r\n\t\t\t");
#nullable restore
#line 14 "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\Appointments\Shared\Details.cshtml"
       Write(Html.DisplayFor(model => model.Date));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t</dd>\r\n\t\t");
#nullable restore
#line 16 "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\Appointments\Shared\Details.cshtml"
   Write(Html.DisplayNameFor(model => model.TenantId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t</dt>\r\n\t\t<dd class=\"col-sm-10\">\r\n\t\t\t");
#nullable restore
#line 19 "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\Appointments\Shared\Details.cshtml"
       Write(ViewBag.TenantId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t</dd>\r\n\t\t<dt class=\"col-sm-2\">\r\n\t\t\t");
#nullable restore
#line 22 "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\Appointments\Shared\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ManagerId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t</dt>\r\n\t\t<dd class=\"col-sm-10\">\r\n\t\t\t");
#nullable restore
#line 25 "C:\Users\mugar\source\repos\Asp\Asp\Areas\Administrator\Views\Appointments\Shared\Details.cshtml"
       Write(ViewBag.ManagerId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\t\t</dd>\r\n\t</dl>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Appointment> Html { get; private set; }
    }
}
#pragma warning restore 1591
