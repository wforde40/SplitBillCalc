using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BillPay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InterestButton : ContentView
    {
        public static BindableProperty SelectedProperty =
                BindableProperty.Create("Selected", typeof(bool), typeof(InterestButton), propertyChanged: OnSelectedChanged);

        public static BindableProperty AmountProperty =
            BindableProperty.Create("Amount", typeof(string), typeof(InterestButton));

        public bool Selected
        {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); } 
        }

        public string Amount
        {
            get { return (string)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }


        SKPath CheckPath = SKPath.ParseSvgPathData(
            "M 20.00,6.00 " +
            "C 20.00, 6.00 9.00, 17.00 9.00, 17.00 " +
            "9.00, 17.00 4.00, 12.00 4.00, 12.00");


        SKPaint paint = new SKPaint()
        {
            Color = SKColor.Parse("#0EBA46"),
        };

        SKPaint LinePaint = new SKPaint()
        {
            Color = SKColor.Parse("#FAFAFA"),
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 3,
        };

        public InterestButton()
        {
            InitializeComponent();

            TipLabel.SetBinding(Label.TextProperty, new Binding("Amount", source: this));


        }

        private static void OnSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = bindable as InterestButton;
            button.SkiaCanvas.InvalidateSurface();
        }


        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            if (Selected)
            {
               
                var height = (float)canvas.DeviceClipBounds.Height;
                var width = (float)canvas.DeviceClipBounds.Width;

                var start = new SKPoint(width * .75f, 0);
                var middle = new SKPoint(width, 0);

                var triangleSideLength = SKPoint.Distance(start, middle);
                var end = new SKPoint(width, triangleSideLength);

                var points = new SKPoint[]
                {
                start,
                middle,
                end,
                };

                canvas.Clear();
                canvas.Save();

                canvas.DrawVertices(SKVertexMode.TriangleFan, points, new SKColor[] { SKColor.Parse("#0EBA46"), SKColor.Parse("#0EBA46"), SKColor.Parse("#0EBA46") }, paint);
                canvas.Save();

                canvas.Translate(width * .87f, height * .05f);
                canvas.DrawPath(CheckPath, LinePaint);     
            }
            else
            {
                canvas.Clear(SKColor.Parse("#F5F7F9"));
                
            }
        }
    }
}