using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Models;

namespace SophaTemp.Pages
{
    public class ShopModel
    {
        public List<Medicament> Medicaments { get; set; }


        private readonly AppDbContext _context;
        public ShopModel(AppDbContext context)
        {
            _context = context;
        }
        // Logique pour récupérer les médicaments, par exemple depuis la base de données
        public void OnGet()
        {
            // Ici, vous pouvez remplir la liste des médicaments depuis votre source de données
        }

        // Méthode de démonstration pour obtenir les médicaments depuis une base de données fictive
        public async Task OnGetAsync()
        {
            // Récupérer les médicaments depuis la base de données
            Medicaments = await _context.Medicaments.ToListAsync();
        }
    }
}
