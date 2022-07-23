namespace Whatsapp.Services.MediaUpload
{
    public enum ImageType
    {
        JPG, PNG
    }

    public record ImageUploadRequestVM(string Base64, ImageType Type);
}