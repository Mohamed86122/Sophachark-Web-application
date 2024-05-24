using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

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
                MedicamentId = commandeVm.MedicamentId,
                LotCommandeId = commandeVm.LotCommandeId

            };

            commande.Client = _context.clients.FirstOrDefault(c => c.ClientId == commandeVm.ClientId);
            commande.Medicament = _context.Medicaments.FirstOrDefault(m => m.MedicamentId == commandeVm.MedicamentId);
            commande.LotCommande = _context.LotCommandes.FirstOrDefault(lc => lc.LotCommandeId == commandeVm.LotCommandeId);

            return commande;
        }
    }
}
