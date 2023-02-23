using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanetMovement : Movement
{
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
            StartCoroutine(sleeve.ActionTwo());
            ScoreManager.scoreManagerInstance.AddPoint();
        }

        if (col.gameObject.name == "Shield") {
            StartCoroutine(sleeve.ActionOne());
            ScoreManager.scoreManagerInstance.ResetCombo();
        }

        StartCoroutine( WaitHandler() );
    }
}
