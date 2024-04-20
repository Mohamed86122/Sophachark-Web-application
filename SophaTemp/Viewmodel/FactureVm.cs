namespace SophaTemp.Viewmodel
{
    public class FactureVm
    {
       
            public int FactureId { get; set; }
            public string Numero { get; set; }
            public double Montant { get; set; }
            public DateTime DateFacturation { get; set; }

            public int CommandeId { get; set; }
    }
}
