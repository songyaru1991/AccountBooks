using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountBooks.Models
{
    //[Table("ChargeModels")]
    public partial class ChargeModels
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
    }
}