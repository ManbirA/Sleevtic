using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SleevticSDK : MonoBehaviour
{
    public string Ip { get; set;}
    // Start is called before the first frame update
    void Start()
    {
      
    }

    public int ActionOne() {
      string requestUrl = string.Format("http://{0}/1/on",Ip);
      StartCoroutine(GetRequest(requestUrl));
      return 0;
    }

    public int ActionTwo() {
      string requestUrl = string.Format("http://{0}/2/on",Ip);
      StartCoroutine(GetRequest(requestUrl));
      return 0;
    }

    int ActionThree() {
      string requestUrl = string.Format("http://{0}/3/on",Ip);
      StartCoroutine(GetRequest(requestUrl));
      return 0;
    }

    int ActionFour() {
      string requestUrl = string.Format("http://{0}/4/on",Ip);
      StartCoroutine(GetRequest(requestUrl));
      return 0;
    }

    int ActionFive() {
      string requestUrl = string.Format("http://{0}/5/on",Ip);
      StartCoroutine(GetRequest(requestUrl));
      return 0;
    }

    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
