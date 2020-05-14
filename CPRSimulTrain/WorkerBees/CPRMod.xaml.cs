// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC.
//
// CPR Compression Simulation Application 1B-9
//
// Name: CPRSimul
// Vers: 2.1.4
//
//  CPR Simulation
// .............................................................
//??//using Android.Media;
using CPRSimulTrain.Resx;
using System;
using System.Threading.Tasks;
using System.Timers;

using Syncfusion.XForms.Buttons;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CPRSimulTrain
{
  //[DesignTimeVisible(true)]s
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class CPRMod : ContentPage
  {


    #region app globals

    bool newTimerEvent;
    bool inPrinting;

    int aX;
    int aY;
    int aZ;
    int aclAvgZ;

    int aTotalZ = 0;
    int aTotalZ_cnt = 0;

    int noise_Readings = 0;
    int noiseRead_Level = 5;
    private readonly int tilt_40 = 40;

    int thirtyCnt = 0;
    int totalThirtyCnt = 0;
    int thirtySetCNT = 1;

    bool noActions;

    int totalCorrectCPR = 0;
    int totalFlatCPR = 0;
    int totalTooDeepCPR = 0;

    #endregion

    // Compressions timer --------------------------------------------
    public static Timer baseTimer = new System.Timers.Timer();

    public CPRMod()
    {
      InitializeComponent();

      #region intro stuff
      this.Title = AppResources.CPRSimulTitle; //CPR"CPR Simulator";
      BackgroundColor = Color.Black; // WhiteSmoke;
      Opacity = 0.9;
      #endregion

      // 
      #region Setup, Accelerator
      //
      this.getAccelerometer();
      if (Alles.popupmsg_001) _ = this.popup001();

      #endregion


    }


    #region popup, OnAppearing, OnDisappearing, zeroOut

    // =======================================================================
    // pop up for popup-notification
    //
    // ======================================================================
    private async Task popup001()
    {
      //
      // announce, and let user decide if yes/no
      string strMM = AppResources.CPRSimulPopupmsg;
      await Gen_Utility_1.DisplaySfPopupAlert("CPR Simulation", strMM, AppResources.CPRSimulGotit, "");
      if (Alles.PopupAccepted)
      {
      }

    }


    // =======================================================================
    // On appearing:
    //
    // =======================================================================
    protected override void OnAppearing()
    {
      base.OnAppearing();
      zeroOut();
      inPrinting = false;

      #region Init Timer, install

      //
      // 100/compressions / minute timer.
      // 1 min, 60 sec, 1000 per sec = 60000 / 100 = 600 milliseconds per compression
      //

      try
      {
        baseTimer.Elapsed += new ElapsedEventHandler(OnBaseTimerEvent);
        baseTimer.Interval = Alles.adultCompressInterval;
        baseTimer.AutoReset = true;
        baseTimer.Enabled = true;
        baseTimer.Start();
      }
      catch (Exception eX)
      {
        _ = eX.Message.ToString();
      }

      #endregion


      #region START

      {
        noActions = false;
        lbl_SetCnt.Text = "0";
        lbl_CPRCnt.Text = "0";
        noise_Readings = 0;
        thirtyCnt = 0;
        totalThirtyCnt = 0;
        thirtySetCNT = 1;

        lbl_goodCompress.Text = AppResources.CPRSimulNoCPR; // "- No CPR -";
        lbl_goodCompress.TextColor = Color.DarkRed;
        lbl_goodCompress.BackgroundColor = Color.WhiteSmoke;
        gridTwo.BackgroundColor = Color.WhiteSmoke;

        //
        try
        {
          Accelerometer.Start(Alles.accelspeed);
        }
        catch (Exception eX)
        {
          _ = eX.Message.ToString();
        }
      }


      #endregion
    }


    // =======================================================================
    // On Disappearing
    //
    // =======================================================================
    protected override void OnDisappearing()
    {
      base.OnDisappearing();
      // Stop, and unregister
      baseTimer.Stop();
      baseTimer.Elapsed -= new ElapsedEventHandler(OnBaseTimerEvent);

      // Stop, and unregister
      Accelerometer.Stop();
      Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;

      //
      zeroOut();
    }


    // ===============================================================================================
    // Zero out
    //
    // ===============================================================================================
    private void zeroOut()
    {
      //
      newTimerEvent = false;

      //
      //grb//lbl_rawStatus.Text = "";
      lbl_goodCompress.Text = AppResources.CPRSimulReady; // "- Ready -";
      lbl_goodCompress.TextColor = Color.DarkRed;
      lbl_goodCompress.BackgroundColor = Color.LightYellow;
      gridTwo.BackgroundColor = Color.LightYellow;
      noActions = true;
      //
      //
      //
      thirtyCnt = 0;
      totalThirtyCnt = 0;
      thirtySetCNT = 1;
      //
      lbl_SetCnt.Text = "0";
      lbl_CPRCnt.Text = "0";
      totalCorrectCPR = 0;
      totalFlatCPR = 0;
      totalTooDeepCPR = 0;
      //
      aTotalZ = 0;
      aTotalZ_cnt = 0;

    }

    #endregion


    #region 'Local Navigation' Functions
    private async void BaseUserGuide_P2_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseUserGuide_P2());
    private async void BaseUserGuide_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseUserGuide());
    private async void BaseFeedback_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseFeedback());
    private async void BaseShare_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseShare());
    private async void BaseDisclaimer_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseDisclaimer());
    private async void BasePrivacy_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BasePrivacy());
    private async void StatAbout_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseAbout());
    private async void BaseContactUs_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new BaseContactUs());






    #endregion


    #region Monitor On/Off / Beep On/Off / Voice (Sound) On/Off


    // ==========================================================================================
    // Monitor state changed to ON/OFF
    //
    // The toggle will start the accelerator
    // ==========================================================================================
    private void btAgain_Clicked(object sender, EventArgs e)
    {
      //
      zeroOut();

      noActions = false;
      lbl_SetCnt.Text = "0";
      lbl_CPRCnt.Text = "0";
      noise_Readings = 0;
      thirtyCnt = 0;
      totalThirtyCnt = 0;
      thirtySetCNT = 1;

      lbl_goodCompress.Text = AppResources.CPRSimulNoCPR; // "- No CPR -";
      lbl_goodCompress.TextColor = Color.DarkRed;
      lbl_goodCompress.BackgroundColor = Color.WhiteSmoke;
      gridTwo.BackgroundColor = Color.WhiteSmoke;

      readyBeep.IsOn = false;

      inPrinting = false;
      //



      #region Init Timer, install

      //
      // 100/compressions / minute timer.
      // 1 min, 60 sec, 1000 per sec = 60000 / 100 = 600 milliseconds per compression
      //

      try
      {
        baseTimer.Elapsed += new ElapsedEventHandler(OnBaseTimerEvent);
        baseTimer.Interval = Alles.adultCompressInterval;
        baseTimer.AutoReset = true;
        baseTimer.Enabled = true;
        baseTimer.Start();
      }
      catch (Exception eX)
      {
        _ = eX.Message.ToString();
      }

      #endregion


      #region START

      //
      try
      {
        Accelerometer.Start(Alles.accelspeed);
      }
      catch (Exception eX)
      {
        _ = eX.Message.ToString();
      }

      #endregion


      #region Setup, Accelerator
      //
      this.getAccelerometer();
      if (Alles.popupmsg_001) _ = this.popup001();

      #endregion


      #region Setup, Accelerator
      //
      this.getAccelerometer();
      if (Alles.popupmsg_001) _ = this.popup001();

      #endregion

      #region Init Timer, install
      //
      // 100/compressions / minute timer.
      // 1 min, 60 sec, 1000 per sec = 60000 / 100 = 600 milliseconds per compression
      //

      try
      {
        baseTimer.Elapsed += new ElapsedEventHandler(OnBaseTimerEvent);
        baseTimer.Interval = Alles.adultCompressInterval;
        baseTimer.AutoReset = true;
        baseTimer.Enabled = true;
        baseTimer.Start();
      }
      catch (Exception eX)
      {
        _ = eX.Message.ToString();
      }

      #endregion


      #region START

      //
      try
      {
        Accelerometer.Start(Alles.accelspeed);
      }
      catch (Exception eX)
      {
        _ = eX.Message.ToString();
      }


      #endregion


    }



    // ==========================================================================================
    // beep state changed to ON/OFF
    //
    // The toggle will start the beep for every simulated Compression
    // ==========================================================================================
    private void SfSwitch_beepStateChanged(object sender, SwitchStateChangedEventArgs e)
    {
      if (e.NewValue == true)
      {
        Alles.BeepAllowed = true;
      }
      else
      {
        Alles.BeepAllowed = false;
      }
    }


    #endregion


    #region Timer Event



    // ==========================================================================================
    // OnBaseTimerEvent
    //
    // This timer event is only 'up' in between Monitor toggled on / off.
    // i.e. Monitor 'ON' will start the timer, Monitor 'OFF' will stop the timer.
    //
    // 
    // ==========================================================================================
    private void OnBaseTimerEvent(Object source, System.Timers.ElapsedEventArgs e)  //EventArgs e)
    {
      newTimerEvent = true;     // announce to the world that we just hit a timer event
    }


    #endregion


    #region Cancel, Save All

    // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Cancel
    //
    // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private async void btCancel_Clicked(object sender, EventArgs e)
    {
      await Navigation.PopAsync();
    }


    // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Save. Save will display "no input" alert.
    //
    // Input via:
    // Id          =  ID (auto increment while adding the record) 
    // Run Date    =  DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
    // goodCPR     =  goodCPR;
    // schallowCPR =  flatCPR;
    // deepCPR     =  deepCPR;
    // :::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private async void btResults_Clicked(object sender, EventArgs e)
    {
      string strLL;
      string strMM;
      string errMSGHeader = AppResources.CPRSimulResultMsg1; // "CPR Simulated Training Result";
      inPrinting = true;

      try
      {
        // noActions is on (true), we have NO RESULT(S), don't save
        //
        if (noActions)
        {
          //
          strLL = AppResources.CPRSimulResultMsg2; // "\nNo CPR Simulation Result available.\n"
                  //+ "___________________________________\n\n"
            //+ "  Please run a set of simulated CPR's\n"
            //+ "    and try again.\n";
          await Gen_Utility_1.DisplaySfPopupAlert(errMSGHeader, strLL, AppResources.CPRSimulGotit, "");
          if (Alles.PopupAccepted)
          {
          }
          inPrinting = false;
        }
        else
        {
          BindingContext = new itemsDBTable();
          var sqlitem = (itemsDBTable)BindingContext;
          sqlitem.datetimeCPR = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
          sqlitem.goodCPR = totalCorrectCPR.ToString();
          sqlitem.shallowCPR = totalFlatCPR.ToString();
          sqlitem.deepCPR = totalTooDeepCPR.ToString();

          // announce, and let user decide if yes/no
          //////strMM = "\n  Adding CPR Simulation Results:\n"
          //////        + "  ______________________________\n\n"
          //////+ " Good Compressions .. .. .. .. .".PadRight(40) + sqlitem.goodCPR + "\n"
          //////+ " Compressions too Shallow..".PadRight(37) + sqlitem.shallowCPR + "\n"
          //////+ " Compressions to Deep .. .. ..".PadRight(40) + sqlitem.deepCPR + "\n\n";

          String data11 = String.Format("{0,-40} {1,-10}", AppResources.CPRSimulResultdata11, sqlitem.goodCPR);
          String data22 = String.Format("{0,-36} {1,-10}", AppResources.CPRSimulResultdata22, sqlitem.shallowCPR);
          String data33 = String.Format("{0,-38} {1,-10}", AppResources.CPRSimulResultdata33, sqlitem.deepCPR);

          strMM = AppResources.CPRSimulResultstm // "\n  Adding CPR Simulation Results:\n"
                  //+ "  ______________________________\n\n"
          + data11 + "\n"
          + data22 + "\n"
          + data33 + "\n\n";


          await Gen_Utility_1.DisplaySfPopupAlert(errMSGHeader, strMM, AppResources.CPRSimulResultYes, AppResources.CPRSimulCancel);
          if (Alles.PopupAccepted)
          {
            await App.Database.SaveItemAsync(sqlitem);
            readyBeep.IsOn = false;     // the first hit (false) is needed to set the 'stage'
            zeroOut();
            this.getAccelerometer();
            if (Alles.popupmsg_001) _ = popup001();
            await this.Navigation.PushAsync(new Results());
          }
          else
          {
            inPrinting = false;
          }
        }
      }
      catch (Exception sqle1)
      {
        _ = (sqle1.Message.ToString());
      }


    }


    #endregion


    #region Accelerometer functions


    // ==============================================================================================
    // get Accelerometer
    //
    // ==============================================================================================
    private void getAccelerometer()
    {
      try
      {
        if (Accelerometer.IsMonitoring)
        {
          Accelerometer.Stop();           // If already running, stop, and set up under MainThread
        }
        else
        {
          MainThread.BeginInvokeOnMainThread(() =>
          {
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(Alles.accelspeed);
            Accelerometer.Stop();
          });
        }
        Alles.popupmsg_001 = false;
      }
      catch (FeatureNotSupportedException fnsEX)
      {
        string aa = fnsEX.Message.ToString();
        Alles.popupmsg_001 = true;
      }
      catch (Exception eX)
      {
        string aa = eX.Message.ToString();
        Alles.popupmsg_001 = true;
      }
    }


    // =================================================================================================
    // Accelerometer_ReadingChanged
    //
    // The accelerometer posts about 30 readings per timer-event.  
    //    This function is entered about 30 times per ONE CPR Compress.
    // One of the 30+ accelerator events happen to be at the same
    //    moment the timer event happen.
    //
    // The first 5 readings are discarded; noise.
    //
    // The accelerator axis are:
    //     X / Y parallel to surface ( think north, east)
    //     Z perpendicular to surface ( think down to earth center, up into space)
    //
    // If Accelerator Z direction (away from ground) is: 
    // (+, positive), it points down, and we have a CPR Compress. CONTINUE 
    // (-, negative), it points up, and we have a CPR Recoil, STOP.
    // =================================================================================================
    private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
      //
      noise_Readings++;
      if (noise_Readings <= noiseRead_Level) return;// Skip very first (5) reads; Noise
      if (inPrinting) return;                       // in printing, and NO ACTIVITIES!!
      // . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
      read_XYZ(e.Reading);                          // Get the X, Y, Z, total Z, readings.
                                                    //
      if (newTimerEvent)                            // Display at each timer event (100/min, every approx 30th acceleration read)
      {
        if (aTotalZ_cnt > 0)                        // after at least one accelerator read
        {
                                                    // Phone must be flat for a valid accelerometer reading
          if (isPhoneLayingFlat(tilt_40, aX, aY))   // iS tilted IF X/Y > 0.4 (40).
          {
            aclAvgZ = aTotalZ / aTotalZ_cnt;          // Get average Z
            thirtyCountProc();                      // process CPR count (30, and Sets)
            Gen_Utility_1.BeepOnce();                     // beep if beep is allowed
                                                    // Only process "Downward" events
                                                    //if (aclAvgZ < 98)                       // 98 = rest on a desk
            displayCPR_Actions(aclAvgZ);            // show " Correct CPR ", " Too Shallow ", " Too Deep "
            showTESTdata(aclAvgZ);
            //
            newTimerEvent = false;                  // turned on inside timer event
            aTotalZ = 0;                            //
            aTotalZ_cnt = 0;                        //
          }
        }
      }

    }




    #endregion


    #region AC helper functions


    // ------------------------------------------------------------------------------------------
    // Thirty Count Helper
    //
    // ------------------------------------------------------------------------------------------
    private void thirtyCountProc()
    {
      thirtyCnt++;
      totalThirtyCnt++;

      if (thirtyCnt >= 30)
      {
        thirtyCnt = 0;
        thirtySetCNT++;
      }
      int temp1 = (((thirtySetCNT - 1) * 30) + (thirtyCnt - 1));
      lbl_CPRCnt.Text = temp1.ToString("0");
      lbl_SetCnt.Text = thirtySetCNT.ToString("0");

    }


    // ------------------------------------------------------------------------------------------
    // Extract X,Y,Z accelerometer values, convert to a 3-member int array.
    // 
    // The values are being converted to int * 100 (Z = 0.98 = 98) for
    // easier processing.
    //  X & Y readings are converted to absolute (taking '-' out).
    // ------------------------------------------------------------------------------------------
    private void read_XYZ(AccelerometerData accelData)
    {
      //
      aX = (Convert.ToInt16(Math.Abs(accelData.Acceleration.X) * 100));
      aY = (Convert.ToInt16(Math.Abs(accelData.Acceleration.Y) * 100));
      aZ = (Convert.ToInt16(accelData.Acceleration.Z * 100));
      //
      // if Z -, in recoil, skip ++ Only downward Z's are used ( < 98)
      if ((aZ > 0) & (aZ <= 98))
      {
        // get the averages lined up
        aTotalZ = aTotalZ + aZ;
        // get total used read count
        aTotalZ_cnt++;
      }
    }


    // -----------------------------------------------------------------------------------------
    // Checking Device readiness; i.e. is device laying flat?
    // Z readings from a badly tilted device at X or Y axis distorts the Z reading.
    // This function assumes 0.40G as badly tilted (0.40 * 100 = 40)
    //
    // Input:
    // tiltg = tilt fence (like 40)
    // intX, intY = accelerator x/y reading
    //
    // test if we read X/Y over 40. If Yes, we have a tilted device'
    // Tilted, and we display " -No CPR- " no matter what.
    //
    // Return:
    // if tilted, return will be true, and the " -No CPR-" message is displayed
    // if flat:) message " -Ready- ", "Too Shallow", "Correct CPR", "Too Deep"
    // -----------------------------------------------------------------------------------------
    private bool isPhoneLayingFlat(int tiltg, int intX, int intY)
    {
      bool intRet = true;         // have flat phone
      try
      {
        if ((intX > tiltg) | (intY > tiltg))
        {
          lbl_goodCompress.Text = AppResources.CPRSimulNoCPR; // "-No CPR-\nNOT Flat";
          lbl_goodCompress.TextColor = Color.Yellow;
          lbl_goodCompress.BackgroundColor = Color.Red;
          gridTwo.BackgroundColor = Color.Red; //.FromHex("#f90000"); // Color.Light;
          return false;
        }
        //
      }
      catch (Exception eX)
      {
        _ = eX.Message.ToString();
        intRet = false;
      }
      return intRet;
    }



    // ------------------------------------------------------------------------------------------
    // Get hit fences
    //
    // See documentation
    //
    // Get Accelerator number, and display notification
    // ------------------------------------------------------------------------------------------
    private void displayCPR_Actions(int hitZ)
    {
      //
      //
      if (hitZ == 98)
      {
        sayACTIVATED_But_STILL();
      }
      else if ((hitZ <= 97) & (hitZ >= 92))
      {
        sayTOO_SHALLOW();
      }
      else if ((hitZ <= 91) & (hitZ >= 85))
      {
        sayCORRECT();
      }
      else
      {
        sayTOODEEP();
      }
    }


    // -------------------------------------------------------------------------------------------------
    //
    // -------------------------------------------------------------------------------------------------
    private void sayACTIVATED_But_STILL()
    {
      lbl_goodCompress.Text = AppResources.CPRSimulActivated; // "Activated\n-At Rest-";
      lbl_goodCompress.TextColor = Color.LightGray;
      lbl_goodCompress.BackgroundColor = Color.LightYellow;
      gridTwo.BackgroundColor = Color.LightYellow;
    }


    // -------------------------------------------------------------------------------------------------
    //
    // -------------------------------------------------------------------------------------------------
    private void sayTOO_SHALLOW()
    {
      lbl_goodCompress.Text = AppResources.CPRSimulTooFlat; // "Too Shallow";
      lbl_goodCompress.TextColor = Color.Blue;
      lbl_goodCompress.BackgroundColor = Color.LightGreen;
      gridTwo.BackgroundColor = Color.LightYellow;
      totalFlatCPR++;
    }

    // -------------------------------------------------------------------------------------------------
    //
    // -------------------------------------------------------------------------------------------------
    private void sayCORRECT()
    {
      lbl_goodCompress.Text = AppResources.CPRSimulCorrect; // "Correct CPR";
      lbl_goodCompress.TextColor = Color.Blue;
      lbl_goodCompress.BackgroundColor = Color.LightGreen;
      gridTwo.BackgroundColor = Color.LightYellow;
      totalCorrectCPR++;
    }

    // -------------------------------------------------------------------------------------------------
    //
    // -------------------------------------------------------------------------------------------------
    private void sayTOODEEP()
    {

      lbl_goodCompress.Text = AppResources.CPRSimulTooDeep; // "Too Deep";
      lbl_goodCompress.TextColor = Color.Blue;
      lbl_goodCompress.BackgroundColor = Color.Red;
      gridTwo.BackgroundColor = Color.LightYellow;
      totalTooDeepCPR++;
    }


    // ---------------------------------------------------------------------------------------------
    // Show stats
    //
    // --------------------------------------------------------------------------------------------
    private void showTESTdata(int av_Zz)
    {
      //grb//lbl_rawStatus.Text = lbl_rawStatus.Text + " / " + av_Zz.ToString("D3");
    }



    //// =======================================================================
    //// Small Help Button, next to [ start/ refresh ]
    ////
    //// =======================================================================
    //private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    //{
    //  await Util100.DisplaySfPopupAlert(AppResources.AlertFirstQuestionHd, AppResources.AlertFisrtQuestion, "  Neat  ", "");
    //}


    #endregion
  }
}