﻿using System.Windows;

namespace SoloTravelAgent.Help
{
    public class HelpProvider
    {
        public static string GetHelpKey(DependencyObject obj)
        {
            return obj.GetValue(HelpKeyProperty) as string;
        }

        public static void SetHelpKey(DependencyObject obj, string value)
        {
            obj.SetValue(HelpKeyProperty, value);
        }

        public static readonly DependencyProperty HelpKeyProperty =
            DependencyProperty.RegisterAttached("HelpKey", typeof(string), typeof(HelpProvider), new PropertyMetadata("index", HelpKey));

        private static void HelpKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //NOOP
        }

        public static void ShowHelp(string key, MainWindow originator)
        {
            HelpViewer hh = new HelpViewer(key, originator);
            hh.Show();
        }
    }
}
