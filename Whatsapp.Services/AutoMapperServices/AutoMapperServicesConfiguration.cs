using Whatsapp.Domain;
using Whatsapp.Domain.MediaMessages;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.Services.AutoMapperServices
{
    public class AutoMapperServicesConfiguration : AutoMapper.Profile
    {
        public AutoMapperServicesConfiguration()
        {
            CreateMap<MediaMessageVM, ImageMessage>().ConstructUsing(x => new ImageMessage(x.To, x.Link));
            CreateMap<MediaMessageVM, VideoMessage>().ConstructUsing(x => new VideoMessage(x.To, x.Link));
            CreateMap<MediaMessageVM, AudioMessage>().ConstructUsing(x => new AudioMessage(x.To, x.Link));
            CreateMap<MediaMessageVM, DocumentMessage>().ConstructUsing(x => new DocumentMessage(x.To, x.Link));
            CreateMap<TextMessageVM, TextMessage>().ConstructUsing(x => new TextMessage(x.To, x.Text, x.PreviewUrl ?? true));
            CreateMap<TemplateMessageVM, TemplateMessage>().ConstructUsing(x => new TemplateMessage(x.To, x.TemplateName, x.Componentes, "pt_BR"));
        }
    }
}