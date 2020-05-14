// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: History 1
// Vers: 2.1.4
//
// Results
// .............................................................
using CPRSimulTrain.Resx;
using System;
using System.Globalization;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPRSimulTrain
{

  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Results : ContentPage
  {

    public Results()
    {
      InitializeComponent();

      #region intro stuff
      this.Title = AppResources.CPRResultTitle; // "CPR Simulator";
      BackgroundColor = Color.Black; // WhiteSmoke;
      Opacity = 0.9;
      #endregion


    }




    // =======================================================================
    // On appearing:
    //
    // =======================================================================
    //protected override async void OnAppearing()
    protected override void OnAppearing()
    {
      base.OnAppearing();
      _ = postingList();
    }


    // =======================================================================
    //
    // Handles to shutting down of current timer/Accelerometer if the
    // [Back button] is pressed
    // =======================================================================
    protected override void OnDisappearing()
    {
      base.OnDisappearing();
    }


    // =================================================================================
    // start, before screen shows (OnAppearing())
    //
    // =================================================================================
    //private async void postingList()
    private async System.Threading.Tasks.Task postingList()
    {
      //
      // do get all records, we need to go to App. under "SQL" and get
      // database and table name
      listView.ItemsSource = await App.Database.GetItemsAsync(); //await App.Database.GetNotesAsync();
      //listView.ItemsSource = App.Database.GetItems(); //await App.Database.GetNotesAsync();

      //
      // Find Best CPR
      //

      var allCPRs = await App.Database.GetItemsAsync();
      int aaLng = allCPRs.Count();
      Alles.PercentKeep = 0.0F;
      Alles.DatecprKeep = "01/01/1900";
      Alles.TimecprKeep = "00:00 AM";
      //
      for (int ii = 0; ii < aaLng; ii++)
      {
        // date time = "12/23/2020 05:34 PM"
        int blankPointer = allCPRs[ii].datetimeCPR.IndexOf(" ");
        int datetimeLength = allCPRs[ii].datetimeCPR.Length;
        //
        // get details
        //
        Alles.Datecpr = allCPRs[ii].datetimeCPR.Substring(0, blankPointer);
        Alles.Timecpr = allCPRs[ii].datetimeCPR.Substring(blankPointer, datetimeLength - blankPointer);
        Alles.Deep = Convert.ToDouble(allCPRs[ii].deepCPR);
        Alles.AOK = Convert.ToDouble(allCPRs[ii].goodCPR);
        Alles.Shallow = Convert.ToDouble(allCPRs[ii].shallowCPR);
        Alles.Percentnow = Alles.AOK / (Alles.Deep + Alles.AOK + Alles.Shallow);
        //
        // use "percent now" to get best of all
        //
        if (Alles.PercentKeep < Alles.Percentnow)
        {
          Alles.PercentKeep = Alles.Percentnow;
          Alles.DatecprKeep = Alles.Datecpr;
          Alles.TimecprKeep = Alles.Timecpr;
        }
      }
      if (Alles.DatecprKeep == "01/01/1900")
      {
        lbl_OnBox.Text = AppResources.CPRResultNoData; //  "No Data Yet";
        lbl_AtBox.Text = "  ";
        lbl_ResultVal.Text = "  ";
        lbl_Congratulation.IsEnabled = false;
        lbl_shareText.IsEnabled = false;
        SfButton_Share.IsEnabled = false;
      }
      else
      {
        lbl_OnBox.Text = Alles.DatecprKeep;
        lbl_AtBox.Text = Alles.TimecprKeep;
        lbl_ResultVal.Text = Alles.PercentKeep.ToString("P", CultureInfo.InvariantCulture);
        lbl_Congratulation.IsEnabled = true;
        lbl_shareText.IsEnabled = true;
        SfButton_Share.IsEnabled = true;
      }

    }


    // ====================================================================================================================================
    // Item selected from List
    //
    // ====================================================================================================================================
    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      var temp1 = e.SelectedItem as itemsDBTable;
      if (e.SelectedItem != null)
      {
        //App.toManageRules = temp1.SilenceRangeDB;
        //await Navigation.PushAsync(new ManageRules
        //{
        BindingContext = e.SelectedItem as itemsDBTable;
        //});
      }

      // announce, and let user decide if yes/no
      string strMM = AppResources.CPRResultData1 +"  " + temp1.datetimeCPR 
        + AppResources.CPRResultGoodData + "  " + temp1.goodCPR 
        + AppResources.CPRResultShallowData + "  " + temp1.shallowCPR 
        + AppResources.CPRResultShallowData + "  " + temp1.deepCPR + "\n\n";
      await Gen_Utility_1.DisplaySfPopupAlert(AppResources.CPRResultDeleteData, strMM, AppResources.CPRResultYesDelete, AppResources.CPRResultDeleteCancel);
      if (Alles.PopupAccepted)
      {
        BindingContext = e.SelectedItem as itemsDBTable;
        var sqlitem = (itemsDBTable)BindingContext;
        await App.Database.DeleteItemAsync(sqlitem).ConfigureAwait(true);
        listView.ItemsSource = await App.Database.GetItemsAsync(); //await App.Database.GetNotesAsync();
        //await Navigation.PopAsync();
      }

    }


    #region 'Local Navigation' Functions
    private async void BaseUserGuide_P3_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseUserGuide_P3());
    private async void BaseUserGuide_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseUserGuide());
    private async void BaseFeedback_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseFeedback());
    private async void BaseShare_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseShare());
    private async void BaseDisclaimer_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseDisclaimer());
    private async void BasePrivacy_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BasePrivacy());
    private async void StatAbout_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseAbout());
    private async void BaseContactUs_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseContactUs());
        #endregion



    // ====================================================================================================================================
    // Share good news of a-ok CPR
    //
    // ====================================================================================================================================
    private async void SfButton_Share_Clicked_1(object sender, EventArgs e)
    {
      await ShareTest.ShareText(Alles.shareLabelDotText);
    }

  }
}
