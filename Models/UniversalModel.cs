namespace AllinOneStock.Models
{
    public class UniversalModel
    {
        public enum Exchange
        {
            NseCm = 1,
            Bse = 2,
            NseFO = 4,
            BseFO = 8,
            NseCD = 16,
            BseCD = 32,
            MCX = 64,
            Ncdex = 128
        }
    }
}
