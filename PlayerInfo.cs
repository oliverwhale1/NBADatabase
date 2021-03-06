//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Basketball_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    
    public partial class PlayerInfo
    {
        public int id { get; set; }

        [StringLength(200)]
        public string Picture { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Please enter a valid name.")]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Please enter a three letter team code. Press the Team Details button below for help.")]
        public string Team { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Please enter a player position code. Press the Position Details button below for help.")]
        public string Position { get; set; }

        [Range(0, 100)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.0}")]
        public decimal Points { get; set; }
 
        [Range(0, 100)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString="{0:0.0}")]
        public decimal Assists { get; set; }

        [Range(0, 100)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString="{0:0.0}")]
        public decimal Rebounds { get; set; }
    }
}
