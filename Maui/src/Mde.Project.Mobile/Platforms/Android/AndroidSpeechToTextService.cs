#if ANDROID
using Android.Content;
using Android.OS;
using Android.Speech;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Services.Interfaces;

namespace Mde.Project.Mobile.Platforms.Android
{
    public class AndroidSpeechToTextService : ISpeechToTextService
    {
        SpeechRecognizer? _recognizer;
        Action<string>? _callback;

        public async Task StartListening(Action<string> onResult)
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
            {
                _callback?.Invoke("Microphone permission denied.");
                return;
            }

            _callback = onResult;

            var activity = Platform.CurrentActivity;
            var intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            intent.PutExtra(RecognizerIntent.ExtraPartialResults, true);

            intent.PutExtra(RecognizerIntent.ExtraLanguage, "en-US");

            _recognizer = SpeechRecognizer.CreateSpeechRecognizer(activity);
            _recognizer.SetRecognitionListener(new Listener(this));

            _recognizer.StartListening(intent);

        }
        public void StopListening()
        {
            _recognizer?.StopListening();
            _recognizer?.Destroy();
            _recognizer = null;
        }
        void SetResult(string text)
        {
            _callback?.Invoke(text);
        }
        class Listener : Java.Lang.Object, IRecognitionListener
        {
            private readonly AndroidSpeechToTextService _service;

            public Listener(AndroidSpeechToTextService service)
            {
                _service = service;
            }

            public void OnResults(Bundle result)
            {
                var matches = result.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
                var text = matches?.FirstOrDefault() ?? string.Empty;

                _service.SetResult(text);
                _service.StopListening();
            }
            public void OnError(SpeechRecognizerError error)
            {
                System.Diagnostics.Debug.WriteLine($"Speech error: {error}");
                _service.SetResult($"Error: {error}");
            }
            public void OnReadyForSpeech(Bundle @params) { System.Diagnostics.Debug.WriteLine($"Ready for speech"); }
            public void OnBeginningOfSpeech() { System.Diagnostics.Debug.WriteLine($"Begin speech"); }
            public void OnRmsChanged(float rmsdB) { }
            public void OnBufferReceived(byte[] buffer) { }
            public void OnEndOfSpeech() { System.Diagnostics.Debug.WriteLine($"End speech"); }
            public void OnPartialResults(Bundle partialResults)
            {
                var matches = partialResults.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
                var text = matches?.FirstOrDefault();

                System.Diagnostics.Debug.WriteLine($"Partial: {text}");
            }
            public void OnEvent(int eventType, Bundle @params) { }

        }
    }
}
#endif 