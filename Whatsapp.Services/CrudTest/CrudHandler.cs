using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Whatsapp.Services.ViewModels;
using Whatsapp.Services.SendMessages;
using Whatsapp.Services.Contracts;

namespace CrudTest
{
    public class CrudHandler
    {
        public static IMessageServices MessageServices { get; set; }

        public static CurrentStateEnum CurrentState { get; set; }

        public static Requisition RequisitionObject { get; set; }

        public static async Task Handle(string receivedMessage, string from, IMessageServices messageServices)
        {
            MessageServices = messageServices;

            switch (CurrentState)
            {
                case CurrentStateEnum.DefaultMessage:
                    HandleDefaultState(receivedMessage, from);
                    break;
                case CurrentStateEnum.RequestedRequisitionName:
                    await HandleRequisitionName(receivedMessage, from);
                    break;
                case CurrentStateEnum.RequestedRequisitionType:
                    await HandleRequisitionType(receivedMessage, from);
                    break;
                case CurrentStateEnum.RequestedRequisitionGuid:
                    await HandleCloseByGuid(receivedMessage, from);
                    break;
                default:
                    break;
            }

        }

        private static async Task<string> SendDefaultMessage(string from)
        {
            return await MessageServices.SendTextMessage(new TextMessageVM(from, _defaultMessage));
        }

        private static async Task<string> ListAllRequisitions(string from)
        {
            await MessageServices.SendTextMessage(new TextMessageVM(from, RequisitionController.ListRequisitions()));

            return await MessageServices.SendTextMessage(new TextMessageVM(from, _defaultMessage));
        }

        #region State Handlers 

        private static async void HandleDefaultState(string receivedMessage, string from)
        {
            switch (receivedMessage)
            {
                case "1": await RequestRequisitionName(from); break;

                case "2": await RequestRequisitionGuid(from); break;

                case "3": await ListAllRequisitions(from); break;

                default: await SendDefaultMessage(from); break;
            }
        }

        private static async Task<string> HandleCloseByGuid(string receivedMessage, string from)
        {
            CurrentState = CurrentStateEnum.DefaultMessage;

            RequisitionController.CloseRequisition(Guid.Parse(receivedMessage));

            await MessageServices.SendTextMessage(new TextMessageVM(from, "Requisição fechada com sucesso!!!"));

            return await MessageServices.SendTextMessage(new TextMessageVM(from, _defaultMessage));
        }

        private static async Task<string> HandleRequisitionName(string receivedMessage, string from)
        {
            CurrentState = CurrentStateEnum.RequestedRequisitionType;

            RequisitionObject = new()
            {
                CreatedDate = DateTime.Now,
                RequisitionId = Guid.NewGuid(),
                IsClosed = false,
                RequisitionName = receivedMessage
            };

            return await MessageServices.SendTextMessage(new TextMessageVM(from, "Digite o tipo da requisição"));
        }

        private static async Task<string> HandleRequisitionType(string receivedMessage, string from)
        {
            CurrentState = CurrentStateEnum.DefaultMessage;

            RequisitionObject.RequisitionType = receivedMessage;

            RequisitionController.Add(RequisitionObject);

            await MessageServices.SendTextMessage(new TextMessageVM(from, "Requisição cadastrada com sucesso!!!"));

            return await MessageServices.SendTextMessage(new TextMessageVM(from, _defaultMessage));

        }

        #endregion

        #region Requests

        private static async Task<string> RequestRequisitionName(string from)
        {
            CurrentState = CurrentStateEnum.RequestedRequisitionName;

            return await MessageServices.SendTextMessage(new TextMessageVM(from, "Digite o nome da requisição"));

        }
        private static async Task<string> RequestRequisitionGuid(string from)
        {
            CurrentState = CurrentStateEnum.RequestedRequisitionGuid;

            return await MessageServices.SendTextMessage(new TextMessageVM(from, "Digite o GUID da requisição que deseja fechar"));
        }
        #endregion 

        private const string _defaultMessage = "1 - Cadastrar requisição\n\n2 - Fechar requisição\n\n3 - Listar requisição";
    }

    public enum CurrentStateEnum
    {
        DefaultMessage,
        RequestedRequisitionName,
        RequestedRequisitionType,
        RequestedRequisitionGuid,
    }

}




