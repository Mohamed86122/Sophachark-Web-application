using SophaTemp.Viewmodel;
using SophaTemp.Models;
namespace SophaTemp.Mappers
{
    public class PersonMapper
    {
        public Personne AddMapVM (PersonVm model)
        {
            var person = new Personne()
            {
                email = model.Email,
                motdepasse = model.MotDePasse
            };
            return person;
        }

    }
}
