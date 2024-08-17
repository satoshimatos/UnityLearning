using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableLadder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("LadderTouched");
        }
    }
}
