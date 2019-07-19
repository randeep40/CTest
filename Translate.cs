using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        // This sample requires C# 7.1 or later for async/await.
        // Async call to the Translator Text API
        static public async Task TranslateTextRequest(string subscriptionKey, string host, string route, string inputText)
        {

            string filepath = "C:/Users/Administrator/Documents/Visual Studio 2013/Projects/Jsontoobj/file.json";
            
            using (StreamReader r = new StreamReader(filepath))
            {
                string result = string.Empty; 
                var response = r.ReadToEnd();

                result = response.ToString();
                // Deserialize the response using the classes created earlier.
                TranslationResult[] deserializedOutput = JsonConvert.DeserializeObject<TranslationResult[]>(result);
                // Iterate over the deserialized results.
                foreach (TranslationResult o in deserializedOutput)
                {
                    // Print the detected input language and confidence score.
                    Console.WriteLine("Detected input language: {0}\nConfidence score: {1}\n", o.DetectedLanguage.Language, o.DetectedLanguage.Score);
                    // Iterate over the results and print each translation.
                    foreach (Translation t in o.Translations)
                    {
                        Console.WriteLine("Translated to {0}: {1}", t.To, t.Text);
                    }
                }
            }
        }
        
        static async Task Main(string[] args)
        {
            // This is our main function.
            // Output languages are defined in the route.
            // For a complete list of options, see API reference.
            // https://docs.microsoft.com/azure/cognitive-services/translator/reference/v3-0-translate
            string host = "https://api.cognitive.microsofttranslator.com";
            string route = "/translate?api-version=3.0&to=de&to=it&to=ja&to=th";
            string subscriptionKey = "YOUR_TRANSLATOR_TEXT_KEY_GOES_HERE";
            // Prompts you for text to translate. If you'd prefer, you can
            // provide a string as textToTranslate.
            Console.Write("Type the phrase you'd like to translate? ");
            string textToTranslate = Console.ReadLine();
            await TranslateTextRequest(subscriptionKey, host, route, textToTranslate);
        }
    }
}
