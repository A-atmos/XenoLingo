using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class Response
{

    public string result;

    public static Response FromJson(string json)
    {
        return JsonUtility.FromJson<Response>(json);
    }

}

[RequireComponent(typeof(AudioSource))]
public class OnPlayButtonClickEvent : MonoBehaviour
{
    public string devnagari_text_field;
    public VirtualButtonBehaviour Vb;
    private Response response;
    private bool get_data = true;


    void PostData()
    {
        
        StartCoroutine(GetAudioClip_Coroutine());
        
    }


    private void Start()
    {  
        Vb.RegisterOnButtonPressed(ButtonTrigger);
    }

    public void ButtonTrigger(VirtualButtonBehaviour Vb)
    {
        Debug.Log(Vb.Pressed);

        if (!Vb.Pressed)
        {
            Debug.Log("Button Pressed");
            PostData();
        }
    }

    IEnumerator GetAudioClip_Coroutine()
    {

        string uri = "http://192.168.106.154:5000/";
        string path = "";
        string pathToAudio = "output";
        string outputFromRequest = "";

        WWWForm form = new WWWForm();
        form.AddField("input_data", devnagari_text_field);

        using (UnityWebRequest request = UnityWebRequest.Post(uri + path, form))
        {
            request.timeout = 10;
            yield return request.SendWebRequest();
            
            if (request.isNetworkError || request.isHttpError)
            {
                
            }
            else
            {
                outputFromRequest = request.downloadHandler.text;

            }

        }

        Debug.Log(outputFromRequest);

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri+pathToAudio+"?filename="+outputFromRequest, AudioType.WAV))
        {
            www.timeout = 10;
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.clip = myClip;
                audioSource.Play();
            }

        }

    }
}