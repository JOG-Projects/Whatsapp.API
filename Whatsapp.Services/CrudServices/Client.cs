namespace Whatsapp.Services.CrudServices
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
    }

    public class Conversation
    {
        public Requisition? CurrentRequisition { get; set; }
        public CurrentStateEnum CurrentState { get; set; } = CurrentStateEnum.InitiatedConversation;
    }
}
