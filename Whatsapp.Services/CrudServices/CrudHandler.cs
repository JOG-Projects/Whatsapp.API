using Whatsapp.Services.ViewModels;
using Whatsapp.Services.Contracts;

namespace Whatsapp.Services.CrudServices
{
    public class CrudHandler
    {
        public readonly IMessageServices _messageServices;
        public readonly ClientServices _clientServices;

        public CrudHandler(IMessageServices messageServices, ClientServices requisitionRepository)
        {
            _clientServices = requisitionRepository;
            _messageServices = messageServices;
        }

        public async Task HandleMessage(string receivedMessage, string from)
        {
            var client = _clientServices.GetClient(from);

            switch (client.Conversation.CurrentState)
            {
                case CurrentStateEnum.InitiatedConversation:
                    await HandleMenuOptionSelected(from, receivedMessage);
                    break;
                case CurrentStateEnum.RequestedRequisitionName:
                    await HandleRequisitionName(from, receivedMessage);
                    break;
                case CurrentStateEnum.RequestedRequisitionType:
                    await HandleRequisitionType(from, receivedMessage);
                    break;
                case CurrentStateEnum.RequestedRequisitionGuid:
                    await HandleCloseByGuid(from, receivedMessage);
                    break;
                default:
                    break;
            }
        }

        private async Task<string> SendDefaultMessage(string from)
        {
            //trocar para template message padrao cadastrada no site do meta, utilizando o _messageServices.SendMessageTemplate
            return await _messageServices.SendTextMessage(new TextMessageVM(from, _defaultMessage));
        }

        private async Task<string> ListAllRequisitions(string from)
        {
            await _messageServices.SendTextMessage(new TextMessageVM(from, _clientServices.ListRequisitions(from)));

            return await _messageServices.SendTextMessage(new TextMessageVM(from, _defaultMessage));
        }

        #region State Handlers 

        private async Task HandleMenuOptionSelected(string from, string receivedMessage)
        {
            switch (receivedMessage)
            {
                case "1": await RequestRequisitionName(from); break;

                case "2": await RequestRequisitionGuid(from); break;

                case "3": await ListAllRequisitions(from); break;

                default: await SendDefaultMessage(from); break;
            }
        }

        private async Task<string> HandleCloseByGuid(string from, string receivedMessage)
        {
            _clientServices.CloseRequisition(from, Guid.Parse(receivedMessage));

            await _messageServices.SendTextMessage(new TextMessageVM(from, "Requisição fechada com sucesso!!!"));

            return await _messageServices.SendTextMessage(new TextMessageVM(from, _defaultMessage));
        }

        private async Task<string> HandleRequisitionName(string from, string receivedMessage)
        {
            _clientServices.AddRequisitionName(from, receivedMessage);

            return await _messageServices.SendTextMessage(new TextMessageVM(from, "Digite o tipo da requisição"));
        }

        private async Task<string> HandleRequisitionType(string from, string receivedMessage)
        {
            _clientServices.AddRequisitionType(from, receivedMessage);

            await _messageServices.SendTextMessage(new TextMessageVM(from, "Requisição cadastrada com sucesso!!!"));

            return await _messageServices.SendTextMessage(new TextMessageVM(from, _defaultMessage));

        }

        #endregion

        #region Requests

        private async Task<string> RequestRequisitionName(string from)
        {
            _clientServices.UpdateState(from, CurrentStateEnum.RequestedRequisitionName);

            return await _messageServices.SendTextMessage(new TextMessageVM(from, "Digite o nome da requisição"));

        }

        private async Task<string> RequestRequisitionGuid(string from)
        {
            _clientServices.UpdateState(from, CurrentStateEnum.RequestedRequisitionGuid);

            return await _messageServices.SendTextMessage(new TextMessageVM(from, "Digite o GUID da requisição que deseja fechar"));
        }
        #endregion 

        private const string _defaultMessage = "1 - Cadastrar requisição\n\n2 - Fechar requisição\n\n3 - Listar requisição";
    }
}