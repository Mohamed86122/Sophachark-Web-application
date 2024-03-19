using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class LotsMapper
    {

        public Lot LotaddVmLot(LotAddVm model)
        {
            return new Lot
            {
                Montant = model.Montant,
                Quantite = model.Quantite,
                PrixAchat = model.PrixAchat,
                PrixVente = model.PrixVente,
                DateDeProduction = model.DateDeProduction,
                DateDExpedition = model.DateDExpedition,
                MedicamentId = model.MedicamentId,
                FournisseurId = model.FournisseurId,

            };

        }
    }
}
