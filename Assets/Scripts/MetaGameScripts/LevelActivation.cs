using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelActivation : MonoBehaviour
{
    public GameObject lever;
    public GameObject objectToActivate;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            objectToActivate.GetComponent<ObjectMovement>().shouldMove = true;
        }
    }
}
