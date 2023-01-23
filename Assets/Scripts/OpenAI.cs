using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Data
{
    public Data(string prompt)
    {
        this.prompt = prompt;
    }
    public string model = "text-davinci-003";
    public string prompt;
    public float temperature = 0.5f;
    public int max_tokens = 2048;
}

public class OpenAI : MonoBehaviour
{
    Data data;
    private CompetitionResponse response;
    private StringBuilder promptBuilder;
    private StringBuilder anwerBuilder;   

    private readonly string apikey = {}; // paste your sectet key

    private void Start()
    {
        promptBuilder = new StringBuilder();
        anwerBuilder = new StringBuilder();
        response = new CompetitionResponse
        {
            choices = new List<Choise>(),
            usage = new Usage()
        };
        CreateBasePrompt();
    }

    public void CreateBasePrompt()
    {
        promptBuilder.AppendLine("a chat bot named fox who knows everything in the world and loves to help in difficult situations. He is a true friend and helper." +
            "he knows all languages and he is a PhD in all sciences" +
            "Every answer start with Fox: ");
        promptBuilder.AppendLine();
        data = new Data(promptBuilder.ToString());
        SendRequest();
    }

    public void CreateData(string prompt)
    {
        promptBuilder.AppendLine("You: "+prompt);


        data = new Data(promptBuilder.ToString());
        ChatUI.TextUpdated.Invoke("\nYou: " + prompt+"\n"+"\n");
        SendRequest();
    }

    public void SendRequest()
    {
        string json = JsonUtility.ToJson(data);
        StartCoroutine(MakePostRequest(json));
    }    

    public IEnumerator MakePostRequest(string json)
    {

        using (var request = new UnityWebRequest("https://api.openai.com/v1/completions", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apikey);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                yield break;
            }
            else
            {
                string responseJson = request.downloadHandler.text;

                response = JsonUtility.FromJson<CompetitionResponse>(responseJson);

                ChatUI.TextUpdated.Invoke(response.choices[0].text+"\n");
                promptBuilder.AppendLine(response.choices[0].text);
            }
        }                
    }
}
