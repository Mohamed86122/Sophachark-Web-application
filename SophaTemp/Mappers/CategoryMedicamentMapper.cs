using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class CategoryMedicamentMapper
    {
        public CategoryMedicament CategoryMedicamentAddMap(CategoryMedicamentVM category)
        {
            return new CategoryMedicament
            {
                Reference = category.Reference,
                Libelle = category.Libelle,
            };

        }
    }
}
