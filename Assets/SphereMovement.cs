using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public GameObject player;
    public int numSpheres = 10;
    public Vector3 currTarget;
    public Vector3 originalTarget;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        originalTarget = player.transform.position;
        currTarget = new Vector3(
            Random.Range(originalTarget.x - 0.20f, originalTarget.x + 0.20f), 
            Random.Range(originalTarget.y - 0.50f, originalTarget.y), 
            originalTarget.z
        );
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currTarget, 5f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Person") {
            if (numSpheres > 0) {
                numSpheres -= 1;
                currTarget = new Vector3(
                    Random.Range(originalTarget.x - 0.20f, originalTarget.x + 0.20f), 
                    Random.Range(originalTarget.y - 0.50f, originalTarget.y), 
                    originalTarget.z
                );
                GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                // TODO: what happens if player got hit
            }
        }

        if (col.gameObject.name == "Sword") {
            ScoreManager.scoreManagerInstance.AddPoint();
            numSpheres -= 1;
            currTarget = new Vector3(
                Random.Range(originalTarget.x - 0.20f, originalTarget.x + 0.20f), 
                Random.Range(originalTarget.y - 0.50f, originalTarget.y), 
                originalTarget.z
            );
            GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

    }
}
