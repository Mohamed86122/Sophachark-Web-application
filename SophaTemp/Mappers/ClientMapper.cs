using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class ClientMapper
    {
        public Client ClientVmClient(ClientVm model)
        {
            return new Client
            {
                LibellePharmacie = model.LibellePharmacie,
                Ville = model.Ville,
                Telephone = model.Telephone,
                X = model.X,
                Y = model.Y,
                Adresse = model.Adresse,
                EnGarde = model.EnGarde,
                WhishlistId=model.WhishlistId,
                nom = model.nom,
                prenom=model.prenom,
                email   =model.email,
                motdepasse=model.motdepasse,
                PasseportId=model.PasseportId


            };

        }
    }
}
