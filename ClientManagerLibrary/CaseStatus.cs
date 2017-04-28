namespace ClientManagerLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaseStatus")]
    public partial class CaseStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaseStatus()
        {
            Cases = new HashSet<Case>();
        }
        //[CaseStatus was returing null], so added this new constructor.
        public CaseStatus(string status)
        {
            Cases = new HashSet<Case>();
            Status = status;
            CaseStatusId = 0;
        } //

        [Key]
        [Display(Name ="Status Id")]
        public int CaseStatusId { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
    }
}
