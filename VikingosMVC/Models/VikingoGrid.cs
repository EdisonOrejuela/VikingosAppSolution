using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VikingosMVC.Models
{
    public class VikingoGrid
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string BatallasGanadas { get; set; }

        public string ArmaFavorita { get; set; }

        public string NivelHonor { get; set; }

        public string MuerteGloriosa { get; set; }
    }
}