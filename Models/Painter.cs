using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJSrenovation_Project_Manager.Models
{
    public enum DrivingLicence { UK_FULL, EU_FULL, NONE }
    public class Painter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        [Display(Name = "Painter ID")]
        [StringLength(10, ErrorMessage = " ID must be 10 characters max ")]
        public string PainterID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First Name must be 50 charaters only")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last Name must be 50 charaters only")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Driving Licence")]
        public DrivingLicence DrivingLicence { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Year")]
        public DateTime YearOfStart { get; set; }
        public string CurrentLocation { get; set; }


        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }




    }
}
