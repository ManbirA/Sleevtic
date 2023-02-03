using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : MonoBehaviour
{
    public GameObject player;
    public Vector3 currTarget;
    public Vector3 originalTarget;
    public float new_x;
    public float new_y;

    public float speed;
    public float waitTime;

    bool wait = true;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        // originalTarget = player.transform.position;
        originalTarget = new Vector3(0, 0.5f, -0.5f);
        new_x = Random.Range(originalTarget.x - 1.0f, originalTarget.x + 1.0f);
        new_y = Random.Range(1.5f, 1.65f);

        currTarget = new Vector3(
            new_x, 
            new_y, 
            originalTarget.z
        );
        gameObject.transform.position = new Vector3(
            new_x, 
            new_y, 
            15f
        );
        
        if (speed == 0) {
            speed = 5f;
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
