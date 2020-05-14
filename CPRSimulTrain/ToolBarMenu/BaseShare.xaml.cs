// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: BaseShare
// Vers: 2.1.14.8
//
// Share your app experience
// ..............................................................
using CPRSimulTrain.Resx;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPRSimulTrain
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class BaseShare : ContentPage
  {

    public BaseShare() 
    {
      InitializeComponent();

      Title = AppResources.BaseShareTitle; // "Share this App";
      BackgroundColor = Color.Black;

      _ = ShareTest.ShareText(shareLabel.Text);

    }

    private async void SfButton_Share_Clicked(object sender, EventArgs e)
    {
      await ShareTest.ShareText(shareLabel.Text);
    }
  }

  public static class ShareTest
  {
    public static Task ShareText(string shtext)
    {
      //
      //
      if (shtext == "WD")
      {
        var aa = shtext + "-";
        return Share.RequestAsync(new ShareTextRequest
        {
          Text = AppResources.BaseShareMsg1 + Alles.DatecprKeep.ToString() + "  at: " + Alles.TimecprKeep.ToString() + AppResources.BaseShareMsg2 + Alles.PercentKeep.ToString("P") + AppResources.BaseShareMsg3
          + "[https://www.cnginternetsoftware.com/smart_phone_software ]",


        Title = AppResources.BaseShareTitle2
        });
      }
      else
      {
        var aa = shtext + "-";
        return Share.RequestAsync(new ShareTextRequest
        {
          Text = AppResources.BaseShareText,
          Title = AppResources.BaseShareTitle2
        });
      }
    }

  }
}
