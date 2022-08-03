namespace Whatsapp.Services.RequisitionService
{
    public class ClientRepository
    {
        public List<Client> Clients { get; set; } = new();

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
    }
}