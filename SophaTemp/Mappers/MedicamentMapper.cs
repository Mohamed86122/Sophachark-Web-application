using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class MedicamentMapper
    {
        public static Medicament MedicamentAddVmTpMedicament(MedicamentAddVM model,AppDbContext _context)
        {
            var medicament =  new Medicament
            {
                Nom = model.Nom,
                Description = model.Description,
                Image = model.Image.FileName,
                QuantiteEnAlerte = model.QuantiteEnAlerte,
                Reference = model.Reference,
            };
            foreach (var categoryId in model.SelectedCategorieIds)
            {
                medicament.MedicamentCategoryMedicaments.Add(new MedicamentCategoryMedicament
                {
                    CategoryMedicamentId = categoryId,
                    Medicament = medicament
                });
            }
            return medicament;
        }
    }
}
