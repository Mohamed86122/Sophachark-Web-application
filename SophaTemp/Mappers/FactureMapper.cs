using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class FactureMapper
    {
        public Facture FactureVmFacture(FactureVm model)
        {
            return new Facture
            {
                FactureId = model.FactureId,
                Numero = model.Numero,
                Montant = model.Montant,
                DateFacturation = model.DateFacturation,
                CommandeId = model.CommandeId,

            };

        }
    }
}
