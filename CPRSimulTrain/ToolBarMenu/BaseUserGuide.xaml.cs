// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: BaseHowItWorks
// Vers: 2.1.14.8
//
// Brief Users "manual"
// ..............................................................
using CPRSimulTrain.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPRSimulTrain
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BaseUserGuide : ContentPage
  {

    public BaseUserGuide()
    {
      InitializeComponent();

      Title = AppResources.BaseUserGuideTitle; // "User Guide";
      BackgroundColor = Color.White;

    }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {

        }
    }
}
