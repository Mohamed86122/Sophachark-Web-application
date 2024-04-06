using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;
using System.Linq;

namespace SophaTemp.Mappers
{
    public class MedicamentMapper
    {
        public static Medicament MedicamentAddVmTpMedicament(MedicamentAddVM model, AppDbContext _context)
        {
            var medicament = new Medicament
            {
                Nom = model.Nom,
                Description = model.Description,
                Image = model.Image.FileName,
                QuantiteEnAlerte = model.QuantiteEnAlerte,
                Reference = model.Reference,
            };

            if (model.SelectedCategorieIds != null && model.SelectedCategorieIds.Any())
            {
                medicament.MedicamentCategoryMedicaments = model.SelectedCategorieIds
                    .Select(categoryId => new MedicamentCategoryMedicament
                    {
                        CategoryMedicamentId = categoryId
                    })
                    .ToList();
            }

            return medicament;
        }
    }
}
