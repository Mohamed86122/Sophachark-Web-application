using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class PasseportVm
    { 
        public string Nom { get; set; }

        public ICollection<int> SelectedpasseportIds { get; set; }
    }
}
