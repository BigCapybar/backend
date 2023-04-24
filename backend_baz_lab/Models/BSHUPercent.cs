namespace backend_baz_lab.Models
{
    public class BSHUPercent : KBSHU
    {
        public double PerProteins { get; set; }
        public double PerFats { get; set; }
        public double PerCarbon { get; set; }
        public BSHUPercent(double kkal, double prot, double fats, double carbon) : base(kkal, prot, fats, carbon) 
        {
            var total = prot + fats + carbon;
            PerProteins = prot / total;
            PerFats = fats / total;
            PerCarbon = carbon / total;
        }



    }
}
