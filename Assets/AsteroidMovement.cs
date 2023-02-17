using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AsteroidMovement : Movement
{
    public override void OnCollisionEnter(Collision col)
    {
        new_x = Random.Range(originalTarget.x - 0.75f, originalTarget.x);
        new_y = Random.Range(1.35f, 1.55f);

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

        if (col.gameObject.name == "Sword") {
            ScoreManager.scoreManagerInstance.ResetCombo();
        }

        if (col.gameObject.name == "Shield") {
            // sleeve.ActionTwo();
            StartCoroutine(GetRequest("http://192.168.206.205:80/1/on"));
        }

        StartCoroutine( WaitHandler() );
    }
}
