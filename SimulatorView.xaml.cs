﻿using System;
using System.Windows;
using System.Windows.Controls;
using FlightSimulator.ViewModel;
using FlightSimulator.Views;
using FlightSimulator.Model;
using System.ComponentModel;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for SimulatorView.xaml
    /// </summary>
    public partial class SimulatorView : Window
    {
        string ip;
        int port;


        public SimulatorView(string ip, string port)
        {
            InitializeComponent();
            int portI = Int32.Parse(port);
            this.ip = ip;
            this.port = portI;
            (Application.Current as App).model.connect(ip, portI);
            navigates.DataContext = (Application.Current as App).vm_navigates;
            dataBoard.DataContext = (Application.Current as App).vm_dataBoard;
            myMap.DataContext = (Application.Current as App).vm_map;
            control.DataContext = (Application.Current as App).vm_control;
            control.getIpPort(ip, portI);
        }


        
        private void navigates_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }

        private void navigates_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            navigates.joystickN.knobPosition.X = 0;
            navigates.joystickN.knobPosition.Y = 0;
        }


    }
}