using Whatsapp.Services.ViewModels;
using Whatsapp.Services.Contracts;

namespace Whatsapp.Services.RequisitionService
{
    public class RequisitionServices
    {
        public readonly IMessageServices _messageServices;
        public readonly ClientRepository _clientRepository;

        public RequisitionServices(IMessageServices messageServices, ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            _messageServices = messageServices;
        }

        public async Task HandleMessage(Message receivedMessage, string from)
        {
            var client = _clientRepository.GetClient(from);

            var message = receivedMessage?.Button?.Text ?? receivedMessage?.Text?.Body ?? throw new NotSupportedException();

            switch (client.Conversation.CurrentState)
            {
                case CurrentStateEnum.InitiatedConversation:
                    await HandleMenuOptionSelected(client, message);
                    break;
                case CurrentStateEnum.RequestedRequisitionName:
                    await HandleRequisitionName(client, message);
                    break;
                case CurrentStateEnum.RequestedRequisitionType:
                    await HandleRequisitionType(client, message);
                    break;
                case CurrentStateEnum.RequestedRequisitionGuid:
                    await HandleCloseByGuid(client, message);
                    break;
                default:
                    break;
            }
        }

        private async Task<string> SendDefaultMessage(Client client)
        {
            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _defaultMessage));
        }

        private async Task<string> ListAllRequisitions(Client client)
        {
            var envios = client.RegisteredRequisitions.Select(r => _messageServices.SendTextMessage(new TextMessageVM(client.Number, r.ToString())));

            await Task.WhenAll(envios);

            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _defaultMessage));
        }

        #region State Handlers 

        private async Task HandleMenuOptionSelected(Client client, string receivedMessage)
        {
            switch (receivedMessage)
            {
                case "Cadastrar Requisição": await RequestRequisitionName(client); break;

                case "Fechar Requisição": await RequestRequisitionGuid(client); break;

                case "Listar Requisições": await ListAllRequisitions(client); break;

                default: await SendDefaultMessage(client); break;
            }
        }

        private async Task<string> HandleCloseByGuid(Client client, string receivedMessage)
        {
            client.CloseRequisition(Guid.Parse(receivedMessage));

            await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Requisição fechada com sucesso!"));

            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _defaultMessage));
        }

        private async Task<string> HandleRequisitionName(Client client, string receivedMessage)
        {
            client.AddRequisitionName(receivedMessage);

            return await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Digite o tipo da requisição:"));
        }

        private async Task HandleRequisitionType(Client client, string receivedMessage)
        {
            client.AddRequisitionType(receivedMessage);

            await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Requisição cadastrada com sucesso!!!"));

            await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _defaultMessage));
        }

        #endregion

        #region Requests

        private async Task<string> RequestRequisitionName(Client client)
        {
            client.UpdateState(CurrentStateEnum.RequestedRequisitionName);

            return await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Digite o nome da requisição:"));
        }

        private async Task<string> RequestRequisitionGuid(Client client)
        {
            client.UpdateState(CurrentStateEnum.RequestedRequisitionGuid);

            return await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Informe o ID da requisição:"));
        }

        #endregion 

        private const string _defaultMessage = "default_message_test_01";
    }
}