using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanetMovement : AsteroidMovement
{
    public override void OnCollisionEnter(Collision col)
    {
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

        if (col.gameObject.name == "Sword") {
            ScoreManager.scoreManagerInstance.AddPoint();
        }

        if (col.gameObject.name == "Shield") {
            StartCoroutine(GetRequest("http://192.168.206.205:80/1/on"));
            ScoreManager.scoreManagerInstance.ResetCombo();
        }

        StartCoroutine( WaitHandler() );
    }
}
