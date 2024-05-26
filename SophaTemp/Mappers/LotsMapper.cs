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
                Libelle = model.Libelle,
                Quantite = model.Quantite,
                PrixAchat = model.PrixAchat,
                PrixVente = model.PrixVente,
                DateDeProduction = model.DateDeProduction,
                DateDExpedition = model.DateDExpedition,
                MedicamentId = model.MedicamentId,
                FournisseurId = model.FournisseurId,
            };
        }

        public void UpdateLotFromVm(Lot lot, LotAddVm model)
        {
            lot.Montant = model.Montant;
            lot.Libelle = model.Libelle;
            lot.Quantite = model.Quantite;
            lot.PrixAchat = model.PrixAchat;
            lot.PrixVente = model.PrixVente;
            lot.DateDeProduction = model.DateDeProduction;
            lot.DateDExpedition = model.DateDExpedition;
            lot.MedicamentId = model.MedicamentId;
            lot.FournisseurId = model.FournisseurId;
        }

        public LotAddVm LotToAddVm(Lot lot)
        {
            return new LotAddVm
            {
                Montant = (int)lot.Montant,
                Libelle = lot.Libelle,
                Quantite = lot.Quantite,
                PrixAchat = (int)lot.PrixAchat,
                PrixVente = (int)lot.PrixVente,
                DateDeProduction = lot.DateDeProduction,
                DateDExpedition = lot.DateDExpedition,
                MedicamentId = lot.MedicamentId,
                FournisseurId = lot.FournisseurId
            };
        }
    }
}
