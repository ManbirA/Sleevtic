using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(player.transform);
        // transform.position += transform.forward * 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Player") {
            Debug.Log("Collision detected");
            Destroy(col.gameObject);
        }
    }
    
}
