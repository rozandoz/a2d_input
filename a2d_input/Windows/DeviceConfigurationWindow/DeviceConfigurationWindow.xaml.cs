using System.Windows;

namespace a2d_input.Windows.DeviceConfigurationWindow
{
    /// <summary>
    ///     Interaction logic for DeviceConfigurationWindow.xaml
    /// </summary>
    public partial class DeviceConfigurationWindow : Window
    {
        public DeviceConfigurationWindow()
        {
            InitializeComponent();

            ViewModel = new DeviceConfigurationViewModel();
            DataContext = ViewModel;
        }

        public DeviceConfigurationViewModel ViewModel { get; }
    }
}