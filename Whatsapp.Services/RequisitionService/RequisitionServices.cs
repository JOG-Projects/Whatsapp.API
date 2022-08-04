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

        public async Task HandleMessage(string receivedMessage, string from)
        {
            var client = _clientRepository.GetClient(from);

            switch (client.Conversation.CurrentState)
            {
                case CurrentStateEnum.InitiatedConversation:
                    await HandleMenuOptionSelected(client, receivedMessage);
                    break;
                case CurrentStateEnum.RequestedRequisitionName:
                    await HandleRequisitionName(client, receivedMessage);
                    break;
                case CurrentStateEnum.RequestedRequisitionType:
                    await HandleRequisitionType(client, receivedMessage);
                    break;
                case CurrentStateEnum.RequestedRequisitionGuid:
                    await HandleCloseByGuid(client, receivedMessage);
                    break;
                default:
                    break;
            }
        }

        private async Task<string> SendDefaultMessage(Client client)
        {
            //trocar para template message padrao cadastrada no site do meta, utilizando o _messageServices.SendMessageTemplate
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
                case "1": await RequestRequisitionName(client); break;

                case "2": await RequestRequisitionGuid(client); break;

                case "3": await ListAllRequisitions(client); break;

                default: await SendDefaultMessage(client); break;
            }
        }

        private async Task<string> HandleCloseByGuid(Client client, string receivedMessage)
        {
            client.CloseRequisition(Guid.Parse(receivedMessage));

            await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _requisitionClosed));

            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _defaultMessage));
        }

        private async Task<string> HandleRequisitionName(Client client, string receivedMessage)
        {
            client.AddRequisitionName(receivedMessage);

            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _getRequisitionType));
        }

        private async Task HandleRequisitionType(Client client, string receivedMessage)
        {
            client.AddRequisitionType(receivedMessage);

            await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _requisitionSuccess));

            await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _defaultMessage));
        }

        #endregion

        #region Requests

        private async Task<string> RequestRequisitionName(Client client)
        {
            client.UpdateState(CurrentStateEnum.RequestedRequisitionName);

            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _getRequisitionName));
        }

        private async Task<string> RequestRequisitionGuid(Client client)
        {
            client.UpdateState(CurrentStateEnum.RequestedRequisitionGuid);

            return await _messageServices.SendTemplateMessage(new TemplateMessageVM(client.Number, _getRequisitionGUID));
        }

        #endregion 

        private const string _defaultMessage = "default_message";
        private const string _getRequisitionName = "get_requisition_name_test_01";
        private const string _requisitionSuccess = "requisition_success_test_01";
        private const string _getRequisitionType = "get_requisition_type_test_01";
        private const string _getRequisitionGUID = "get_requisition_guid_test_01";
        private const string _requisitionClosed = "requisition_closed_test_01";
    }
}