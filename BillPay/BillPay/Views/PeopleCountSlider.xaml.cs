
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace BillPay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PeopleCountSlider : ContentView
    {
        public static readonly BindableProperty CountProperty =
                BindableProperty.Create("Count", typeof(int), typeof(PeopleCountSlider));

        public int Count
        {
            get { return (int)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        SKPaint rect = new SKPaint()
        {
            Style = SKPaintStyle.StrokeAndFill,
            Color = SKColor.Parse("#0EBA46"),
        };

        SKPaint IconPaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#FAFAFA"),
            StrokeWidth = 3,
        };

        SKPath IconPath = SKPath.ParseSvgPathData(
            "M 16.00,3.13 " +
            "C 17.73,3.57 19.01,5.14 19.01,7.00 " +
            "19.01,8.87 17.73,10.44 16.00,10.88M 23.00,21.00 " +
            "C 23.00,21.00 23.00,19.00 23.00,19.00 " +
            "23.00,17.14 21.73,15.58 20.00,15.13M 13.00,7.00 " +
            "C 13.00,9.21 11.21,11.00 9.00,11.00 " +
            "6.79,11.00 5.00,9.21 5.00,7.00 " +
            "5.00,4.79 6.79,3.00 9.00,3.00 " +
            "11.21,3.00 13.00,7.00 13.00,7.00 Z " +
            "M 17.00,21.00 " +
            "C 17.00,21.00 17.00,19.00 17.00,19.00 " +
            "17.00,16.79 15.21,15.00 13.00,15.00 " +
            "13.00,15.00 5.00,15.00 5.00,15.00 " +
            "2.79,15.00 1.00,16.79 1.00,19.00 " +
            "1.00,19.00 1.00,21.00 1.00,21.00 ");

        SKPoint IconPosition;

        public PeopleCountSlider()
        {
            InitializeComponent();   
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            NumberSlider.TranslationX = width * .25f;
            SliderBuffer.TranslationX = width * .25f;
        }

        private int CalculateNumberOfPeople(int width, int divs, int location)
        {
            var divSize = width / divs;
            return location / divSize;
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            float canvasWidth = canvas.DeviceClipBounds.Width;
            float canvasHeight = canvas.DeviceClipBounds.Height;

            int width = (int)(DeviceDisplay.MainDisplayInfo.Density * NumberSlider.TranslationX + .5);
            var height = canvas.DeviceClipBounds.Height;

            canvas.Clear(SKColor.Parse("#EFEFEF"));

            canvas.Save();
            canvas.DrawRect(0,0, width, height, rect);
            canvas.Save();
            canvas.Translate(canvasWidth * .05f, canvasHeight * .3f);
            canvas.Scale(2f);
            canvas.DrawPath(IconPath, IconPaint);
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var minPan = SliderCanvas.Width * .25f;
            var maxPan = SliderCanvas.Width * .75f;
            var width = maxPan - minPan;

            var incomingX = NumberSlider.TranslationX + e.TotalX;
            var test = 0.0;

            switch (e.StatusType)
            {
                case GestureStatus.Running:

                    test = Math.Max(minPan, incomingX);
                    test = Math.Min(maxPan, test);

                    NumberSlider.TranslationX = test;
                    SliderBuffer.TranslationX = test;
                    Count = CalculateNumberOfPeople((int)width, 10, (int)test) - 5;
                    CountLabel.Text = Count.ToString();
                    
                    SliderCanvas.InvalidateSurface();
                   
                    break;

                case GestureStatus.Completed:
                    test = Math.Max(minPan, incomingX);
                    test = Math.Min(maxPan, test);

                    NumberSlider.TranslateTo(test, 0);
                    SliderBuffer.TranslateTo(test, 0);
                    SliderCanvas.InvalidateSurface();

                    break;
            }
        }
    }
}
