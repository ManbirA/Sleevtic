using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class AsteroidMovement : MonoBehaviour
{
    public GameObject player;
    public Vector3 currTarget;
    public Vector3 originalTarget;
    public float speed;

    int wait = 0;

    string[] objects = {"Asteroid_A", "Asteroid_E", "Asteroid_H", "Supernove_A", "Gas_Planet_A", "Sun"};

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
        
        if (speed == 0) {
            speed = 5f;
        }

        wait = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (wait < 0) { // move object
            transform.position = Vector3.MoveTowards(transform.position, currTarget, speed * Time.deltaTime);
        } else if (wait == 0) { // changing state from not visible to visible
            wait -= 1;
            gameObject.SetActive(true);
        } else { // wait before renderng again
            wait -= 1;
        }
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
        if (objects.Contains(col.gameObject.name)) {
            return;
        }

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

        gameObject.SetActive(false);
        wait = 100;

        if (col.gameObject.name == "Shield") {
            StartCoroutine(GetRequest("http://192.168.206.205:80/1/on"));
        } else {
            // ScoreManager.scoreManagerInstance.ResetCombo();
        }
    }
}