using Mde.Project.Mobile.Core.Services.Interfaces;
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
                _speechRecognizer = new SpeechRecognizer();
                await _speechRecognizer.CompileConstraintsAsync();
                var result = await _speechRecognizer.RecognizeAsync();
                if(result.Status == SpeechRecognitionResultStatus.Success)
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
