﻿using SoloTravelAgent.Model.Data;
using SoloTravelAgent.ViewModel;
using System.Windows.Controls;

namespace SoloTravelAgent.View
{
    /// <summary>
    /// Interaction logic for AgentBookingsView.xaml
    /// </summary>
    public partial class AgentBookingsView : UserControl
    {
        public AgentBookingsView()
        {
            InitializeComponent();
            DataContext = new AgentBookingsViewModel();
        }
    }
}