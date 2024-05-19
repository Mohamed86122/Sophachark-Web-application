namespace SophaTemp.Viewmodel
{
    public class CartLineVm
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float     PrixdeVente { get; set; }

        public int idMedicament { get; set; }

        public int Quantite { get; set; }


    }
}
