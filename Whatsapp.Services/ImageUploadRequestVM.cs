namespace Whatsapp.Services
{
    public enum ImageType
    {
        jpeg, png
    }

    public record ImageUploadRequestVM(string Base64);
}