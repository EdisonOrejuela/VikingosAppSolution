using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VikingosAPI.Models
{
    public class Vikingo
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int BatallasGanadas { get; set; }

        [Required]
        public int ArmaFavoritaId { get; set; }

        [Required]
        public int NivelHonorId { get; set; }

        [Required]
        public int MuerteGloriosaId { get; set; }
    }
}