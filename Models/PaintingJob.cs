using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJSrenovation_Project_Manager.Models
{
    public enum Stages { START, PREP, UNDERCOAT, FINISHCOAT, SNACK, SIGNED_OFF }
    public enum UnderCoatType { WATER_BASE, OIL_BASE, HYBRID }
    public enum FinishTypes
    {
        MATT, CHALKY, GLOSS, SILK, SATIN, EGGSHELL,
        SOFT_SHEEN
    }
    public class PaintingJob
    {
        [Key]
        [Display(Name = "Job ID")]
        [Column("ID")]
        public int PaintDecoratingJobID { get; set; }

        [Required]
        [Display(Name = "Work ID")]
        [ForeignKey("PaintingWork")]
        public string WorkID { get; set; }

        [Required]
        [Display(Name = "Painter ID")]
        [ForeignKey("Painter")]
        public string SelfEmployedPainterID { get; set; }

#nullable enable
        [Display(Name = "Under Coat Type")]
        public UnderCoatType? UnderCoatType { get; set; }
#nullable enable
        [Display(Name = " Under Coat Name")]
        [StringLength(50)]
        public string? UnderCoatName { get; set; }
#nullable enable

        [Display(Name = "Wall")]
        [StringLength(50)]
        public string? Wall { get; set; }
#nullable enable

        [Display(Name = "Ceiling")]
        [StringLength(50)]
        public string? Ceiling { get; set; }

        [Display(Name = "Door")]
        [StringLength(50)]
        public string? Door { get; set; }

        [Display(Name = "Window")]
        [StringLength(50)]
        public string? Window { get; set; }
#nullable disable

        [Required]
        public string Brand { get; set; }
#nullable enable
        [Display(Name = "colour")]
        public string? WallColourName { get; set; }
#nullable disable
        [Required]
        [Display(Name = "Colour Value")]
        public string WallColourValue { get; set; }

        [Required]
        public FinishTypes Finish { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
#nullable enable
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }
        public int? ManHours { get; set; }
        public Stages Stages { get; set; }
#nullable enable
        public string? JobImage { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ImageDate { get; set; }
#nullable disable
        public virtual PaintingWork Work { get; set; }
        public virtual Painter Painter { get; set; }
        public int WorkLevel
        {
            get
            {

                if (Stages == Stages.PREP)
                {
                    return 30;
                }
                if (Stages == Stages.UNDERCOAT)
                {
                    return 50;
                }
                if (Stages == Stages.FINISHCOAT)
                {
                    return 80;
                }
                if (Stages == Stages.SNACK)
                {
                    return 95;
                }
                if (Stages == Stages.SIGNED_OFF)
                {
                    return 100;
                }
                return 0;

            }
        }



    }
}
