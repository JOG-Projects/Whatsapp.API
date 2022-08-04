namespace Whatsapp.Services
{
    public class Change
    {
        public Value Value { get; set; }
        public string Field { get; set; }
    }

    public class Contact
    {
        public Profile Profile { get; set; }
        public string Wa_id { get; set; }
    }

    public class Entry
    {
        public string Id { get; set; }
        public List<Change> Changes { get; set; }
    }

    public class Message
    {
        public string From { get; set; }
        public string Id { get; set; }
        public string Timestamp { get; set; }
        public Text Text { get; set; }
        public string Type { get; set; }
        public Audio Audio { get; set; }
        public Sticker Sticker { get; set; }
        public Button Button { get; set; }
        public Context Context { get; set; }
        public Image Image { get; set; }
        public Video Video { get; set; }
        public Document Document { get; set; }
        public Interactive Interactive { get; set; }
    }

    public class Document
    {

    }

    public class Interactive
    {
        public Type Type { set; get; }
    }

    public class Type
    {
        public ButtonReply button_reply { get; set; }
        public ListReply list_reply { get; set; }
    }

    public class ListReply
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class ButtonReply
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class Video
    {

    }

    public class Image
    {

    }

    public class Context
    {
        public bool Forwarded { get; set; }
        public bool Frequently_forwarded { get; set; }
        public string From { set; get; }
        public string Id { set; get; }
    }

    public class Button
    {
        public string Payload { get; set; }
        public string Text { get; set; }
    }

    public class Sticker
    {

    }

    public class Audio
    {

    }

    public class Metadata
    {
        public string Display_phone_number { get; set; }
        public string Phone_number_id { get; set; }
    }

    public class Profile
    {
        public string Name { get; set; }
    }

    public class MessageReceived
    {
        public string Object { get; set; }
        public List<Entry> Entry { get; set; }
    }

    public class Text
    {
        public string Body { get; set; }
    }

    public class Value
    {
        public string Messaging_product { get; set; }
        public Metadata Metadata { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<Message> Messages { get; set; }
    }
}