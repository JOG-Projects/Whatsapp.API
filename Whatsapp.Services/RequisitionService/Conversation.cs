namespace Whatsapp.Services.RequisitionService
{
    public class Conversation
    {
        public Requisition? CurrentRequisition { get; set; }
        public CurrentStateEnum CurrentState { get; set; } = CurrentStateEnum.InitiatedConversation;
    }
}
