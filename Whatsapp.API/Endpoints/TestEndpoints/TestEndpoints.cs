using Whatsapp.Domain;
using Whatsapp.Services.Contracts;
using Whatsapp.Services.RequisitionService;
using Whatsapp.Services.ViewModels;

namespace Whatsapp.API.Endpoints.TestEndpoints
{
    public class TestEndpoints : IEndpointDefinition
    {
        public void DefineEndpoints(WebApplication app)
        {
            app.MapPost("sendLinkAndClientData", HandleLinkAndData);
            app.MapPost("propaganda", HandlePropaganda);
        }

        private async Task HandleLinkAndData(IMessageServices messageServices, ClientRepository clientRepository, string clientNumber, string youtubeLink)
        {
            const string templateName = "variables_test_01";

            var primeiraRequisicao = clientRepository.GetClient(clientNumber).RegisteredRequisitions.First();

            var components = new List<Component>()
            {
                new Component("body", new List<Parameter>()
                {
                    new Parameter(primeiraRequisicao.ToString(), youtubeLink),
                }),
                new Component("button", new List<Parameter>()
                {
                    new Parameter("text", youtubeLink),
                }, "url", "1"),
            };

            var vm = new TemplateMessageVM(clientNumber, templateName, components);

            await messageServices.SendTemplateMessage(vm);
        }

        private async Task HandlePropaganda(IMessageServices messageServices, string clientNumber)
        {
            const string templateName = "propaganda";

            var vm = new TemplateMessageVM(clientNumber, templateName);

            await messageServices.SendTemplateMessage(vm);
        }
    }
}