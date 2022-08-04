namespace Whatsapp.Services.ViewModels
{
    public record MediaMessageVM : BaseVM
    {
        public MediaMessageVM(string to, string link) : base(to)
        {
            Link = link;
        }

        public string Link { get; }
    }
}