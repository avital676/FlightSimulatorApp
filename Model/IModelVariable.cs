﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;
namespace FlightSimulator.Model

{
    // Interface for all model types
    public interface IModelVariable : INotifyPropertyChanged
    {
        // Methods
        void Connect(string ip, int port);
        void Disconnect();
        void Start();
        
        void SetRudder(double rud);
        void SetElevator(double ele);
        void SetAileron(double ail);
        void SetThrottle(double thr);

        // Variables properties
        string Indicated_heading_deg { set; get; }
        string Gps_indicated_vertical_speed { set; get; }
        string Gps_indicated_ground_speed_kt { set; get; }
        string Airspeed_indicator_indicated_speed_kt  { set; get; }
        string Gps_indicated_altitude_ft { set; get; }
        string Attitude_indicator_internal_roll_deg { set; get; }
        string Attitude_indicator_internal_pitch_deg { set; get; }
        string Altimeter_indicated_altitude_ft { set; get; }
        Location Location { get; }
        string Error { set; get; }
        bool IsConnected { set; get; }
    }
}

