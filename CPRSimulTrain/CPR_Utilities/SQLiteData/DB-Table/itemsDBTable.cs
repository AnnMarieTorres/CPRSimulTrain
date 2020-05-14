// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: Items Table
// Vers: 3.0.0.0
//
// Low-level DB table layout routines
// .............................................................
using SQLite;

namespace CPRSimulTrain
{
  public class itemsDBTable
  {
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string datetimeCPR { get; set; }
    public string goodCPR { get; set; }
    public string shallowCPR { get; set; }
    public string deepCPR { get; set; }
  }
}

