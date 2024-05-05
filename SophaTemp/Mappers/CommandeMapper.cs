using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SophaTemp.Mappers
{
    public class CommandeMapper
    {
        private readonly AppDbContext _context;

        public CommandeMapper(AppDbContext context)
        {
            _context = context;
        }

        public Commande CommandeFromVm(CommandeVm commandeVm)
        {
            var commande = new Commande
            {
                ClientId = commandeVm.ClientId,
                DateCommande = commandeVm.DateCommande,
                Status = commandeVm.Status,
                Medicament = _context.Medicaments.FirstOrDefault(m => m.MedicamentId == commandeVm.MedicamentId),
                lotCommande = new LotCommande
                {
                    Lots = commandeVm.LotSelections.Select(lotSel => _context.Lots.Find(lotSel.LotId)).ToList()
                }
            };

            commande.Quantite = commandeVm.LotSelections.Sum(lotSel => lotSel.Quantite);

            return commande;
        }
    }
}
