using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BillPay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillSpitItem : ContentView
    {
        
        public static readonly BindableProperty PersonProperty =
            BindableProperty.Create("Person", typeof(int), typeof(BillSpitItem));

        public static readonly BindableProperty AmountProperty =
            BindableProperty.Create("Amount", typeof(decimal), typeof(BillSpitItem));

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(BillSplitView));


        public static readonly BindableProperty ButtonHeightProperty =
            BindableProperty.Create("ButtonHeight", typeof(double), typeof(BillSplitView));

        public static readonly BindableProperty ButtonRectangleProperty =
            BindableProperty.Create("ButtonRectangle", typeof(Rectangle), typeof(BillSplitView));



        public int Person 
        {
            get { return (int)GetValue(PersonProperty); }
            set { SetValue(PersonProperty, value); }
        }

        public decimal Amount
        {
            get { return (decimal)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); 
            }
        }

        public double ButtonHeight
        {
            get => (double)GetValue(ButtonHeightProperty);
            set => SetValue(AmountProperty, value);
        }

        public Rectangle ButtonRectangle
        {
            get => (Rectangle)GetValue(ButtonRectangleProperty);
            set => SetValue(ButtonRectangleProperty, value);
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Image Handle;
        public ScrollView Container;
        public Action<double> ResizeContent;

        private double MinPan;
        private double MaxPan;

        public BillSpitItem()
        {
            InitializeComponent();
        }

        public BillSpitItem(int person, decimal amount, double minHeight, double maxHeight, Action<double> resize = null, ScrollView scroll = null)
        {
            InitializeComponent();
            Person = person;
            Amount = amount;
            Text = $"Person {Person}";
            personLabel.Text = Text;
            amountLabel.Text = string.Format("{0:C2}", Amount);

            MinimumHeightRequest = minHeight;
            
            MinPan = minHeight;
            MaxPan = maxHeight;
            ResizeContent = resize;

            Container = scroll;
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
           
            switch(e.StatusType)
            {

                case GestureStatus.Running:

                    var newHeightRequest = Math.Max(Math.Min(Height + (e.TotalX), MaxPan), MinPan);
                    ResizeContent(newHeightRequest);
                    break;
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Debug.Write("We Clicked It!");
        }
    }
}