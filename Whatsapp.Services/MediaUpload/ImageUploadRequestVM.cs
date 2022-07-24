namespace Whatsapp.Services.MediaUpload
{
    public enum ImageType
    {
        jpg, png
    }

    public record ImageUploadRequestVM(string Base64, ImageType Type);
}