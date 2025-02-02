﻿//
// BusinessTier objects:  these classes define the objects serving as data 
// transfer between UI and business tier.  These objects carry the data that
// is normally displayed in the presentation tier.  The classes defined here:
//
//    CTAStation
//    CTAStop
//
// NOTE: the presentation tier should not be creating instances of these objects,
// but instead calling the BusinessTier logic to obtain these objects.  You can 
// create instances of these objects if you want, but doing so has no impact on
// the underlying data store --- to change the data store, you have to call the
// BusinessTier logic.
//

using System;
using System.Collections.Generic;


namespace BusinessTier
{
  //---------------------------------------------------------------------
  ///
  /// <summary>
  /// Info about one CTA station.
  /// </summary>
  /// 
  public class CTAStation
  {
    public int ID { get; private set; }
    public string Name { get; private set; }

    public CTAStation(int stationID, string stationName)
    {
      ID = stationID;
      Name = stationName;
    }
  }
  //---------------------------------------------------------------------
  ///
  /// <summary>
  /// Info about one CTA stop.
  /// </summary>
  /// 
  public class CTAStop
  {
    public int ID { get; private set; }

    public string Name { get; private set; }

    public int StationID { get; private set; }

    public string Direction { get; private set; }

    public bool ADA { get; private set; }

    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public CTAStop(int stopID, string stopName, int stationID, string direction, bool ada, double latitude, double longitude)
    {
      ID = stopID;
      Name = stopName;
      StationID = stationID;
      Direction = direction;
      ADA = ada;
      Latitude = latitude;
      Longitude = longitude;
    }
  }
  //---------------------------------------------------------------------
  ///
  /// <summary>
  /// statistics of a station that matches Name
  /// </summary>
  /// 
  public class RidershipStats
  {
    public string Name { get; private set; }

    public int totalRidership { get; private set; }
    public int averageRidership { get; private set; }
    public double percentRidership { get; private set; }
    public int weekdayRidership { get; private set; }
    public int saturdayRidership { get; private set; }
    public int sundayHolidayRidership { get; private set; }

    public RidershipStats(string name, int total, int avg, double percent,
					   int wkdy, int sat, int sunHol)
    {
	 this.Name = name;
	 this.totalRidership = total;
	 this.averageRidership = avg;
	 this.percentRidership = percent;
	 this.weekdayRidership = wkdy;
	 this.saturdayRidership = sat;
	 this.sundayHolidayRidership = sunHol;
    }
  }
  //---------------------------------------------------------------------

}//namespace
