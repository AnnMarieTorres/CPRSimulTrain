// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: Disclaimer
// Vers: 2.1.14.8
//
// Legal Disclaimer, short version
// ..............................................................
using CPRSimulTrain.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPRSimulTrain
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BaseDisclaimer : ContentPage
  {

    public BaseDisclaimer()
    {
      InitializeComponent();
      Title = AppResources.BaseDisclaimerTitle; // "Legal Disclaimer";
      BackgroundColor = Color.Black;
    }

  }
}
