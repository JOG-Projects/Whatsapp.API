using Whatsapp.Domain;

namespace Whatsapp.Services.ViewModels
{
    public record TemplateMessageVM : BaseVM
    {
        public TemplateMessageVM(string to, string templateName, List<Component>? componentes = null) : base(to)
        {
            TemplateName = templateName;
            Componentes = componentes;
        }

        public string TemplateName { get; }
        public List<Component>? Componentes { get; }
    }
}