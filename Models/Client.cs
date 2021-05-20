using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PJSrenovation_Project_Manager.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        [StringLength(10)] public string Title { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = " First Name not more than 50 characters")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = " First Name not more than 50 characters")]
        [Column("LastName")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = " First Name not more than 8 characters")]
        public string Postcode { get; set; }

        [Required]
        [Phone]
        [StringLength(11, ErrorMessage = "Phone Number must be 11 digits long")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = " Please Enter a valide Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; }

        public bool PropertyAddres { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;

            }
        }
        public string FullAddress
        {
            get
            {
                return AddressLine1 + ",  " + City + ", " + Postcode;
            }
        }
        public string Contact
        {
            get
            {
                return Phone + " / " + Email;
            }
        }





    }
}
