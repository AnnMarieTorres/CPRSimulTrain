// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: App
// Vers: 2.1.4
//
// The actual start of this app.
// .............................................................
using System;
using System.IO;

using Xamarin.Essentials;
using Xamarin.Forms;



namespace CPRSimulTrain
{
  public partial class App : Application
  {



    public App()
    {
      //grb//bool iB;

      Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQxNzk1QDMxMzgyZTMxMmUzMGl1TFZpakk4VmcrSm9Eb25SQzdSNlBWaVJBclpKOG1VQWw2dElaNGNYUFE9");

      InitializeComponent();

      // 
      // sound pitch
      Preferences.Set("SpeechPitch", "50");
      // sound rate
      Preferences.Set("SpeechVolume", "100");

      var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
      Alles.ScreenWidth = mainDisplayInfo.Width;        // Width (in pixels)
      Alles.ScreenHeight = mainDisplayInfo.Height;      // Height (in pixels)

      var content = new EntryMain();
      NavigationPage navPage = new NavigationPage(content)
      {
        Title = "CPR Simul Trainer",
        BarBackgroundColor = Color.LightGreen,
        BarTextColor = Color.Black,
        BackgroundColor = Color.LightBlue,
        IsEnabled = true,
        IsVisible = true
      };
      MainPage = navPage;
      //
      //MainPage = new MainPage();

    }



    // ===============================================================================
    // Check if there is a database, create a new database if none exists
    // ===============================================================================
    public static CPRSimulTrainDB Database
    {
      get
      {
        if (Alles.database == null)
        {
          string dbtemp001 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Alles.dbname100);
          Alles.database = new CPRSimulTrainDB(dbtemp001);
        }
        return Alles.database;
      }
    }


    protected override void OnStart()
    {
      // Handle when your app starts
      //Debug.WriteLine("OnStart");

      //// always re-set when the app starts
      //// users expect this (usually)
      ////			Properties ["ResumeAt100"] = "";
      //if (Properties.ContainsKey("ResumeAt100"))
      //{
      //	var rati = Properties["ResumeAt100"].ToString();
      //	Debug.WriteLine("   rati=" + rati);
      //	if (!String.IsNullOrEmpty(rati))
      //	{
      //		Debug.WriteLine("   rati=" + rati);
      //		ResumeAt100 = int.Parse(rati);

      //		if (ResumeAt100 >= 0)
      //		{
      //			var todoPage = new TodoItemPage();
      //			todoPage.BindingContext = await Database.GetItemAsync(ResumeAt100);
      //			await MainPage.Navigation.PushAsync(todoPage, false); // no animation
      //		}
      //	}
      //}
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
      //Debug.WriteLine("OnSleep saving ResumeAt100 = " + ResumeAt100);
      //// the app should keep updating this value, to
      //// keep the "state" in case of a sleep/resume
      //Properties["ResumeAt100"] = ResumeAt100;
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
      //Debug.WriteLine("OnResume");
      //if (Properties.ContainsKey("ResumeAt100")) 
      //{
      //	var rati = Properties["ResumeAt100"].ToString();
      //	Debug.WriteLine("   rati=" + rati);
      //	if (!String.IsNullOrEmpty(rati))
      //	{
      //		Debug.WriteLine("   rati=" + rati);
      //		ResumeAt100 = int.Parse(rati);

      //		if (ResumeAt100 >= 0)
      //		{
      //			var todoPage = new TodoItemPage();
      //			todoPage.BindingContext = await Database.GetItemAsync(ResumeAt100);
      //			await MainPage.Navigation.PushAsync(todoPage, false); // no animation
      //		}
      //	}
      //}
    }


    // k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k  k


    public bool PromptToConfirmExit
    {
      get
      {
        bool promptToConfirmExit = false;
        if (MainPage is ContentPage)
        {
          promptToConfirmExit = true;
        }
        else if (MainPage is Xamarin.Forms.MasterDetailPage masterDetailPage
            && masterDetailPage.Detail is NavigationPage detailNavigationPage)
        {
          promptToConfirmExit = detailNavigationPage.Navigation.NavigationStack.Count <= 1;
        }
        else if (MainPage is NavigationPage mainPage)
        {
          if (mainPage.CurrentPage is TabbedPage tabbedPage
              && tabbedPage.CurrentPage is NavigationPage navigationPage)
          {
            promptToConfirmExit = navigationPage.Navigation.NavigationStack.Count <= 1;
          }
          else
          {
            promptToConfirmExit = mainPage.Navigation.NavigationStack.Count <= 1;
          }
        }
        else if (MainPage is TabbedPage tabbedPage
            && tabbedPage.CurrentPage is NavigationPage navigationPage)
        {
          promptToConfirmExit = navigationPage.Navigation.NavigationStack.Count <= 1;
        }
        return promptToConfirmExit;
      }
    }


    public void ToggleNavigation()
    {
      //if (MainPage is TabNavPage)
      //  MainPage = new MasterDetailNavPage();
      //else
      //  MainPage = new TabNavPage();
    }


    public bool IsToastExitConfirmation
    {
      get => Preferences.Get(nameof(IsToastExitConfirmation), false);
      set => Preferences.Set(nameof(IsToastExitConfirmation), value);
    }


  }
}
