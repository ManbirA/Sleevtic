using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Asteroid_H") {
            if (numSpheres > 1) {
                numSpheres -= 1;
            } else {
                Destroy(col.gameObject);
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(1);
            }
        }

    }
}
