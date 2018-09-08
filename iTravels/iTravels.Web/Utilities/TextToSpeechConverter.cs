using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Speech;
using System.Speech.Synthesis;

namespace iTravels.Web.Utilities
{
    public class TextToSpeechConverter
    {
        void Test()
        {
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();

            // Speak a string.
            synth.Speak("This example demonstrates a basic use of Speech Synthesizer");


            ////
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult); // to change VoiceGender and VoiceAge check out those links below
            synthesizer.Volume = 100;  // (0 - 100)
            synthesizer.Rate = 0;     // (-10 - 10)
                                      // Synchronous
            synthesizer.Speak("Now I'm speaking, no other function'll work");
            // Asynchronous
            // here args = pran
            string args = "pran";
            synthesizer.SpeakAsync("Welcome" + args);
        }

        void GoogleTest()
        {
            ////            Recently Google published Google Cloud Text To Speech.

            ////.NET Client version of Google.Cloud.TextToSpeech can be found here: https://github.com/jhabjan/Google.Cloud.TextToSpeech.V1

            ////            Nuget: Install - Package JH.Google.Cloud.TextToSpeech.V1

            ////Here is short example how to use the client:
            //uncomment below before use
            //GoogleCredential credentials =
            //    GoogleCredential.FromFile(Path.Combine(Program.AppPath, "jhabjan-test-47a56894d458.json"));

            //TextToSpeechClient client = TextToSpeechClient.Create(credentials);

            //SynthesizeSpeechResponse response = client.SynthesizeSpeech(
            //    new SynthesisInput()
            //    {
            //        Text = "Google Cloud Text-to-Speech enables developers to synthesize natural-sounding speech with 32 voices"
            //    },
            //    new VoiceSelectionParams()
            //    {
            //        LanguageCode = "en-US",
            //        Name = "en-US-Wavenet-C"
            //    },
            //    new AudioConfig()
            //    {
            //        AudioEncoding = AudioEncoding.Mp3
            //    }
            //);

            //string speechFile = Path.Combine(Directory.GetCurrentDirectory(), "sample.mp3");

            //File.WriteAllBytes(speechFile, response.AudioContent);
        }
        private string textToRead = "This example demonstrates a basic use of Speech Synthesizer";
        SpeechSynthesizer reader = new SpeechSynthesizer();
        void Speak()
        {
            if (!string.IsNullOrWhiteSpace(textToRead))
            {
                reader.Dispose();
                reader = new SpeechSynthesizer();
                reader.SpeakAsync(textToRead);

            }
        }
        void Pause()
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Speaking)
                {
                    reader.Pause();
                }
            }
        }
        void Resume()
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Paused)
                {
                    reader.Resume();
                }
            }
            
        }
        void Stop() {
            if (reader!=null)
            {
                reader.Dispose();
            }
        }
    }
}