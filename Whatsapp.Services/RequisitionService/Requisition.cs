using System.Text;

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
            var sb = new StringBuilder();

            sb.AppendLine($"Nome da Requisição : {RequisitionName}");
            sb.AppendLine($"Tipo da Requisição : {RequisitionType}");
            sb.AppendLine($"Id : {RequisitionId}");
            sb.AppendLine($"Data de Criação : {CreatedDate}");
            sb.AppendLine($"{(IsClosed ? "Fechada" : "Aberta")}");

            return sb.ToString();
        }
    }
}