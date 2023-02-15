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
    public SleevticSDK sleeve;

    bool wait = true;

    // Start is called before the first frame update
    void Start()
    {
        // Random.InitState(System.DateTime.Now.Millisecond);

        Random.InitState(42);
        // originalTarget = player.transform.position;
        originalTarget = new Vector3(0, 0.5f, -0.5f);
        new_x = Random.Range(originalTarget.x - 0.5f, originalTarget.x + 0.5f);
        new_y = Random.Range(1.45f, 1.55f);

        SleevticSDK sleeve = new SleevticSDK();
        sleeve.Ip = "localhost";

        currTarget = new Vector3(
            new_x, 
            new_y, 
            originalTarget.z
        );
        gameObject.transform.position = new Vector3(
            new_x, 
            new_y, 
            10f
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
            // Vector3 direction = new Vector3(
            //     0f, 
            //     0f, 
            //     -1f
            // );
            // transform.position += direction.normalized * speed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, currTarget, speed * Time.deltaTime);
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
