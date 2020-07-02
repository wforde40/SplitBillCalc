using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BillPay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillView : ContentView
    {

        public static readonly BindableProperty TotalBillProperty =
            BindableProperty.Create("TotalBill", typeof(decimal), typeof(BillView));

        public static readonly BindableProperty BillAmountProperty =
                BindableProperty.Create("BillAmount", typeof(string), typeof(BillView), propertyChanged: OnTipAmountChanged);

        public static readonly BindableProperty PeopleCountProperty =
                BindableProperty.Create("PeopleCount", typeof(string), typeof(BillView));

        public static readonly BindableProperty TipAmountProperty =
            BindableProperty.Create("TipAmount", typeof(string), typeof(BillView), propertyChanged: OnTipAmountChanged);

        public static readonly BindableProperty RecieveInputProperty =
            BindableProperty.Create("RecieveInput", typeof(Action<string>), typeof(BillView));

        public static readonly BindableProperty DeleteActionProperty =
            BindableProperty.Create("DeleteAction", typeof(Action), typeof(BillView));

        public decimal TotalBill
        {
            get { return (decimal)GetValue(TotalBillProperty); }
            set { SetValue(TotalBillProperty, value); }
        }

        public string BillAmount
        {
            get { return (string)GetValue(BillAmountProperty); }
            set { SetValue(BillAmountProperty, value); }
        }

        public string PeopleCount
        {
            get { return (string)GetValue(PeopleCountProperty); }
            set { SetValue(PeopleCountProperty, value); }
        }

        public string TipAmount
        {
            get { return (string)GetValue(TipAmountProperty); }
            set { SetValue(TipAmountProperty, value); }
        }

        public Action<string> RecieveInput
        {
            get { return GetValue(RecieveInputProperty) as Action<string>; }
            set { SetValue(RecieveInputProperty, value); }
        }

        public Action DeleteAction
        {
            get { return GetValue(DeleteActionProperty) as Action; }
            set { SetValue(DeleteActionProperty, value); }
        }

        SKPaint rect = new SKPaint()
        {
            Style = SKPaintStyle.StrokeAndFill,
            Color = SKColor.Parse("#00BA40"),

        };

        int PageHeight = (int)DeviceDisplay.MainDisplayInfo.Height;
        int PageWidth = (int)DeviceDisplay.MainDisplayInfo.Width;


        public BillView()
        {
            InitializeComponent();

            BillAmountLabel.SetBinding(Label.TextProperty, new Binding("BillAmount", source: this));
            FriendLabel.SetBinding(Label.TextProperty, new Binding("PeopleCount", source: this));
            TipsLabel.SetBinding(Label.TextProperty, new Binding("TipAmount", source: this));

            RecieveInput = RecieveInputAction;
            DeleteAction = DeleteActionDelegate;
           

        }

        private void RecieveInputAction(string input)
        {
            var amountString = BillAmount + input;
            amountString = amountString.Replace("$", "");
            amountString = amountString.Replace(".", "");

            amountString = string.Format("{0:C2}", Convert.ToDecimal(amountString) / 100);

            if (amountString.Length < 8)
                BillAmount = amountString;

        }


        private void DeleteActionDelegate ()
        {
            var numbers = BillAmount
                    .Replace("$", "")
                    .Split('.');

            var first = numbers[0].Substring(0, numbers[0].Length - 1) + ".";
            var second = numbers[0].Substring(numbers[0].Length - 1) + numbers[1].Substring(0, 1);
            var newAmount = first + second;
            newAmount = string.Format("{0:C2}", Convert.ToDecimal(newAmount));

            if (newAmount.Length< 8)
                BillAmount = newAmount;

        }

        private static void OnTipAmountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable != null)
            {
                var billView = bindable as BillView;

                if (billView.TipAmount != null)
                {
                    var tipAmount = "0." + string.Concat(billView.TipAmount.Where(c => char.IsDigit(c)));
                    var d = decimal.Parse(tipAmount);
                    var m = Convert.ToDecimal(billView.BillAmount.Replace("$", ""));
                    var e = (d * m);
                    var amount = string.Format("{0:C2}", e);

                    billView.TipAmountLabel.Text = amount;

                    var total = m + e;

                    billView.TotalBill = total;

                    var totalString = string.Format("{0:C2}", total);

                    billView.TotalAmountLabel.Text = totalString;
                }
            }
        }


        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var height = canvas.DeviceClipBounds.Height;
            var width = canvas.DeviceClipBounds.Width;

            var boxHeight = PageHeight * .2;
            var boxWidth = PageWidth * .9;

            var vertices = new SKPoint[]
            {
                new SKPoint((float)width * .33f, 0f),
                new SKPoint((float)width * .66f, (float)height),
                new SKPoint((float)width, 0f),
             };

            var vertices1 = new SKPoint[]
            {
                new SKPoint((float)width * .5f, 0f),
                new SKPoint((float)width * .85f, (float)height + (float)height * .1f),
                new SKPoint((float)width + (float)width * .25f, 0f),
            };

            var offset = SKPoint.Distance(vertices[0], vertices[2]);

            var point1 = new SKPoint((float)width * .66f, (float)height);
            var point2 = new SKPoint((float)width * .85f, (float)height + (float)height * .1f);

            var distance = SKPoint.Distance(point1, point2);

            var midPoint = new SKPoint(point1.X + distance / 2, point1.Y - point1.Y * .20f);

            var vertices2 = new SKPoint[]
            {
                new SKPoint((float)width * .5f, 0f),
                midPoint,
                new SKPoint((float)width, 0f),
            };


            var triangleFill = new SKPaint()
            {
                Style = SKPaintStyle.StrokeAndFill,
                Color = SKColor.Parse("#00BE4F"),
            };

            canvas.Clear();

            canvas.Save();
            canvas.DrawRect(0f, 0f, (float)boxWidth, (float)boxHeight, rect);
            canvas.DrawVertices(SKVertexMode.Triangles, vertices, new SKColor[] { SKColor.Parse("#00BE4F"), SKColor.Parse("#00BE4F"), SKColor.Parse("#00BE4F") }, triangleFill);
            canvas.DrawVertices(SKVertexMode.Triangles, vertices1, new SKColor[] { SKColor.Parse("#00BE4F"), SKColor.Parse("#00BE4F"), SKColor.Parse("#00BE4F") }, triangleFill);
            canvas.DrawVertices(SKVertexMode.Triangles, vertices2, new SKColor[] { SKColor.Parse("#19C75D"), SKColor.Parse("#19C75D"), SKColor.Parse("#19C75D") }, triangleFill);
            canvas.Restore();
        }
    }
}