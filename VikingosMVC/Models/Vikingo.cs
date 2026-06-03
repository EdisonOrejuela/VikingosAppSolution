using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VikingosMVC.Models
{
    public class Vikingo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "Ingrese batallas ganadas")]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un numero positivo")]
        [Display(Name = "Batallas ganadas")]
        public int? BatallasGanadas { get; set; }

        [Required(ErrorMessage = "Seleccione una opcion")]
        [Display(Name = "Arma favorita")]
        public int ArmaFavoritaId { get; set; }

        [Required(ErrorMessage = "Seleccione una opcion")]
        [Display(Name = "Nivel honor")]
        public int NivelHonorId { get; set; }

        [Required(ErrorMessage = "Seleccione una opcion")]
        [Display(Name = "Muerte gloriosa")]
        public int MuerteGloriosaId { get; set; }

        public IEnumerable<SelectListItem> ListaArmas { get; set; }
        public IEnumerable<SelectListItem> ListaNivelHonor { get; set; }
        public IEnumerable<SelectListItem> ListaMuerteGloriosa { get; set; }


    }
}