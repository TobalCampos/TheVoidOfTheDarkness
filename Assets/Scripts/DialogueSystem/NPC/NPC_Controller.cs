using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject bordecanvas;
    
    
    public void ActivateDialogue()
    {
        dialogue.SetActive(true);
        bordecanvas.SetActive(true);
    }

    public bool DialogueActive()
    {
        return dialogue.activeInHierarchy;
    }
}
