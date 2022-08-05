using Whatsapp.Services.ViewModels;
using Whatsapp.Services.Contracts;
using Whatsapp.Domain;
using Whatsapp.Services.NotifyClientsService;

namespace Whatsapp.Services.RequisitionService
{
    public class RequisitionServices
    {
        private const string DEFAULT_MESSAGE_TEMPLATE = "menu_requisicao";

        public readonly IMessageServices _messageServices;
        public readonly ClientRepository _clientRepository;
        private readonly NotifyClientServices _notifyServices;

        public RequisitionServices(IMessageServices messageServices, ClientRepository clientRepository, NotifyClientServices notifyServices)
        {
            _clientRepository = clientRepository;
            _notifyServices = notifyServices;
            _messageServices = messageServices;
        }

        public async Task HandleMessage(Message receivedMessage, string from)
        {
            if (receivedMessage.Context is not null && await _notifyServices.HandleMessage(receivedMessage, from))
            {
                return;
            }

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

        private async Task<SuccessResponse> SendDefaultMessage(Client client)
        {
            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, DEFAULT_MESSAGE_TEMPLATE));
        }

        private async Task<SuccessResponse> ListAllRequisitions(Client client)
        {
            var envios = client.RegisteredRequisitions.Select(r => _messageServices.SendTextMessage(new TextMessageVM(client.Number, r.ToString())));

            await Task.WhenAll(envios);

            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, DEFAULT_MESSAGE_TEMPLATE));
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

        private async Task<SuccessResponse> HandleCloseByGuid(Client client, string receivedMessage)
        {
            client.CloseRequisition(Guid.Parse(receivedMessage));

            await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Requisição fechada com sucesso!"));

            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, DEFAULT_MESSAGE_TEMPLATE));
        }

        private async Task<SuccessResponse> HandleRequisitionName(Client client, string receivedMessage)
        {
            client.AddRequisitionName(receivedMessage);

            return await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Digite o tipo da requisição:"));
        }

        private async Task HandleRequisitionType(Client client, string receivedMessage)
        {
            client.AddRequisitionType(receivedMessage);

            await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Requisição cadastrada com sucesso!!!"));

            await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, DEFAULT_MESSAGE_TEMPLATE));
        }

        #endregion

        #region Requests

        private async Task<SuccessResponse> RequestRequisitionName(Client client)
        {
            client.UpdateState(CurrentStateEnum.RequestedRequisitionName);

            return await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Digite o nome da requisição:"));
        }

        private async Task<SuccessResponse> RequestRequisitionGuid(Client client)
        {
            client.UpdateState(CurrentStateEnum.RequestedRequisitionGuid);
            return await _messageServices.SendTextMessage(new TextMessageVM(client.Number, "Informe o ID da requisição:"));
        }

        #endregion 
    }
}