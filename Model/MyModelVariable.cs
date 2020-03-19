﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace FlightSimulator.Model
{
    partial class MyModelVariable : IModelVariable
    {
        public MyModelVariable(string s, int i)
        {
            Console.WriteLine("constractor");
            connect(s, i);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        volatile Boolean stop;
        TcpClient client;
        NetworkStream strm;

        void connect(string ip, int port) 
        {
            Console.WriteLine("connect");
            client = new TcpClient();
            while (!client.Connected)
            {
                Console.WriteLine("Trying to connect to server...");
                try
                {
                    client.Connect(ip, port);
                } catch (Exception)
                {
                    Console.WriteLine("Can't connect. Trying again...");
                }
            }
            strm = client.GetStream();
            stop = false;
            start();
        }

        void start()
        {
            Console.WriteLine("start");
            new Thread(delegate ()
            {
                String msg;
                String ans;
                ASCIIEncoding asen = new ASCIIEncoding();
                List<string> dataList = new List<string>();
                byte[] msgB = new byte[256];
                byte[] dataB = new byte[256];
                while (!stop)
                {
                    // get eight values for data board:
                    //1
                    msg = "get/ indicated-heading-deg\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    //Console.WriteLine(ans);
                    if (!ans.Contains("ERR"))
                    {
                        Indicated_heading_deg = Double.Parse(ans);
                    }
                    Console.WriteLine("3");
                    this.Indicated_heading_deg = 20;
                    //indicated_heading_deg = Double.Parse("3");

                    //2
                    msg = "get/ gps_indicated-vertical-speed\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    if (!ans.Contains("ERR"))
                    {
                        Gps_indicated_vertical_speed = Double.Parse(ans);
                    }

                    //3
                    msg = "get/ gps_indicated-ground-speed-kt\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    if (!ans.Contains("ERR"))
                    {
                        Gps_indicated_ground_speed_kt = Double.Parse(ans);
                    }

                    //4
                    msg = "get/ airspeed-indicator_indicated-speed-kt\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    if (!ans.Contains("ERR"))
                    {
                        Airspeed_indicator_indicated_speed_kt = Double.Parse(ans);
                    }

                    //5
                    msg = "get/ gps_indicated-altitude-ft\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    if (!ans.Contains("ERR"))
                    {
                        Gps_indicated_altitude_ft = Double.Parse(ans);
                    } 

                    //6
                    msg = "get/ attitude-indicator_internal-roll-deg\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    if (!ans.Contains("ERR"))
                    {
                        Attitude_indicator_internal_roll_deg = Double.Parse(ans);
                    }

                    //7
                    msg = "get/ attitude-indicator_internal-pitch-deg\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    if (!ans.Contains("ERR"))
                    {
                        this.Attitude_indicator_internal_pitch_deg = Double.Parse(ans);
                    }

                    //8
                    msg = "get/ altimeter_indicated-altitude-ft\n";
                    msgB = asen.GetBytes(msg);
                    strm.Write(msgB, 0, msgB.Length);
                    dataB = new byte[100];
                    strm.Read(dataB, 0, 100);
                    ans = System.Text.Encoding.ASCII.GetString(dataB, 0, dataB.Length);
                    if (!ans.Contains("ERR"))
                    {
                        Altimeter_indicated_altitude_ft = Double.Parse(ans);
                    }
                    Console.WriteLine("sent 8 values\n");
                    Thread.Sleep(250);
                }

            }).Start();
        }

        void disconnect() 
        {
            stop = true;
            client.Close();
        }

        private double indicated_heading_deg;
        public double Indicated_heading_deg
        {
            get { return indicated_heading_deg; }
            set
            {
                Console.WriteLine("set");
                Console.WriteLine(value);
                indicated_heading_deg = value;
                NotifyPropertyChanged("Indicated_heading_deg");
            }
        }

        private double gps_indicated_vertical_speed;
        public double Gps_indicated_vertical_speed
        {
            get { return gps_indicated_vertical_speed; }
            set
            {
                gps_indicated_vertical_speed = value;
                NotifyPropertyChanged("gps_indicated_vertical_speed");
            }
        }

        private double gps_indicated_ground_speed_kt;
        public double Gps_indicated_ground_speed_kt
        {
            get { return gps_indicated_ground_speed_kt; }
            set
            {
                gps_indicated_ground_speed_kt = value;
                NotifyPropertyChanged("gps_indicated_ground_speed_kt");
            }
        }

        private double airspeed_indicator_indicated_speed_kt;
        public double Airspeed_indicator_indicated_speed_kt
        {
            get { return airspeed_indicator_indicated_speed_kt; }
            set
            {
                airspeed_indicator_indicated_speed_kt = value;
                NotifyPropertyChanged("airspeed_indicator_indicated_speed_kt");
            }
        }

        private double gps_indicated_altitude_ft;
        public double Gps_indicated_altitude_ft
        {
            get { return gps_indicated_altitude_ft; }
            set
            {
                gps_indicated_altitude_ft = value;
                NotifyPropertyChanged("gps_indicated_altitude_ft");
            }
        }

        private double attitude_indicator_internal_roll_deg;
        public double Attitude_indicator_internal_roll_deg
        {
            get { return attitude_indicator_internal_roll_deg; }
            set
            {
                attitude_indicator_internal_roll_deg = value;
                NotifyPropertyChanged("attitude_indicator_internal_roll_deg");
            }
        }

        private double attitude_indicator_internal_pitch_deg;
        public double Attitude_indicator_internal_pitch_deg
        {
            get { return attitude_indicator_internal_pitch_deg; }
            set
            {
                attitude_indicator_internal_pitch_deg = value;
                NotifyPropertyChanged("attitude_indicator_internal_pitch_deg");
            }
        }

        private double altimeter_indicated_altitude_ft;
        private object requestCallback;

        public double Altimeter_indicated_altitude_ft
        {
            get { return altimeter_indicated_altitude_ft; }
            set
            {
                altimeter_indicated_altitude_ft = value;
                NotifyPropertyChanged("altimeter_indicated_altitude_ft");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                Console.WriteLine(propName);
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}


    

