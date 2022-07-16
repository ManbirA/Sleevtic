using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("testing");
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision start");
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Sphere") {
            Debug.Log("Collision detected");
            Destroy(col.gameObject);
        }

    }
}
