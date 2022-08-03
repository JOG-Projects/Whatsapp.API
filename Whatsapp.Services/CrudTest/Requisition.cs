using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTest
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
            return $"Nome da Requisição : {RequisitionName}\n Tipo da Requisição : {RequisitionType}\n" +
                $" Id : {RequisitionId}\n Data de Criação : {CreatedDate}\n Fechada : {IsClosed}";
        }

    }
}