using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mission11.Models.ViewModels;

namespace Mission11.Infrastructure;

[HtmlTargetElement("div", Attributes = "page-model")]

public class PaginationTagHelper : TagHelper
{
    private IUrlHelperFactory urlHelperFactory;
    
    public PaginationTagHelper(IUrlHelperFactory temp)
    {
        urlHelperFactory = temp;
    }
    
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext? ViewContext { get; set; }
    public string? pageAction { get; set; }
    public PaginationInfo pageModel { get; set; }

    public bool PageClassesEnabled { get; set; } = false;
    public string PageClass { get; set; } = String.Empty;
    public string PageClassNormal { get; set; } = String.Empty;
    public string PageClassSelected { get; set; } = String.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext != null && pageModel != null)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            
            TagBuilder result = new TagBuilder("div");
            
            for (int i = 1; i <= pageModel.totalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(pageAction, new { pageNum = i });
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == pageModel.currentPage ? PageClassSelected : PageClassNormal);
                }
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
    
}
