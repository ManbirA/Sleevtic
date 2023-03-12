using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlanetMovement : Movement
{
    int numObjects = 1000;

    public override void OnCollisionEnter(Collision col)
    {   
        new_x = Random.Range(originalTarget.x, originalTarget.x + 0.75f);
        new_y = Random.Range(1.35f, 1.5f);

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

        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        
        if (col.gameObject.name == "Sword") {
            ScoreManager.scoreManagerInstance.AddPoint();
        }

        if (col.gameObject.name == "Shield") {
            StartCoroutine(sleeve.ActionTwo());
            ScoreManager.scoreManagerInstance.ResetCombo();
        }

        if (numObjects > 0) {
            numObjects = numObjects - 1;
        } else {
            SceneManager.LoadScene(1);
        }

        StartCoroutine( WaitHandler() );
    }
}
