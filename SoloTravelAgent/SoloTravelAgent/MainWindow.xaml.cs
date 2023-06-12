using SoloTravelAgent.Model.Data;
using SoloTravelAgent.Model.Entities;
using SoloTravelAgent.Model.Service;
using SoloTravelAgent.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace SoloTravelAgent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqU01mQ1NNaV1CX2BZeFl2Q2lafU4BCV5EYF5SRHNcQl1lSXlXcUNmWHY=;Mgo+DSMBPh8sVXJ1S0R+WlpCaV1LQmFJfFBmQ2lZeVRzdkU3HVdTRHRcQlhgSX5VdkJnXnlYeH0=;ORg4AjUWIQA/Gnt2VFhiQldPcEBAVHxLflF1VWVTf1h6d1dWESFaRnZdQV1mSn5TcEVkXXhYeHBc;MjI0NjM0NEAzMjMxMmUzMDJlMzBGTzNLeFZxc0Uyc1d2YXlBRDdqanZ2MmJmWEN3dG54UURMZEhmNWlvYldZPQ==;MjI0NjM0NUAzMjMxMmUzMDJlMzBleForYndxQzJtamorVHM4TmZ0aUhHUnY4T2hFVTlwWFdGU1drckl4NTdjPQ==;NRAiBiAaIQQuGjN/V0d+Xk9CfVldX2pWfFN0RnNfdV52flZEcC0sT3RfQF5jTH1TdkNjXntZd3JXTw==;MjI0NjM0N0AzMjMxMmUzMDJlMzBqeXpkZFNlTC9IZVRVQ2diMndGUHBNVXBPd1NSYXAyWVhnTE1WTmV0TVhZPQ==;MjI0NjM0OEAzMjMxMmUzMDJlMzBZcUw4R2w4L0lkMk41OFoyMFpxV0RPSFcySWFlT2orZ3JwMDllUEhpOEhnPQ==;Mgo+DSMBMAY9C3t2VFhiQldPcEBAVHxLflF1VWVTf1h6d1dWESFaRnZdQV1mSn5TcEVkXXhWc3Bc;MjI0NjM1MEAzMjMxMmUzMDJlMzBDN013UlR3YUF6YVVvRXFjcWJjVlJNdnlnM2VCNHJUUkRFdzFMZ29lMmVVPQ==;MjI0NjM1MUAzMjMxMmUzMDJlMzBmYnh6SUJ0YkZzL2dqN3doRmlvdXNjSlpaV2E0VWQvODZLWm5oMTFrY2Q0PQ==;MjI0NjM1MkAzMjMxMmUzMDJlMzBqeXpkZFNlTC9IZVRVQ2diMndGUHBNVXBPd1NSYXAyWVhnTE1WTmV0TVhZPQ==");
            TestDatabase();
            InitializeComponent();
            var dbContext = new TravelSystemDbContext();
            DataContext = new MainViewModel(dbContext);
        }

        private void TestDatabase()
        {

            using var dbContext = new TravelSystemDbContext();


            var agentService = new AgentService(dbContext);

     
            List<Agent> agents = (List<Agent>)agentService.GetAllAgents();
            foreach (Agent age in agents)
            {
                agentService.RemoveAgent(age);
            }
            var ag = new Agent { Name = "John Doe", Email = "vanja@gmail.com", PhoneNumber = "555-1234", Password = "vanja123" };
            agentService.AddAgent(ag);
        }
    }
}
