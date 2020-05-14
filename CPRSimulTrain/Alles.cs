// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC.
//
// CPR Compression Simulation Application 1B-9
//
// Name: Alles
// Vers: 2.1.4
//
// Global class, variables
// .............................................................
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace CPRSimulTrain
{
  public class Alles
  {


    private static bool _beepAllowed;
    public static bool BeepAllowed
    {
      get { return _beepAllowed; }
      set { _beepAllowed = value; }
    }


    // Set speed delay for monitor changes.
    public static readonly SensorSpeed accelspeed = SensorSpeed.Game;  //.UI;


    #region Database Material

    // Database name
    public const string dbname100 = "CNGCPRSimul002.db3";
    // SQLite
    public static CPRSimulTrainDB database;

    //grb//public static int toManageRules = 0;


    #endregion


    #region Notification popup
    public static bool PopupAccepted = false;
    public static bool popupmsg_001;
    #endregion


    public readonly static string AppName = "CPR Simul Training";
    public readonly static string AppVersion = "2.1.14";
    public readonly static string RegNum = "Fa-KK-23776-IO";
    public readonly static string RegWhen = "Apr-29-20";
    public readonly static string RelDate = "May 9, 2020";

    //
    // Accelerometer E N T R Y
    //
    // Set basic for 100 compressions per minute 
    // 1 min, 60 sec, 1000 per sec = 60000 / 100 = 600 milliseconds per compression
    public const int adultCompressInterval = 600;

    //public static double ScreenWidth;        // Width (in pixels)
    //public static double ScreenHeight;       // Height (in pixels)


    private static double _screenWidth;
    public static double ScreenWidth
    {
      get { return _screenWidth; }
      set { _screenWidth = value; }
    }

    private static double _screenHeight;
    public static double ScreenHeight
    {
      get { return _screenHeight; }
      set { _screenHeight = value; }
    }


    #region "Result.cs" variables

    // if a share-note starts with "WD" (With Data), its a 
    // special share out or "Result" and carries some data too.
    public static string shareLabelDotText = "WD";


    private static double _deep;
    public static double Deep
    {
      get { return _deep; }
      set { _deep = value; }
    }
    
    private static double _aok;
    public static double AOK
    {
      get { return _aok; }
      set { _aok = value; }
    }
    
    private static double _shallow;
    public static double Shallow
    {
      get { return _shallow; }
      set { _shallow = value; }
    }
    
    private static double _percentnow;
    public static double Percentnow
    {
      get { return _percentnow; }
      set { _percentnow = value; }
    }

    private static double _percentKeep;
    public static double PercentKeep
    {
      get { return _percentKeep; }
      set { _percentKeep = value; }
    }

    private static string _datecpr;
    public static string Datecpr
    {
      get { return _datecpr; }
      set { _datecpr = value; }
    }

    private static string _timecpr;
    public static string Timecpr
    {
      get { return _timecpr; }
      set { _timecpr = value; }
    }

    private static string _datecprKeep;
    public static string DatecprKeep
    {
      get { return _datecprKeep; }
      set { _datecprKeep = value; }
    }

    private static string _timecprKeep;
    public static string TimecprKeep
    {
      get { return _timecprKeep; }
      set { _timecprKeep = value; }
    }

    #endregion


  }
}
