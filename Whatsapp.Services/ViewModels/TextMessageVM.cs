namespace Whatsapp.Services.ViewModels;

public record TextMessageVM(string To, string Text, bool? PreviewUrl = true);