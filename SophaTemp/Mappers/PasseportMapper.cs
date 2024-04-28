using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Mappers
{
    public class PasseportMapper
    {
        private readonly AppDbContext _context;

        public PasseportMapper(AppDbContext context)
        {
            _context = context;
        }
        public Passeport PassportAddVmmap(PasseportVm model )
        {
            var passeport = new Passeport()
            {
                Nom = model.Nom,
            };


            if (model.SelectedpasseportIds != null && model.SelectedpasseportIds.Any())
            {
                passeport.Permissions = _context.permissions
                    .Where(p => model.SelectedpasseportIds.Contains(p.PermissionId))
                    .ToList();
            }
            return passeport;
        }

    }
}
