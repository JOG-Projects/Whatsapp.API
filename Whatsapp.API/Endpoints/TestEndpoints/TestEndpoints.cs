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
        }

        private async Task<IResult> HandleLinkAndData(IMessageServices messageServices, ClientRepository clientRepository, string clientNumber, string youtubeLink)
        {
            const string templateName = "test_link_data";

            var primeiraRequisicao = clientRepository.GetClient(clientNumber).RegisteredRequisitions.FirstOrDefault()?? new Requisition {RequisitionName = "TesteNome", RequisitionType = "TesteTipo"};

            var components = new List<Component>()
            {
                new Component("body", new List<Parameter>()
                {
                    new Parameter("text", primeiraRequisicao.RequisitionName.ToString()),
                }),
                new Component("button", new List<Parameter>()
                {
                    new Parameter("text", youtubeLink),
                }, "url", "0"),
            };

            var vm = new TemplateMessageVM(clientNumber, templateName, components);

            return Results.Ok(await messageServices.SendTemplateMessage(vm));
        }
    }
}