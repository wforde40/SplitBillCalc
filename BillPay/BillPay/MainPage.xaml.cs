using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using Xamarin.Essentials;
using System.Diagnostics;
using BillPay.Views;

namespace BillPay
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        public bool ShowKeys { get; set; } = true;
        private double SplitHeight { get; set; }

        public MainPage()
        {
            InitializeComponent();
            //BillSplit.SplitBill();

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            
            if (ShowKeys)
            {
                PeopleCountSlider.FadeTo(0, 1);
                PeopleCountSlider.InputTransparent = true;
                TipSelector.FadeTo(0, 1);
                TipSelector.InputTransparent = true;
                Keypad.FadeTo(0, 1);
                Keypad.InputTransparent = true;

                BillSplit.FadeTo(1, 1);
                
                BillSplit.InputTransparent = false;

                //if (BillSplit.TranslationY >= 0)
                //    BillSplit.TranslateTo(0, -(MainGrid.RowDefinitions[4].Height.Value / 100 * Height), 500);

                BillSplit.SplitBill();
                BillSplit.TranslateTo(0, 0, 500);

            }
            else
            {
                BillSplit.FadeTo(0, 1);
                BillSplit.InputTransparent = true;
                //BillSplit.TranslateTo(0, -BillSplit.Height, 0);

                PeopleCountSlider.FadeTo(1, 1);
                PeopleCountSlider.InputTransparent = false;
                TipSelector.FadeTo(1, 1);
                TipSelector.InputTransparent = false;
                Keypad.FadeTo(1, 1);
                Keypad.InputTransparent = false;
            }

            ShowKeys = !ShowKeys;

        }
    }
}
