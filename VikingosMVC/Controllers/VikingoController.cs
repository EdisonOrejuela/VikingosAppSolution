using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VikingosMVC.Models;

namespace VikingosMVC.Controllers
{
    public class VikingoController : Controller
    {
        private string vikingoUrlAPI = ConfigurationManager.AppSettings["apiVikingoUrl"];

        public async Task<ActionResult> Index()
        {
            List<VikingoGrid> listGrid = new List<VikingoGrid>();
            Vikingo vikingoItems = new Vikingo();
            this.LoadDropDownItems(ref vikingoItems);
            List<Vikingo> list = new List<Vikingo>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(vikingoUrlAPI);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Vikingo>>(json);
                }
                else
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            foreach (var item in list)
            {
                var newItemGrid = new VikingoGrid
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    BatallasGanadas = item.BatallasGanadas.ToString(),
                    ArmaFavorita = vikingoItems.ListaArmas.First(x => x.Value.Equals(item.ArmaFavoritaId.ToString())).Text,
                    NivelHonor = vikingoItems.ListaNivelHonor.First(x => x.Value.Equals(item.NivelHonorId.ToString())).Text,
                    MuerteGloriosa = vikingoItems.ListaMuerteGloriosa.First(x => x.Value.Equals(item.MuerteGloriosaId.ToString())).Text
                };
                listGrid.Add(newItemGrid);
            }

            return View(listGrid);
        }

        public ActionResult AddVikingoForm()
        {
            Vikingo vikingo = new Vikingo();
            this.LoadDropDownItems(ref vikingo);
            return View("VikingoForm", vikingo);
        }

        public async Task<ActionResult> EditVikingoForm(int vikingoId)
        {
            Vikingo vikingoEdit = null;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{vikingoUrlAPI}/{vikingoId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    vikingoEdit = JsonConvert.DeserializeObject<Vikingo>(json);
                }
                else
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
            }

            this.LoadDropDownItems(ref vikingoEdit);
            return View("VikingoForm", vikingoEdit);
        }

        public async Task<ActionResult> Delete(int vikingoId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{vikingoUrlAPI}/{vikingoId}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(Vikingo updateVikingo)
        {
            if (!ModelState.IsValid)
            {
                this.LoadDropDownItems(ref updateVikingo);
                return View("VikingoForm", updateVikingo);

            }

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(updateVikingo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync($"{vikingoUrlAPI}/{updateVikingo.Id}", content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");
                else
                    return View("~/Views/Shared/Error.cshtml");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Save(Vikingo newVikingo)
        {
            if (!ModelState.IsValid)
            {
                this.LoadDropDownItems(ref newVikingo);
                return View("VikingoForm", newVikingo);
            }

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(newVikingo);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(vikingoUrlAPI, content);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                else
                    return View("~/Views/Shared/Error.cshtml");
            }
        }

        private void LoadDropDownItems(ref Vikingo vikingoItem)
        {
            vikingoItem.ListaNivelHonor = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Bajo" },
                    new SelectListItem { Value = "2", Text = "Medio" },
                    new SelectListItem { Value = "3", Text = "Alto" }
                };
            vikingoItem.ListaMuerteGloriosa = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Campo abierto" },
                    new SelectListItem { Value = "2", Text = "Uno contra uno" },
                    new SelectListItem { Value = "3", Text = "Protegiendo aldea" }
                };
            vikingoItem.ListaArmas = new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Hacha" },
                    new SelectListItem { Value = "2", Text = "Espada" },
                    new SelectListItem { Value = "3", Text = "Martillo" }
                };
        }
    }
}