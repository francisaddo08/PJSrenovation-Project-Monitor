using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PJSrenovation_Project_Manager.Models
{
    public class Project
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(8, ErrorMessage = "Project ID must be 8 characters only")]
        [Display(Name = "Project ID")]
        public string ProjectID { get; set; }
        [ForeignKey("Property")]
        [Display(Name = "Property ID")]
        public string PropertyID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }
#nullable enable
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ActualEndDate { get; set; }
        public string? ProjectScope { get; set; }
        public string? ProjectImage { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ImageDate { get; set; }
        public int Duration
        {
            get
            {
                TimeSpan duration = EndDate - StartDate;
                return duration.Days;
            }
        }




#nullable disable
        public virtual Property Property { get; set; }
        public virtual ICollection<PaintingWork> PaintingWorks { get; set; }
    }
}
