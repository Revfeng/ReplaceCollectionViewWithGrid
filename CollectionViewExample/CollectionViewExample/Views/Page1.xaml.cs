using CollectionViewExample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CollectionViewExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private Page1ViewModel viewModel;
        public Page1()
        {
            InitializeComponent();
            viewModel = new Page1ViewModel();
            BindingContext = viewModel;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}