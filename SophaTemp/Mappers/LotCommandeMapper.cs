using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class LotCommandeMapper
    {

        public LotCommande LotaddVmCommande(LotCommandeVm model)
        {
            return new LotCommande
            {
                Frais=model.Frais,
                Quantite=model.Quantite,
                LotId=model.LotId
            };

        }
    }
}
