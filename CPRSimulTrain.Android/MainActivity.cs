// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: MainActivity
// Vers: 2.1.14.8
//
// The actual start of all data and control flows
// .............................................................
using CPRSimulTrain.Resx;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace CPRSimulTrain.Droid
{
  [Activity(Label = "CPR Simulation", Icon = "@mipmap/cprteach100", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);


      Xamarin.Essentials.Platform.Init(this, savedInstanceState);
      global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

      // must be in for "permission" Authority
      //grb//Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
      //
      Syncfusion.XForms.Android.PopupLayout.SfPopupLayoutRenderer.Init();
      //
      LoadApplication(new App());

    }

    // ................................................................................................
    // OnRequestPermissionsResult
    // 
    // Required by Xamarin Essentials to 'connect' to Permission
    // ................................................................................................
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
      Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

      base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    bool _isBackPressed = false;
    public override void OnBackPressed()
    {
      var app = (CPRSimulTrain.App)App.Current;
      if (app.PromptToConfirmExit)
      {
        if (app.IsToastExitConfirmation)
          ConfirmWithToast();
        else
          ConfirmWithDialog();

        return;
      }
      base.OnBackPressed();
    }

    private void ConfirmWithDialog()
    {
      using (var alert = new AlertDialog.Builder(this))
      {
        alert.SetTitle("Exit from \"CPR Simulator\"");
        alert.SetMessage("\nAre you sure you want to exit?");
        alert.SetPositiveButton("Yes", (sender, args) => { FinishAffinity(); });
        alert.SetNegativeButton("No", (sender, args) => { }); // do nothing

        var dialog = alert.Create();
        dialog.Show();
      }
      return;
    }

    private void ConfirmWithToast()
    {
      if (_isBackPressed)
      {
        FinishAffinity(); // inform Android that we are done with the activity
        return;
      }

      _isBackPressed = true;
      Toast.MakeText(this, "Press back again to exit", ToastLength.Short).Show();

      // Disable back to exit after 2 seconds.
      new Handler().PostDelayed(() => { _isBackPressed = false; }, 2000);
      return;
    }

  }
}