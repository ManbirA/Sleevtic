using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public GameObject player;
    public int numSpheres = 10;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 5f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Person") {
            if (numSpheres > 0) {
                numSpheres -= 1;
                target = new Vector3(Random.Range(target.x - 0.25f, target.x + 0.25f), Random.Range(target.y - 0.25f, target.y + 0.25f), target.z);
            }
        }

    }
}
