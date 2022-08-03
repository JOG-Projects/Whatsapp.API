namespace Whatsapp.Services.RequisitionService
{
    public class Requisition
    {
        public string RequisitionName { get; set; }

        public string RequisitionType { get; set; }

        public Guid RequisitionId { get; set; } = Guid.NewGuid();

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsClosed { get; set; } = false;

        public void CloseRequisition()
        {
            IsClosed = true;
        }

        public override string ToString()
        {
            return $" Nome da Requisição : {RequisitionName}\n Tipo da Requisição : {RequisitionType}\n" +
                $" Id : {RequisitionId}\n Data de Criação : {CreatedDate}\n Fechada : {IsClosed}";
        }
    }
}