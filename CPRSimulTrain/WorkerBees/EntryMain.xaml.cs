// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: EntryMain
// Vers: 2.1.4
//
//  Entry
// .............................................................
using CPRSimulTrain.Resx;
using System;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace CPRSimulTrain
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class EntryMain : ContentPage
  {

    public EntryMain()
    {
      InitializeComponent();

      #region intro stuff
      this.Title = AppResources.EntryMainTitle; // "CPR - Realtime Simulation";
      BackgroundColor = Color.Black; // WhiteSmoke;
      Opacity = 0.9;

      #endregion


    }



    #region OnApearing, etc

    // =======================================================================
    // On appearing:
    //
    // =======================================================================
    protected override void OnAppearing()
    {
      base.OnAppearing();
    }


    // =======================================================================
    // On Disappearing
    //
    // =======================================================================
    protected override void OnDisappearing()
    { 
      base.OnDisappearing();
    }

    #endregion


    #region 'Local Navigation' Functions
    private async void BaseUserGuide_P1_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseUserGuide_P1());
    private async void BaseUserGuide_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseUserGuide());
    private async void BaseFeedback_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseFeedback());
    private async void BaseShare_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseShare());
    private async void BaseDisclaimer_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseDisclaimer());
    private async void BasePrivacy_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BasePrivacy());
    private async void StatAbout_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseAbout());
    private async void BaseContactUs_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseContactUs());
    #endregion


    #region Buttons

    // ======================================================================
    // Start It, CPR Simulation Training
    //
    // ======================================================================
    private void btSimul_Clicked(object sender, EventArgs e)
    {
      try
      {
        var duration = TimeSpan.FromMilliseconds(1000);
        Vibration.Vibrate(duration);
      }
      catch (FeatureNotSupportedException ex)
      {
        _ = ex.Message.ToString();
      }
      Navigation.PushAsync(new CPRMod());
    }


    // ======================================================================
    // Start It, Showing Results
    //
    // ======================================================================
    private void btResults_Clicked(object sender, EventArgs e)
    {
      try
      {
        var duration = TimeSpan.FromMilliseconds(1000);
        Vibration.Vibrate(duration);
      }
      catch (FeatureNotSupportedException ex)
      {
        _ = ex.Message.ToString();
      }
      Navigation.PushAsync(new Results());
    }

    #endregion

  }
}
