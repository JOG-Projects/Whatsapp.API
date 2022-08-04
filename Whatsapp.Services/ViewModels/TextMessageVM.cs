namespace Whatsapp.Services.ViewModels;

public record TextMessageVM : BaseVM
{
    public TextMessageVM(string to, string text, bool? previewUrl = true) : base(to)
    {
        Text = text;
        PreviewUrl = previewUrl;
    }

    public string Text { get; }
    public bool? PreviewUrl { get; }
}