namespace Whatsapp.Services
{
    public enum ImageType
    {
        jpeg, png
    }

    public record ImageUploader(string Base64);
}