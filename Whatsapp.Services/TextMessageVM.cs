namespace Whatsapp.Services;

public record TextMessageVM(string To, string Text, bool? PreviewUrl = true);
