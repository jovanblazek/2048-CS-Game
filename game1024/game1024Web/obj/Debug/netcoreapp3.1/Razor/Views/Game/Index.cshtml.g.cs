#pragma checksum "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "00f6de67071364b893edd93df84fd62e43fbab44"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Game_Index), @"mvc.1.0.view", @"/Views/Game/Index.cshtml")]
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
#line 1 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\_ViewImports.cshtml"
using game1024Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\_ViewImports.cshtml"
using game1024Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
using game1024Core.Core;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"00f6de67071364b893edd93df84fd62e43fbab44", @"/Views/Game/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57ddab4f2247ce804b24a26728cfc81bebb621da", @"/Views/_ViewImports.cshtml")]
    public class Views_Game_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<game1024Core.Core.Game>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
  
    ViewData["Title"] = "Game";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Game 1024</h1>\r\n<div class=\"game-grid\">\r\n");
#nullable restore
#line 9 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
     for (var row = 0; row < Model.GetField().RowCount; row++)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"game-row\">\r\n");
#nullable restore
#line 12 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
         for (var column = 0; column < Model.GetField().ColumnCount; column++)
        {

            var tile = Model.GetField().GetTile(row, column);
            if (tile == null)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"tile tile-empty\"></div>\r\n");
#nullable restore
#line 19 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div");
            BeginWriteAttribute("class", " class=\"", 566, "\"", 605, 4);
            WriteAttributeValue("", 574, "tile", 574, 4, true);
            WriteAttributeValue(" ", 578, "tile-full", 579, 10, true);
            WriteAttributeValue(" ", 588, "tile-", 589, 6, true);
#nullable restore
#line 22 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
WriteAttributeValue("", 594, tile.Value, 594, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 22 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
                                                        Write(tile.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 23 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
            }

        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
#nullable restore
#line 27 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n<script>\r\n    document.onkeydown = () => {\r\n        switch (window.event.keyCode) {\r\n        case 37:\r\n            window.location.href = \"/Game/Move?dir=");
#nullable restore
#line 34 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
                                              Write(Direction.Left);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n            break;\r\n        case 38:\r\n            window.location.href = \"/Game/Move?dir=");
#nullable restore
#line 37 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
                                              Write(Direction.Up);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n            break;\r\n        case 39:\r\n            window.location.href = \"/Game/Move?dir=");
#nullable restore
#line 40 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
                                              Write(Direction.Right);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n            break;\r\n        case 40:\r\n            window.location.href = \"/Game/Move?dir=");
#nullable restore
#line 43 "F:\plocha\Random\TUKE\LS 21\NET\dotnet-1024-game\game1024\game1024Web\Views\Game\Index.cshtml"
                                              Write(Direction.Down);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n            break;\r\n        }\r\n    }\r\n</script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<game1024Core.Core.Game> Html { get; private set; }
    }
}
#pragma warning restore 1591
