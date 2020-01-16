using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Forms = System.Windows.Forms;
using System.Drawing;

namespace UberLedController
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const bool openWindowOnStart = true;

        private MainWindow mainWindow;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (openWindowOnStart)
            {
                mainWindow = new MainWindow();
                mainWindow.Show();
            }
            Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon()
            {
                Icon = new Icon(@"C:\Users\Jesper\Documents\LedController project\LedController\UberLedController\Resources\tray16_dark.ico"),
                Text = "Led Controller - Click to open",
                Visible = true
            };
            notifyIcon.DoubleClick += (o, a) =>
            {
                if (mainWindow is null) (mainWindow = new MainWindow()).Show();
                else mainWindow.Restore();
            };

        }
    }
}
