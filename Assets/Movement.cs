using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : MonoBehaviour
{
    public GameObject player;
    public Vector3 currTarget;
    public Vector3 originalTarget;
    public float speed;
    public float waitTime;

    bool wait = true;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        // originalTarget = player.transform.position;
        originalTarget = new Vector3(0, 0.5f, -0.5f);
        currTarget = new Vector3(
            Random.Range(originalTarget.x - 0.20f, originalTarget.x + 0.20f), 
            Random.Range(originalTarget.y + 0.20f, originalTarget.y + 1.75f), 
            originalTarget.z
        );
        gameObject.transform.position = new Vector3(
            Random.Range(-4.0f, 4.0f), 
            Random.Range(2.0f, 4.0f), 
            15f
        );
        
        if (speed == 0) {
            speed = 5f;
        }

        if (speed == 0) {
            waitTime = Random.Range(3f, 10f);
        }

       StartCoroutine( WaitHandler() );
    }

    // Update is called once per frame
    void Update()
    {
        if (!wait) {
            transform.position = Vector3.MoveTowards(transform.position, currTarget, speed * Time.deltaTime);
        }
    }

    public IEnumerator WaitHandler()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        wait = true;

        yield return new WaitForSeconds(waitTime);

        gameObject.GetComponent<Renderer>().enabled = true;
        wait = false;
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

    public virtual void OnCollisionEnter(Collision col)
    {
        Debug.Log("This is the base class");
    }
}