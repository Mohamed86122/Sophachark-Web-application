using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class CommandeMapper
    {
        public Commande commandemapperAddVm(CommandeVm commande)
        {
            return new Commande
            {
                DateCommande = commande.DateCommande,
                Status = commande.Status,
                Quantite = commande.Quantite,
            };

        }

        
    }
}
