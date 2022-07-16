using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehaviour : MonoBehaviour
{
    public int numSpheres = 10;

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
        if (col.gameObject.name == "Sphere") {
            if (numSpheres > 0) {
                numSpheres -= 1;
                col.gameObject.transform.position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(2.0f, 8.0f), Random.Range(-6.0f, 10.0f));
            } else {
                Debug.Log("Destroy Sphere");
                Destroy(col.gameObject);
            }
        }

    }
}
