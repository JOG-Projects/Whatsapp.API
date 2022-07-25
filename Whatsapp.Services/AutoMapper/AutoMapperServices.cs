using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whatsapp.Domain.MediaMessages;

namespace Whatsapp.Services.AutoMapper
{
    public class AutoMapperServices : Profile
    {
        public AutoMapperServices()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<MediaMessage, ImageMessage>();
                x.CreateMap<MediaMessage, VideoMessage>();
                x.CreateMap<MediaMessage, AudioMessage>();
            });
        }
    }
}
