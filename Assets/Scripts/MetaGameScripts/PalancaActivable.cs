using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalancaActivable : MonoBehaviour
{
    public GameObject lever;
    public GameObject objectToActivate;

    [SerializeField]
    private AudioClip SoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectToActivate.SetActive(false);
            soundManager.instance.PlaySound(SoundEffect);
        }
    }
}
