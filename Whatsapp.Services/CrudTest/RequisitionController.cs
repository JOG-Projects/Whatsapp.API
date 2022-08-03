using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTest
{
    public class RequisitionController
    {
        public static List<Requisition> Requisitions { get; set; } = new();

        public static void Add(Requisition requisition)
        {
            Requisitions.Add(requisition);
        }

        public static void CloseRequisition(Guid requisitionId)
        {
            Requisitions.Where(req => req.RequisitionId == requisitionId).First().CloseRequisition();
        }

        public static string ListRequisitions()
        {
            string _requisitions = "";


            foreach (var requisition in Requisitions)
            {
                _requisitions += $"\n{requisition}\n";
            }

            return _requisitions;
        }



    }
}
