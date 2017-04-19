namespace ClientManagerLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Cases = new HashSet<Case>();
        }

        public int ClientId { get; set; }

        [Required]
        [StringLength(50)]
        [System.ComponentModel.DisplayName("First Name")]
        public string FName { get; set; }

        [Required]
        [StringLength(50)]
        [System.ComponentModel.DisplayName("Last Name")]
        public string LName { get; set; }

        [System.ComponentModel.DisplayName("Birthday")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Bday { get; set; }

        [StringLength(50)]
        public string Sex { get; set; }

        [StringLength(50)]
        [System.ComponentModel.DisplayName("Phone Number")]
        public string PhoneNo { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Case> Cases { get; set; }
    }
}
