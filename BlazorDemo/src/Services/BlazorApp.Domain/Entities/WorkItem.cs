using BlazorApp.Domain.Common;
using BlazorApp.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Domain.Entities
{
    public class WorkItem : EntityBase
    {
        [ForeignKey(nameof(Ticket))]
        public int TicketId { get; set; }

        public ItemType ItemType { get; set; }

        public double Rate { get; set; }

        public UnitPrice UnitPrice { get; set; }

        public double Hours { get; set; }

        public int Quantity { get; set; }

        public Ticket Ticket { get; set; }
    }
}