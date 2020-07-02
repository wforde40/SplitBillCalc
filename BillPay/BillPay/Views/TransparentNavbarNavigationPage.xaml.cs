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
    public partial class TransparentNavbarNavigationPage : NavigationPage
    {
        public TransparentNavbarNavigationPage()
        {
            InitializeComponent();
        }

        public TransparentNavbarNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}