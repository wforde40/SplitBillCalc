using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BillPay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InterestSelector : ContentView
    {

        public static readonly BindableProperty SelectedProperty =
               BindableProperty.Create("Selected", typeof(InterestButton), typeof(InterestSelector));

        public InterestButton Selected
        {
            get { return (InterestButton)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        public InterestSelector()
        {
            InitializeComponent();
            Selected = TenPercent;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Selected.Selected = false;
            Selected = sender as InterestButton;
            Selected.Selected = true;
        }
    }
}