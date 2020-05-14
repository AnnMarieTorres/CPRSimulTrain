// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: StatAbout
// Vers: 2.1.14.8
//
// Brief [About] and [Reference] page
// .............................................................
using CPRSimulTrain.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPRSimulTrain
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BaseAbout : ContentPage
  {

    public BaseAbout()
    {
      InitializeComponent();

      Title = AppResources.BaseAboutTitle; // "About";
      BackgroundColor = Color.Black;

      abtName.Text = Alles.AppName;
      abtVersion.Text = Alles.AppVersion;
      abtRelease.Text = Alles.RelDate; ;
      abtReg.Text = Alles.RegNum;
    }

  }
}