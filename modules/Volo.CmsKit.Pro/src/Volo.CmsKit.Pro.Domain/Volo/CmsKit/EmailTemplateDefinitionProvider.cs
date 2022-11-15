using Volo.Abp.Emailing.Templates;
using Volo.Abp.TextTemplating;
using Volo.CmsKit.Templates;

namespace Volo.CmsKit
{
    public class EmailTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                new TemplateDefinition(
                        CmsKitEmailTemplates.ContactEmailTemplate,
                        layout: StandardEmailTemplates.Layout
                    )
                    .WithVirtualFilePath(
                        "/Volo/CmsKit/Templates/ContactEmail.tpl",
                        isInlineLocalized: true
                    )
            );
        }
    }
}