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
                Numero = model.Numero,
                Montant = model.Montant,
                DateFacturation = model.DateFacturation,
                CommandeId = model.CommandeId
            };
        }
    }
}
