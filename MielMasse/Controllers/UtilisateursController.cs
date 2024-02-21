using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MielMasse.Data;
using MielMasse.Models;
using MielMasse.Models.Domain;
using System.Net.NetworkInformation;

namespace MielMasse.Controllers
{
    public class UtilisateursController : Controller
    {
        private readonly MielMasseDbContext _context;

        public UtilisateursController(MielMasseDbContext mielMasseDbContext)
        {
            _context = mielMasseDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> IndexUtilisateurs(string SortOrder, string SearchString)
        {
            ViewData["CurrentFilter"] = SearchString;
            var utilisateurs = from u in _context.Utilisateurs
                               select u;
            if(!String.IsNullOrEmpty(SearchString))
            {
                utilisateurs = utilisateurs.Where(u => u.Nom.Contains(SearchString));
            }


            ViewData["NomSortParam"] = String.IsNullOrEmpty(SortOrder) ? "nom_sort_desc" : "";
            ViewData["GsmSortParam"] = SortOrder == "gsm_sort" ? "gsm_sort_desc" : "gsm_sort";

            switch (SortOrder)
            {
                case "gsm_sort_desc":
                    utilisateurs = utilisateurs.OrderByDescending(u => u.Gsm);
                    break;
                case "gsm_sort":
                    utilisateurs = utilisateurs.OrderBy(u => u.Gsm);
                    //ViewData["GsmSortParam"] = String.Empty;
                    break;
                case "nom_sort_desc":
                    utilisateurs = utilisateurs.OrderByDescending(u => u.Nom);
                    break;
                case "":
                default: utilisateurs = utilisateurs.OrderBy(u => u.Nom);
                    break;

            }

            return View(await utilisateurs.AsNoTracking().ToListAsync());
        }


        [HttpGet]
        public IActionResult AddUtilisateur()
        {
            ViewBag.Users = _context.Utilisateurs;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUtilisateur(AddUtilisateurViewModel addUtilisateurRequest)
        {
            ViewBag.Users = _context.Utilisateurs;
            var utilisateur = new Utilisateur()
            {
                Id = Guid.NewGuid(),
                Nom = addUtilisateurRequest.Nom,
                Gsm = addUtilisateurRequest.Gsm,
                Adresse = addUtilisateurRequest.Adresse,
                ParrainNom = addUtilisateurRequest.ParrainNom,
                NbFilleuls = addUtilisateurRequest.NbFilleuls,
                NbFilleulsUsed = addUtilisateurRequest.NbFilleulsUsed,
                Status = addUtilisateurRequest.Status,
                Preferences = addUtilisateurRequest.Preferences
            };

            

            await _context.Utilisateurs.AddAsync(utilisateur);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexUtilisateurs");
        }

        [HttpGet]
        public async Task<IActionResult> ViewUtilisateur(Guid id)
        {
            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(x => x.Id == id);

            if (utilisateur != null)
            {
                var viewModel = new UpdateUtilisateurViewModel()
                {
                    Id = utilisateur.Id,
                    Nom = utilisateur.Nom,
                    Gsm = utilisateur.Gsm,
                    Adresse = utilisateur.Adresse,
                    Parrain = utilisateur.Parrain,
                    NbFilleuls = utilisateur.NbFilleuls,
                    NbFilleulsUsed = utilisateur.NbFilleulsUsed,
                    Status = utilisateur.Status,
                    Preferences = utilisateur.Preferences
                };
                return await Task.Run(() => View("ViewUtilisateur", viewModel));
            }

            return RedirectToAction("IndexUtilisateurs");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateUtilisateurViewModel model)
        {
            var utilisateur = await _context.Utilisateurs.FindAsync(model.Id);

            if (utilisateur != null)
            {
                utilisateur.Nom = model.Nom;
                utilisateur.Gsm = model.Gsm;
                utilisateur.Adresse = model.Adresse;
                utilisateur.Parrain = model.Parrain;
                utilisateur.NbFilleuls = model.NbFilleuls;
                utilisateur.NbFilleulsUsed = model.NbFilleulsUsed;
                utilisateur.Status = model.Status;
                utilisateur.Preferences = model.Preferences;

                await _context.SaveChangesAsync();

                return RedirectToAction("IndexUtilisateurs");
            }

            return RedirectToAction("IndexUtilisateurs");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUtilisateurViewModel model)
        {
            var utilisateur = _context.Utilisateurs.Find(model.Id);

            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
                await _context.SaveChangesAsync();

                return RedirectToAction("IndexUtilisateurs");
            }

            return RedirectToAction("IndexUtilisateurs");
        }


    }
}

