using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SphereMovement : MonoBehaviour
{
    public GameObject player;
    public Vector3 currTarget;
    public Vector3 originalTarget;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        originalTarget = player.transform.position;
        currTarget = new Vector3(
            Random.Range(originalTarget.x - 0.20f, originalTarget.x + 0.20f), 
            Random.Range(originalTarget.y + 0.20f, originalTarget.y + 1.75f), 
            originalTarget.z
        );
        gameObject.transform.position = new Vector3(
            Random.Range(-4.0f, 4.0f), 
            Random.Range(2.0f, 4.0f), 
            Random.Range(3.0f, 19.0f)
        );
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currTarget, 5f * Time.deltaTime);
    }

    public IEnumerator GetRequest(string uri)
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

    void OnCollisionEnter(Collision col)
    {
        currTarget = new Vector3(
            Random.Range(originalTarget.x - 0.20f, originalTarget.x + 0.20f), 
            Random.Range(originalTarget.y + 0.20f, originalTarget.y + 1.75f), 
            originalTarget.z
        );

        gameObject.transform.position = new Vector3(
            Random.Range(-4.0f, 4.0f), 
            Random.Range(2.0f, 4.0f), 
            Random.Range(3.0f, 19.0f)
        );
        
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        if (col.gameObject.name == "Sword") {
            ScoreManager.scoreManagerInstance.AddPoint();
        }

        if (col.gameObject.name == "Shield") {
            StartCoroutine(GetRequest("http://192.168.206.205:80/1/on"));
        }

    }
}
