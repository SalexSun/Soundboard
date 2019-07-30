using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace RawInputProcessor.Demo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private RawPresentationInput _rawInput;
        private int _deviceCount;
        private RawInputEventArgs _event;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        public int DeviceCount
        {
            get { return _deviceCount; }
            set
            {
                _deviceCount = value;
                OnPropertyChanged();
            }
        }

        public RawInputEventArgs Event
        {
            get { return _event; }
            set
            {
                _event = value;
                OnPropertyChanged();
            }
        }

        private void OnKeyPressed(object sender, RawInputEventArgs e)
        {
            Event = e;
            DeviceCount = _rawInput.NumberOfKeyboards;
            e.Handled = (ShouldHandle.IsChecked == true);

            if(e.Device.Name == @"\\?\ACPI#HPQ8002#4&35d0e288&0#{884b96c3-56ef-11d1-bc8c-00a0c91405dd}")
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
              
                switch(e.Key)
                {
                    case Key.Q:
                        player.SoundLocation = @"C:\Soundboard\Audio\q.wav";
                        break;
                }

                player.Play();

            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            StartWndProcHandler();
            base.OnSourceInitialized(e);
        }

        private void StartWndProcHandler()
        {
            _rawInput = new RawPresentationInput(this, RawInputCaptureMode.ForegroundAndBackground);
            _rawInput.KeyPressed += OnKeyPressed;
            DeviceCount = _rawInput.NumberOfKeyboards;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}