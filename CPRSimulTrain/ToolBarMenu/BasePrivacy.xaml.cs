// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: BasePrivacy
// Vers: 2.1.14.8
//
// Pointing to the CNG Internet Software Privacy page
// ..............................................................
using CPRSimulTrain.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace CPRSimulTrain
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BasePrivacy : ContentPage
  {

    public BasePrivacy()
    {
      InitializeComponent();

      Title = AppResources.BasePrivacyTitle; // "Privacy";
      BackgroundColor = Color.Black;
    }

  }
}