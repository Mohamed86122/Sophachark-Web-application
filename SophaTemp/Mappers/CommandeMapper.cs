using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;
using System.Linq;

namespace SophaTemp.Mappers
{
    public class CommandeMapper
    {
        private readonly AppDbContext _context; // Supposons que vous injectiez le DbContext

        public CommandeMapper(AppDbContext context)
        {
            _context = context;
        }

        public Commande CommandeMapperAddVm(CommandeVm commandeVm)
        {
            var commande = new Commande
            {
                DateCommande = commandeVm.DateCommande,
                Status = "En cours",
                lotCommande = new LotCommande 
                {
                    Lots = new List<Lot>()
                }
            };

            foreach (var lotId in commandeVm.SelectedLotIds)
            {
                var lot = _context.Lots.Find(lotId);
                if (lot != null)
                {
                    commande.lotCommande.Lots.Add(lot);
                }
            }
            commande.Quantite = commande.lotCommande.Lots.Sum(l => l.Quantite);

            return commande;
        }
    }
}
