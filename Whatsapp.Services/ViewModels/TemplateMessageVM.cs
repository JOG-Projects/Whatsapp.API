using Whatsapp.Domain;

namespace Whatsapp.Services.ViewModels
{
    public record TemplateMessageVM(string To, string TemplateName, List<Component>? componentes = null);
}
