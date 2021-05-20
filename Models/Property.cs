using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJSrenovation_Project_Manager.Models
{
    public enum PropertyType { FLAT, BUNGALOW, HOUSE, MAISONETTE }
    public class Property
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [StringLength(10)]
        [Display(Name = "Property ID")]
        [Key]
        public string PropertyID { get; set; }

        [Required]
        [ForeignKey("Client")]
        public string ClintID { get; set; }
        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        public string Location { get; set; }
        public string County { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = " First Name not more than 8 characters")]
        public string Postcode { get; set; }

        public PropertyType PropertyType { get; set; }
        [Display(Name = "Size")]
        public int? NumberOfRooms { get; set; }
       
        public DateTime? EntryDate { get; set; }
#nullable disable
        public virtual Project Project { get; set; }
        public virtual Client Client { get; set; }

        public string FullAddress
        {
            get
            {
                return Address + ", " + Location + ", " + Postcode;
            }
        }
        public string Description
        {
            get
            {
                return PropertyType + " " + "," + " " + NumberOfRooms.ToString() + " " + "Bed";
            }
        }

    }
}
