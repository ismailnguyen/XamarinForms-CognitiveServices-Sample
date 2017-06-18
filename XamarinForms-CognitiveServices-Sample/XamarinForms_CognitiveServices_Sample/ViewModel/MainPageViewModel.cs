using Microsoft.ProjectOxford.Emotion;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms_CognitiveServices_Sample.Model;

namespace XamarinForms_CognitiveServices_Sample.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private const string COGNITIVE_SERVICE_EMOTION_KEY = "d515e9da362b48a2a37ec1159c5f9269";

        private EmotionServiceClient EmotionServiceClient { get; set; }
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
            EmotionServiceClient = new EmotionServiceClient(COGNITIVE_SERVICE_EMOTION_KEY);

            EmotionStart = new Command(StartEmotionAnalysis);
        }

        private async void StartEmotionAnalysis()
        {
            var photoStream = await TakePhoto();

            if (photoStream == null)
                return;

            AnalyzePhoto(photoStream);
        }

        private async Task<Stream> TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable
                    || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            var photoOptions = new StoreCameraMediaOptions
            {
                CompressionQuality = 100,
                PhotoSize = PhotoSize.Full,
                Directory = "EmotionPhotos",
                Name = $"{DateTime.UtcNow}.jpg"
            };

            var photoShot = await CrossMedia.Current.TakePhotoAsync(photoOptions);

            if (photoShot == null) return null;

            PhotoSource = ImageSource.FromFile(photoShot.Path);

            return photoShot.GetStream();
        }

        private async void AnalyzePhoto(Stream photoStream)
        {
            var emotionRecognizition = await EmotionServiceClient.RecognizeAsync(photoStream);

            Emotion = new Emotion()
            {
                Anger = emotionRecognizition[0].Scores.Anger,
                Contempt = emotionRecognizition[0].Scores.Contempt,
                Disgust = emotionRecognizition[0].Scores.Disgust,
                Fear = emotionRecognizition[0].Scores.Fear,
                Happiness = emotionRecognizition[0].Scores.Happiness,
                Neutral = emotionRecognizition[0].Scores.Neutral,
                Sadness = emotionRecognizition[0].Scores.Sadness,
                Surprise = emotionRecognizition[0].Scores.Surprise
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
