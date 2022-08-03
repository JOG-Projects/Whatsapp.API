namespace Whatsapp.Services.CrudServices
{
    public class ClientServices
    {
        public List<Client> Clients { get; set; } = new();

        public void CloseRequisition(string from, Guid requisitionId)
        {
            var client = GetClient(from);

            client.Conversation.CurrentState = CurrentStateEnum.InitiatedConversation;

            var req = client.RegisteredRequisitions.First(req => req.RequisitionId == requisitionId);

            req.CloseRequisition();
        }

        public string ListRequisitions(string from)
        {
            var client = GetClient(from);

            return string.Join("\n", client.RegisteredRequisitions);
        }

        public Client GetClient(string from)
        {
            var client = Clients.FirstOrDefault(c => c.Number == from);

            if (client == null)
            {
                client = new Client(from);
                Clients.Add(client);
            }

            return client;
        }

        public void UpdateState(string from, CurrentStateEnum newState)
        {
            var client = GetClient(from);

            client.Conversation.CurrentState = newState;
        }

        public void AddRequisitionType(string from, string requisitionType)
        {
            var client = GetClient(from);

            client.Conversation.CurrentState = CurrentStateEnum.InitiatedConversation;

            client.Conversation.CurrentRequisition!.RequisitionType = requisitionType;

            client.RegisteredRequisitions.Add(client.Conversation.CurrentRequisition);

            client.Conversation.CurrentRequisition = null;
        }

        public void AddRequisitionName(string from, string requisitionName)
        {
            var client = GetClient(from);

            client.Conversation.CurrentRequisition = new()
            {
                RequisitionName = requisitionName
            };
        }
    }
}