using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ledger.Data
{
    public class Entry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public bool IsDeposit { get; set; }
        [ForeignKey("Account")]
        public long AccountId { get; set; }
        public Account Account { get; set; }
    }
}