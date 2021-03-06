﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using FlightSimulator;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        public MainWindow() 
        {
            InitializeComponent();
            ServerIP.Text = deafultIp;
            ServerPort.Text = deafultPort;
        }

        readonly string deafultPort = ConfigurationManager.AppSettings["port"];
        readonly string deafultIp = ConfigurationManager.AppSettings["ip"];

        private void Button_Click_Fly(object sender, RoutedEventArgs e)
        {
            // If the user didn't enter an ip and port- use deafult values
            if (ServerIP.Text == "")
            {
                ServerIP.Text = deafultIp;
            }
            if (ServerPort.Text == "")
            {
                ServerPort.Text = deafultPort;
            }
            // Try to connect
            try
            {
                (Application.Current as App).simulatorView = new SimulatorView(ServerIP.Text, ServerPort.Text);
                // Hide this screen and open the simulator screen
                this.Hide();
                (Application.Current as App).simulatorView.ShowDialog();
            }
            catch (Exception)
            {
                text.Text = "Try again";
                ServerIP.Text = deafultIp;
                ServerPort.Text = deafultPort;
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // If the user didn'tt enter an ip and port - use deafult values
            if (ServerIP.Text == "")
            {
                ServerIP.Text = deafultIp;
            }
            if (ServerPort.Text == "")
            {
                ServerPort.Text = deafultPort;
            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            // Exit the program:
            Application.Current.Shutdown();
        }
    }
}


