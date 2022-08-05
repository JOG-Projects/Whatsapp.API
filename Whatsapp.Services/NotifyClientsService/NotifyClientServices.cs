using AutoMapper;
using System.Net.Http.Headers;
using Whatsapp.Domain;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.Services.NotifyClientsService
{
    public class NotifyClientServices
    {
        const string templateName = "notificar_requisicao";
        const string imageEndpoint = "https://picsum.photos/800";
        private readonly IMessageServices _messageServices;
        private readonly NotificiationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotifyClientServices(IMessageServices messageServices, NotificiationRepository notificationRepository, IMapper mapper)
        {
            _messageServices = messageServices;
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task NotifyClients(List<ClientVM> model)
        {
            var clients = _mapper.Map<List<Client>>(model);

            var image = await GetRedirectedUrl(imageEndpoint);

            var notification = _notificationRepository.Add(clients, image);

            var envios = notification.Clients.Select(c => Notify(c, notification));

            await Task.WhenAll(envios);
        }

        private async Task Notify(Client client, Notification notification)
        {
            var success = await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, templateName, new()
            {
                new Component("header", new()
                {
                    new ImageParameter(notification.Image)
                }),
                new Component("body", new()
                {
                    new TextParameter(notification.Id.ToString())
                })
            }));

            var messageId = success.Messages.First().Id;
            client.NotificationWaId = messageId;
        }

        public async Task<bool> HandleMessage(Message receivedMessage, string from)
        {
            var contextId = receivedMessage.Context.Id;
            var notification = _notificationRepository.Notifications.FirstOrDefault(n => n.Clients.Any(c => c.NotificationWaId == contextId));

            if (notification is null)
            {
                return false;
            }

            var client = notification.Clients.First(c => c.Number == from);
            client.Reaction = receivedMessage.Button.Text;

            var otherClients = notification.Clients.Where(c => c.Number != from);
            var envios = otherClients.Select(c => NotifyReaction(c, client, notification.Id));
            await Task.WhenAll(envios);

            return true;
        }

        private async Task NotifyReaction(Client client, Client clientReacted, Guid id)
        {
            var message = new TextMessageVM(client.Number, $"O {clientReacted.Name} acabou de reagir com {clientReacted.Reaction} sobre a requisição {id}");
            await _messageServices.SendTextMessage(message);
        }

        private static async Task<string> GetRedirectedUrl(string url)
        {
            var handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };

            using (var client = new HttpClient(handler))
            using (var response = await client.GetAsync(url))
            using (var content = response.Content)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Found)
                {
                    HttpResponseHeaders headers = response.Headers;
                    if (headers != null && headers.Location != null)
                    {
                        return headers.Location.AbsoluteUri;
                    }
                }
            }

            throw new Exception("nao funfou ou nao foi redirecionado");
        }
    }
}