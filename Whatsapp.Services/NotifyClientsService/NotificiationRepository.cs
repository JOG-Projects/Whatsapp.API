namespace Whatsapp.Services.NotifyClientsService
{
    public class NotificiationRepository
    {
        public List<Notification> Notifications { get; }

        public NotificiationRepository()
        {
            Notifications = new();
        }

        public Notification Add(List<Client> clients, string image) 
        {
            var notification = new Notification(Guid.NewGuid(), image, clients);
            Notifications.Add(notification);
            return notification;
        }
    }

    public class Notification
    {
        public Notification(Guid id, string image, List<Client> clients)
        {
            Id = id;
            Clients = clients;
            Image = image;
        }

        public Guid Id { get; }

        public List<Client> Clients { get; }

        public string Image { get; }
    }

    public class Client
    {
        public Client(string name, string number)
        {
            Name = name;
            Number = number;
        }

        public string Name { get; }
        public string Number { get; }
        public string NotificationWaId { get; set; }
        public string Reaction { get; set; }
    }
}