using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJSrenovation_Project_Manager.Models
{
    public enum Category
    {
        WALL_CELING, WALL_CEILING_DOORS_WINDOWS, WALL_DOORS, WALL_WINDOWS,
        WINDOWS_DOORS, WINDOWS_ONLY, DOORS_ONLY, WALL_ONLY,
    }
    public enum Surface
    {
        ONE_COAT_WALL, NEW_PLASTER, PREP_LEVEL_ONE_WALL,
        PREP_LEVEL_TWO_WALL, NEW_WOOD,
        PREP_WOOD, NEW_METAL, PREP_METAL
    }
    public class PaintingWork
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Required]
        [StringLength(10)]
        [Display(Name = "Work ID")]
        public string WorkID { get; set; }
        [Display(Name = "Project ID")]
        public string ProjectID { get; set; }
        [Required]
        [Display(Name = "Property Area")]
        public string PropertyArea { get; set; }
        [Required]
        [Display(Name = "Work Area")]
        public string WorkArea { get; set; }

        [Required]
        public Surface Surface { get; set; }
        [Required]
        [Display(Name = "Expected Hours")]
        public int ExpectedHours { get; set; }
#nullable enable
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? EntryDate { get; set; }
        public string? WorkImage { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ImageDate { get; set; }
#nullable disable
        public virtual PaintingJob Job { get; set; }
        public virtual Project Project { get; set; }


        public int NumberOfCoats
        {
            get
            {
                if (Surface == Surface.NEW_METAL || Surface == Surface.ONE_COAT_WALL)
                {
                    return 1;
                }

                if (Surface == Surface.PREP_LEVEL_ONE_WALL ||
                    Surface == Surface.NEW_PLASTER ||
                    Surface == Surface.PREP_METAL ||
                    Surface == Surface.PREP_WOOD)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }

            }
        }



    }
}
