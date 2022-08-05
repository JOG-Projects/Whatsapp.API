using Whatsapp.Domain;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.NotifyClientsService;
using Whatsapp.Services.RequisitionService;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.API.Endpoints.TestEndpoints
{
    public class TestEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("sendLinkAndClientData", SendLinkAndData);
            app.MapPost("sendPropaganda", SendPropaganda);
            app.MapPost("sendNotification", NotifyClients);
        }

        private async Task<IResult> SendLinkAndData(IMessageServices messageServices, ClientRepository clientRepository, string clientNumber, string youtubeLink)
        {
            const string templateName = "test_link_data";

            var primeiraRequisicao = clientRepository.GetClient(clientNumber).RegisteredRequisitions.FirstOrDefault() ?? new Requisition { RequisitionName = "TesteNome", RequisitionType = "TesteTipo" };

            var components = new List<Component>()
            {
                new Component("body", new List<Parameter>()
                {
                    new TextParameter(primeiraRequisicao.RequisitionName.ToString()),
                }),
                new Component("button", new List<Parameter>()
                {
                    new TextParameter(youtubeLink),
                }, "url", "0"),
            };

            var vm = new TemplateMessageVM(clientNumber, templateName, components);

            return Results.Ok(await messageServices.SendTemplateMessage(vm));
        }

        private async Task SendPropaganda(IMessageServices messageServices, string clientNumber)
        {
            const string templateName = "propaganda";

            var vm = new TemplateMessageVM(clientNumber, templateName);

            await messageServices.SendTemplateMessage(vm);
        }

        private static async Task NotifyClients(NotifyClientServices notificationServices, List<ClientVM> clients)
        {
            await notificationServices.NotifyClients(clients);
        }
    }
}