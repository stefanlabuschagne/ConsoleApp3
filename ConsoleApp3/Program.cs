using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Speech.V1;

namespace GoogleCloudSamples
{
    public class QuickStart
    {
        // The name of the local audio file to transcribe
        public static string DEMO_FILE = @"C:\GOOGLE_APP_CREDS\Videoplayback_out.mp4";

        public static string DEMO_FILE_TOO = @"gs://vids-bucket/videoplayback_out.mp4";


        public static void Main(string[] args)
        {

            //var speech = SpeechClient.Create();
            //var response = speech.Recognize(new RecognitionConfig()
            //{
            //    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,               
            //    SampleRateHertz = 16000,
            //    LanguageCode  = "af-ZA"
            //    // Model =  video,   // Will automatically Choose!

            //}, RecognitionAudio.FromFile(DEMO_FILE));
            //foreach (var result in response.Results)
            //{
            //    foreach (var alternative in result.Alternatives)
            //    {
            //        Console.WriteLine(alternative.Transcript);
            //    }
            //}

            var speech = SpeechClient.Create();
            var longOperation = speech.LongRunningRecognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = "af-ZA",
            }, RecognitionAudio.FromStorageUri(DEMO_FILE_TOO));
            longOperation = longOperation.PollUntilCompleted();
            var response = longOperation.Result;
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine(alternative.Transcript);
                }
            }
            Console.WriteLine("Press any Key..");
            Console.ReadLine();
            // return 0;


        }
    }
}
