using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class PerMapper
    {
        public Personne AddVmtoPerson (PerViewModel model )
        {
            return new Personne
            {
                nom = model.nom,
                prenom = model.prenom,
                email = model.email,
                motdepasse = model.motdepasse,
                PasseportId = model.PasseportId,
            };
        }

        public void UpdatePersonFromVm(PerViewModel model, Personne personne)
        {
            personne.nom = model.nom;
            personne.prenom = model.prenom;
            personne.email = model.email;
            personne.motdepasse = model.motdepasse;
            personne.PasseportId = model.PasseportId;
        }
    }
}
