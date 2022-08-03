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
            return $"Nome da Requisição : {RequisitionName}\nTipo da Requisição : {RequisitionType}\nId : {RequisitionId}\nData de Criação : {CreatedDate}\n {(IsClosed ? "Fechada" : "Aberta")}";
        }
    }
}