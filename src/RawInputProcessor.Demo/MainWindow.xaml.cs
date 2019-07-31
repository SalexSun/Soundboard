using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.IO;
using System.Configuration;

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
        private string KeyBoardHidId;

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

        private void ReadFromAppConfig()
        {
            this.KeyBoardHidId = ConfigurationManager.AppSettings["mykeyboardid"];
        }

        private void OnKeyPressed(object sender, RawInputEventArgs e)
        {
            Event = e;
            DeviceCount = _rawInput.NumberOfKeyboards;
            e.Handled = (ShouldHandle.IsChecked == true);

            ReadFromAppConfig();

            if (e.Device.Name == this.KeyBoardHidId)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
  
                string folderpath = ConfigurationManager.AppSettings["wavfileslocation"];

                // for correct file naming of .wav files, refer to the Key enum
                string filepath = string.Format("{0}{1:c}{2}", folderpath, e.Key.ToString(), ".wav");

                player.SoundLocation = @filepath;

                if (File.Exists(player.SoundLocation))
                { 
                    player.Play();
                }

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