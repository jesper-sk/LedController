using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data;

namespace UberLedController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string test = "Hey Jochie";
        public MainWindow()
        {
            InitializeComponent();
            //Binding bind = new Binding(@"C:\Users\Jesper\Documents\LedController project\LedController\LEDcontroller\bin\Debug\data\profiles\ProfileSet_0.xml");
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void Restore()
        {
            if (WindowState == WindowState.Minimized)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(delegate ()
                    {
                        WindowState = WindowState.Normal;
                        Activate();
                    }
                ));
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            InFocusImage.Visibility = Visibility.Visible;
            OutFocusImage.Visibility = Visibility.Hidden;
            //TitleLabel.Foreground = Application.Current.Resources["LightAccentColor"] as SolidColorBrush;
            Application.Current.Resources["HeaderbarTextColor"] = new SolidColorBrush((Color)Application.Current.Resources["FocusHeaderTextColor"]);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            InFocusImage.Visibility = Visibility.Hidden;
            OutFocusImage.Visibility = Visibility.Visible;
            //TitleLabel.Foreground = Application.Current.Resources["DarkLightAccent"] as SolidColorBrush;
            Application.Current.Resources["HeaderbarTextColor"] = new SolidColorBrush((Color)Application.Current.Resources["UnFocusHeaderTextColor"]);
        }
    }
}
