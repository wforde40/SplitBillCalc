namespace BillPay.Views
{
    public class Interest
    {
        public Interest(int rate)
        {
            Rate = rate.ToString();
        }

        public string Rate
        {

            get { return Rate; }
            set { Rate = value + "%"; }
        }

    }
}