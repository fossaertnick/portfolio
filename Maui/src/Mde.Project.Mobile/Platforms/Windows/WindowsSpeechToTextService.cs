using Mde.Project.Mobile.Core.Services.Interfaces;
using Windows.Globalization;
using Windows.Media.SpeechRecognition;

namespace Mde.Project.Mobile.Platforms.Windows
{
    public class WindowsSpeechToTextService : ISpeechToTextService
    {
        private SpeechRecognizer? _speechRecognizer;
        private bool _isListening;

        public async Task StartListening(Action<string> onResult)
        {
            try
            {
                if (_isListening) return;

                _isListening = true;

                _speechRecognizer = new SpeechRecognizer(new Language("en-US"));
                await _speechRecognizer.CompileConstraintsAsync();

                _speechRecognizer.UIOptions.AudiblePrompt = "Speak now";
                _speechRecognizer.UIOptions.ExampleText = "Say something...";

                var result = await _speechRecognizer.RecognizeAsync();

                System.Diagnostics.Debug.WriteLine(result.Status);
                System.Diagnostics.Debug.WriteLine(result.Text);

                if (result.Status == SpeechRecognitionResultStatus.Success)
                {
                    onResult?.Invoke(result.Text);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                _isListening = false;
            }
        }

        public void StopListening()
        {
            _isListening = false;
            _speechRecognizer?.Dispose();
            _speechRecognizer = null;
        }
    }
}
