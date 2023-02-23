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

    public IEnumerator ActionOne() {
      string requestUrl = string.Format("http://{0}/1/on",Ip);
      yield return GetRequest(requestUrl);
    }

    public IEnumerator ActionTwo() {
      string requestUrl = string.Format("http://{0}/2/on",Ip);
      yield return GetRequest(requestUrl);
    }

    public IEnumerator ActionThree() {
      string requestUrl = string.Format("http://{0}/3/on",Ip);
      yield return GetRequest(requestUrl);
    }

    public IEnumerator ActionFour() {
      string requestUrl = string.Format("http://{0}/4/on",Ip);
      yield return GetRequest(requestUrl);
    }

    public IEnumerator ActionFive() {
      string requestUrl = string.Format("http://{0}/5/on",Ip);
      yield return GetRequest(requestUrl);
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
