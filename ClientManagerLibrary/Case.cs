namespace ClientManagerLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Case")]
    public partial class Case
    {
        public int CaseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        public DateTime? OpenDate { get; set; }

        public int CaseStatusId { get; set; }

        public string Note { get; set; }

        public DateTime? ClosedDate { get; set; }

        public int ClientId { get; set; }

        public virtual CaseStatus CaseStatus { get; set; }

        public virtual Client Client { get; set; }
    }
}
