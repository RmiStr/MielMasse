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
        private readonly MielMasseDbContext mielMasseDbContext;

        public UtilisateursController(MielMasseDbContext mielMasseDbContext)
        {
            this.mielMasseDbContext = mielMasseDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> IndexUtilisateurs()
        {
            var utilisateurs = await mielMasseDbContext.Utilisateurs.ToListAsync();
            return View(utilisateurs);
        }

        [HttpGet]
        public IActionResult AddUtilisateur()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUtilisateur(AddUtilisateurViewModel addUtilisateurRequest)
        {
            var utilisateur = new Utilisateur()
            {
                Id = Guid.NewGuid(),
                Nom = addUtilisateurRequest.Nom,
                Gsm = addUtilisateurRequest.Gsm,
                Adresse = addUtilisateurRequest.Adresse,
                Parrain = addUtilisateurRequest.Parrain,
                NbFilleuls = addUtilisateurRequest.NbFilleuls,
                NbFilleulsUsed = addUtilisateurRequest.NbFilleulsUsed,
                Status = addUtilisateurRequest.Status,
                Preferences = addUtilisateurRequest.Preferences
            };

            

            await mielMasseDbContext.Utilisateurs.AddAsync(utilisateur);
            await mielMasseDbContext.SaveChangesAsync();
            return RedirectToAction("IndexUtilisateurs");
        }

        [HttpGet]
        public async Task<IActionResult> ViewUtilisateur(Guid id)
        {
            var utilisateur = await mielMasseDbContext.Utilisateurs.FirstOrDefaultAsync(x => x.Id == id);

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
            var utilisateur = await mielMasseDbContext.Utilisateurs.FindAsync(model.Id);

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

                await mielMasseDbContext.SaveChangesAsync();

                return RedirectToAction("IndexUtilisateurs");
            }

            return RedirectToAction("IndexUtilisateurs");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUtilisateurViewModel model)
        {
            var utilisateur = mielMasseDbContext.Utilisateurs.Find(model.Id);

            if (utilisateur != null)
            {
                mielMasseDbContext.Utilisateurs.Remove(utilisateur);
                await mielMasseDbContext.SaveChangesAsync();

                return RedirectToAction("IndexUtilisateurs");
            }

            return RedirectToAction("IndexUtilisateurs");
        }


    }
}

