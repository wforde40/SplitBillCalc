using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Essentials;
using System.Diagnostics;

namespace BillPay.Views
{
    public class BillSplitView : ContentView
    {
        public static readonly BindableProperty TotalProperty =
               BindableProperty.Create("Total", typeof(decimal), typeof(BillSplitView));
        
        public decimal Total
        {
            get { return (decimal)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        public static readonly BindableProperty SplitProperty =
            BindableProperty.Create("Split", typeof(int), typeof(BillSplitView));

        public int Split
        {
            get { return (int)GetValue(SplitProperty); }
            set { SetValue(SplitProperty, value); }
        }

       
        private ScrollView SplitScroll;
        private Grid SplitGrid;
        private PancakeView Wrapper;
        private Frame InsideFrame;
        private double ViewHeight;
        private double ViewWidth;

        public BillSplitView()
        {
            SplitScroll = new ScrollView()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            SplitGrid = new Grid()
            {
                RowSpacing = 5,
                BackgroundColor = Color.FromHex("F5F7F9"),
                Padding = new Thickness(0),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition()
                    },
            };

            InsideFrame = new Frame()
            {
                Padding = new Thickness(0),
            };

            Wrapper = new PancakeView()
            {
                VerticalOptions = LayoutOptions.Fill,
                HasShadow = true,
                CornerRadius = 5,
                Content = InsideFrame
            };

            Content = Wrapper;
        }

        private void SetRowSize(int index, GridLength height)
        {
            SplitGrid.RowDefinitions[index].Height = height;
        }

        public void SplitBill()
        {
           
            SplitGrid.RowDefinitions = new RowDefinitionCollection();
            SplitGrid.Children.Clear();

            var pricePerPerson = Total / (Split == 0 ? 1 : Split);

            ViewHeight = Split < 3 ? Height / Split : Height / 3;
            ViewWidth = Width;

            var maxHeight = ViewHeight + ViewHeight * .3;
            var minHeight = (Height - maxHeight) / Split;
            
            SplitGrid.HeightRequest = ViewHeight * Split;

            for (int i = 0; i < Split; i++)
            {
                var rowDef = new RowDefinition() { Height = ViewHeight};
               
                TranslationY = -ViewHeight;

                var test = new BillSpitItem(i,
                                            pricePerPerson,
                                             minHeight,
                                             maxHeight,
                                            (heightRequest) => {
       
                                                rowDef.Height = heightRequest;
                                                SplitGrid.RowDefinitions
                                                    .Where((row) => row != rowDef)
                                                    .ToList()
                                                    .ForEach((row) => row.Height = GridLength.Star);

                                            });


                SplitGrid.RowDefinitions.Add(rowDef);
                SplitGrid.Children.Add(test, 0, i);
            }

                SplitScroll.Content = SplitGrid;
                InsideFrame.Content = SplitScroll;

        }
    }
}