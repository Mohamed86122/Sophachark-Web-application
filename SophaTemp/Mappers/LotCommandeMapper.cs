using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class LotCommandeMapper
    {

        public LotCommande LotaddVmCommande(LotCommandeVm model)
        {
            var lotCommande = new LotCommande
            {
                Frais=model.Frais,
                Quantite=model.Quantite,
                Lots = model.LotId.Select(id => new Lot { LotId = id }).ToList()  // Assumant que vous avez une propriété LotId dans Lot

            };
            
            return lotCommande;
        }
    }
}
