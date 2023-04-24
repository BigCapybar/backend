namespace backend_baz_lab.Models;
public class KBSHU
{
    public double Kkal { get; set; }
    public double Proteins { get; set; }
    public double Fats { get; set; }
    public double Carbohydrates { get; set; }
    public KBSHU(double kkal, double prot, double fats, double carbon)
    {
        Kkal = kkal;
        Proteins = prot;
        Fats = fats; 
        Carbohydrates = carbon;
    }  

}
