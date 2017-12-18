using a2d_input.core;
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
using a2d_input.core.Enumerators;
using NAudio.Wave;

namespace a2d_input
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var device = new WaveInput(DeviceEnumerator.GetWaveInDevices().First());
            device.Start();
            device.OnDataReady += OnDataReady;
        }

        private void OnDataReady(IAudioData obj)
        {
            
        }
    }
}
