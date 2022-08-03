namespace Whatsapp.Services.RequisitionService
{
    public class Client
    {
        public Client(string number)
        {
            Number = number;
            RegisteredRequisitions = new List<Requisition>();
            Conversation = new();
        }

        public string Number { get; }

        public Conversation Conversation { get; set; }

        public List<Requisition> RegisteredRequisitions { get; }

        public void CloseRequisition(Guid requisitionId)
        {
            Conversation.CurrentState = CurrentStateEnum.InitiatedConversation;

            var req = RegisteredRequisitions.First(req => req.RequisitionId == requisitionId);

            req.CloseRequisition();
        }

        public string ListRequisitions()
        {
            return string.Join("\n", RegisteredRequisitions);
        }
        public void UpdateState(CurrentStateEnum newState)
        {
            Conversation.CurrentState = newState;
        }

        public void AddRequisitionType(string requisitionType)
        {
            Conversation.CurrentState = CurrentStateEnum.InitiatedConversation;

            Conversation.CurrentRequisition!.RequisitionType = requisitionType;

            RegisteredRequisitions.Add(Conversation.CurrentRequisition);

            Conversation.CurrentRequisition = null;
        }

        public void AddRequisitionName(string requisitionName)
        {
            Conversation.CurrentState = CurrentStateEnum.RequestedRequisitionType;

            Conversation.CurrentRequisition = new()
            {
                RequisitionName = requisitionName
            };
        }
    }
}