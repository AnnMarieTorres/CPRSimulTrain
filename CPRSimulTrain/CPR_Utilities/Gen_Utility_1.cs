// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: Gen_Utility_1
// Vers: 2.1.14.8
//
// Low-level routines
// .............................................................
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Syncfusion.XForms.PopupLayout;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace CPRSimulTrain
{

  class Gen_Utility_1
  {


    #region Standard

    // ============================================================================
    // Voice Announcement at start of each function
    //
    //  Values:
    //   Pitch	0	 to 2.0
    //   Volume	0	 to 1.0
    //
    //   Preferences.Set("SpeechPitch", "50");
    //   Preferences.Set("SpeechVolume", "100");
    // ============================================================================
    public static async Task CCAnnounce(string anc)     
    {
      //
      // Set Volume  input 0 - 100,  output 0.0 1.0
      //
      int temp1 = Convert.ToInt32(Preferences.Get("SpeechVolume", "default_value"));    //  100
      float temp2 = temp1 / 100.0f;                                                     //  1
      string temp3 = Convert.ToString(temp2);
      float Vol1 = float.Parse(temp3, CultureInfo.InvariantCulture.NumberFormat);
      //
      // Set Pitch   input 0 100,  output 0. 2.0
      //
      int temp10 = Convert.ToInt32(Preferences.Get("SpeechPitch", "default_value"));    //  100
      float temp12 = (temp10 / 100.0f) * 2f;                                                   //  1
      string temp13 = Convert.ToString(temp12);
      float Pit1 = float.Parse(temp13, CultureInfo.InvariantCulture.NumberFormat);
      {
        if (anc.Length > 0)
        {
          var locales = await TextToSpeech.GetLocalesAsync();

          var locale = locales.FirstOrDefault();

          var settings = new SpeechOptions()
          {
            Volume = Vol1, //1.0f, // .75f,
            Pitch = Pit1,
            Locale = locale
          };

          //grb//await TextToSpeech.SpeakAsync("Volume, Pitch, Locale", settings);

        }
      }
    }


    // ====================================================================================================================================
    // Process plain sound
    //
    // Alles.BeepAllowed  true / false
    // ====================================================================================================================================
    public static void BeepOnce()
    {
      if (Alles.BeepAllowed)
      {
        var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
        player.Load("beep.mp3");
        player.Play();
      }
    }


    // ...............................................................................
    // Vibration, this long in mil seconds (1000 is 'default')
    //
    // ...............................................................................
    public static void cngVibrate(int duration)
    {//
      try
      {
        var thisLong = TimeSpan.FromMilliseconds(duration);
        Vibration.Vibrate(thisLong);
      }
      catch (FeatureNotSupportedException ex)
      {
        _ = ex.Message.ToString();
      }
      catch (Exception ex)
      {
        _ = ex.Message.ToString();
      }
    }


    // ...............................................................................
    // Wait loop, testing only
    //
    // ...............................................................................
    public static void ccWait(int iwaitthislong)
    {
      bool iB = true;
      var startTime = DateTime.Now;
      //do until true or timeout reached.
      while (iB)
      {
        if (DateTime.Now - startTime > TimeSpan.FromSeconds(Convert.ToInt32(iwaitthislong)))
          break;
      }
    }


    // ..............................................................................
    // Our web site display options
    //
    // ..............................................................................
    public static async Task websiteRequest(string siteName)
    {
      //
      switch (siteName)
      {
        case "CNGSoftware":
          await Browser.OpenAsync("https://www.cnginternetsoftware.com/smart_phone_software", BrowserLaunchMode.SystemPreferred);
          break;
        case "Blockchain Explanation":
          await Browser.OpenAsync("https://www.cnginternetsoftware.com/blockchain", BrowserLaunchMode.SystemPreferred);
          break;
        case "Purchase of advanced version":
          await Browser.OpenAsync("https://www.cnginternetsoftware.com/payment-process", BrowserLaunchMode.SystemPreferred);
          break;
        default:
          await Browser.OpenAsync("https://www.cnginternetsoftware.com/smart_phone_software", BrowserLaunchMode.SystemPreferred);
          break;

      }
    }

    #endregion


    #region Notification popup

    // =============================================================================================
    // Display notification (box)
    //
    // if 'cancel text' is null/zero, only one bottom button is shown (Acceptance)
    // Output:  PopupAccepted = true if 'Accepted'  /  false if 'Cancelled'
    // =============================================================================================
    public static async Task<bool> DisplaySfPopupAlert(string title, string message, string acceptText, string cancelText)
    {
      Label popupContent;
      Label headerContent;
      SfPopupLayout popupLayout = new SfPopupLayout();

      DataTemplate contentTemplateView = new DataTemplate(() =>
      {
        Label label = new Label();
        popupContent = label;
        popupContent.VerticalOptions = LayoutOptions.Center;
        popupContent.BackgroundColor = Color.LightGray; // Color.FromHex("#d2fffe"); // Color.LightSkyBlue;
        popupContent.HorizontalOptions = LayoutOptions.Center;
        popupContent.Text = message;
        popupContent.Padding = new Thickness(10, 0, 0, 0);
        return popupContent;
      });

      DataTemplate headerTemplateView = new DataTemplate(() =>
      {
        headerContent = new Label
        {
          Text = title,
          //headerContent.FontAttributes = FontAttributes.Bold;
          BackgroundColor = Color.White,    // FromRgb(0, 0, 225);
          FontSize = 16,
          FontFamily = "Helvetica-Bold",
          HorizontalTextAlignment = TextAlignment.Center,
          VerticalTextAlignment = TextAlignment.Center
        };
        return headerContent;
      });

      popupLayout.PopupView.ContentTemplate = contentTemplateView;
      popupLayout.PopupView.HeaderTemplate = headerTemplateView;

      popupLayout.StaysOpen = true;
      popupLayout.PopupView.ShowCloseButton = false;
      if (string.IsNullOrEmpty(cancelText))
      {
        popupLayout.PopupView.AppearanceMode = AppearanceMode.OneButton;
      }
      else
      {
        popupLayout.PopupView.AppearanceMode = AppearanceMode.TwoButton;
      }
      popupLayout.PopupView.PopupStyle = new PopupStyle()
      {
        FooterBackgroundColor = Color.White
      };
      // ..................................................................................................
      // Configure our Accept button
      popupLayout.PopupView.AcceptButtonText = acceptText;  // "Done";
      popupLayout.PopupView.AcceptCommand = new Command(() =>
      {
        Alles.PopupAccepted = true;
        popupLayout.IsOpen = false;
      });
      popupLayout.PopupView.PopupStyle.AcceptButtonTextColor = Color.Black;
      popupLayout.PopupView.PopupStyle.AcceptButtonBackgroundColor = Color.LightBlue; // Color.LightGray;

      // ..................................................................................................
      // Configure our Decline button
      popupLayout.PopupView.DeclineButtonText = cancelText;
      popupLayout.PopupView.DeclineCommand = new Command(() =>
      {
        Alles.PopupAccepted = false;
        popupLayout.IsOpen = false;
      });
      popupLayout.PopupView.PopupStyle.DeclineButtonTextColor = Color.Black;
      popupLayout.PopupView.PopupStyle.DeclineButtonBackgroundColor = Color.LightBlue; // Color.LightGray;
      popupLayout.PopupView.ShowFooter = true;

      popupLayout.PopupView.PopupStyle.CornerRadius = 50;

      // Show the popup and wait for user to close
      Alles.PopupAccepted = false;
      popupLayout.IsOpen = true;
      while (popupLayout.IsOpen)
      {
        await Task.Delay(100);
      }

      return Alles.PopupAccepted;
    }


    #endregion




  }
}
