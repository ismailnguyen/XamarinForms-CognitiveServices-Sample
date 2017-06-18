using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XamarinForms_CognitiveServices_Sample.Model;

namespace XamarinForms_CognitiveServices_Sample.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private const string COGNITIVE_SERVICE_EMOTION_KEY = "d515e9da362b48a2a37ec1159c5f9269";

        public Command EmotionStart { get; set; }

        private ImageSource photoSource;
        public ImageSource PhotoSource
        {
            get
            {
                return photoSource;
            }
            set
            {
                photoSource = value;
                OnPropertyChanged();
            }
        }

        private Emotion emotion;
        public Emotion Emotion
        {
            get
            {
                return emotion;
            }
            set
            {
                emotion = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            EmotionStart = new Command(AnalyzePhoto);
        }

        private void AnalyzePhoto()
        {
            //TODO analyse photo with Cognitive Services
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
