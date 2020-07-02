using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;
using SkiaSharp;

namespace BillPay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumberKeys : ContentView
    {
        public static readonly BindableProperty SendInputProperty =
            BindableProperty.Create("SendInput", typeof(Action<string>), typeof(NumberKeys));

        public static readonly BindableProperty DeleteActionProperty =
            BindableProperty.Create("DeleteAction", typeof(Action), typeof(NumberKeys));

        public Action<string> SendInput
        {
            get { return GetValue(SendInputProperty) as Action<string>; }
            set { SetValue(SendInputProperty, value); }
        }

        public Action DeleteAction       
        {
            get { return GetValue(DeleteActionProperty) as Action; }
            set { SetValue(DeleteActionProperty, value); }
        }

        private SKPath DeleteButtonPath = SKPath.ParseSvgPathData(@"M 12.22,15.54
           C 12.22,15.54 22.06,5.46 22.06,5.46M 12.22,5.46
           C 12.22,5.46 22.06,15.54 22.06,15.54M 25.02,19.57
           C 25.02,19.57 25.02,1.43 25.02,1.43M 10.25,19.57
           C 10.25,19.57 25.02,19.57 25.02,19.57M 1.39,10.50
           C 1.39,10.50 10.25,19.57 10.25,19.57M 10.25,1.43
           C 10.25,1.43 1.39,10.50 1.39,10.50M 25.02,1.43
           C 25.02,1.43 10.25,1.43 10.25,1.43");

        private SKPaint DeleteButtonBrush = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColor.Parse("#EF3649"),
            StrokeWidth = 3,
        };

        public NumberKeys()
        {
            InitializeComponent();
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var keyPressed = sender as Label;
                SendInput(keyPressed.Text);
            }
            catch
            { 
            }

            try
            {
                var pancake = sender as PancakeView;
                var keyPressed = pancake.Content as Label;
                SendInput(keyPressed.Text);
            }
            catch
            {

            }


        }

        private void TapGestureRecognizer_Plus(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Delete(object sender, EventArgs e)
        {
            DeleteAction();
        }

        private void SKCanvasView_PaintDragButton(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {

            var canvas = e.Surface.Canvas;
            var info = e.Info;

            

            canvas.Clear();
            canvas.Save();
            canvas.Translate((float)Width * .4f, (float)Height * .2f);
            canvas.Scale(2.5f);
            canvas.DrawPath(DeleteButtonPath, DeleteButtonBrush);

        }
    }
}