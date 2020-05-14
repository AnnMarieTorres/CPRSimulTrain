// ..............................................................
// Copyright @ 2020  CNG Internet Software, LLC
//
// CPR Compression Simulation Application 1B-9
//
// Name: IAudio  ..  Interface - Dependency
// Vers: 2.1.4
//
// 
// .............................................................using System;
using System.Collections.Generic;
using System.Text;

namespace CPRSimulTrain
{
  public interface IAudio
  {
    void PlayAudioFile(string fileName);
    //
    //
    void PlayNOTAudioFile(string fileName);
  }
}
