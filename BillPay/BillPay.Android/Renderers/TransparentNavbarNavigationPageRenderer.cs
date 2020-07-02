using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AView = Android.Views.View;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BillPay.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using BillPay;
using BillPay.Views;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TransparentNavbarNavigationPage), typeof(TransparentNavbarNavigationPageRenderer))]
namespace BillPay.Droid.Renderers
{
    
    class TransparentNavbarNavigationPageRenderer : NavigationPageRenderer
    {
        public TransparentNavbarNavigationPageRenderer(Context context) : base(context)
        {
        
        }

        IPageController PageController => Element as IPageController;
        TransparentNavbarNavigationPage Page => Element as TransparentNavbarNavigationPage;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            Page.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            Page.IgnoreLayoutChange = false;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if (child is Android.Support.V7.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }

    }
}