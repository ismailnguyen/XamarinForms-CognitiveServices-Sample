using Xamarin.Forms;
using XamarinForms_CognitiveServices_Sample.ViewModel;

namespace XamarinForms_CognitiveServices_Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }
    }
}
