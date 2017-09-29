using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ledger.Data
{
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public ICollection<Entry> Entries { get; set; }

        [ForeignKey("UserProfile")]
        public long ProfileId { get; set; }
        public UserProfile Profile { get; set; }
    }
}