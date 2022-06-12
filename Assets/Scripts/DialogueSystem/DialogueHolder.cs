using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        private IEnumerator dialogueSeq;
        private bool dialogueFinished;
        [SerializeField] private GameObject bordecanvas;

        

        private void OnEnable()
        {
            dialogueSeq = dialogueSequence();
            StartCoroutine(dialogueSeq);
        }

        public void CerrarDialogo(InputAction.CallbackContext context)
        {
                Desactivate();
                gameObject.SetActive(false);
                StopCoroutine(dialogueSeq);
                bordecanvas.SetActive(false);
        }

         public void CerrarDialogoTel()
        {
                Desactivate();
                gameObject.SetActive(false);
                StopCoroutine(dialogueSeq);
                bordecanvas.SetActive(false);
        }

        private IEnumerator dialogueSequence()
        {
            if(!dialogueFinished)
            {

                for(int i = 0; i < transform.childCount - 1; i++)
                {
                    Desactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                    yield return new WaitUntil(() => transform.GetChild(i).GetComponent<DialogueLine>().finished);
                }
            }else
            {
                int index = transform.childCount - 1;
                Desactivate();
                transform.GetChild(index).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(index).GetComponent<DialogueLine>().finished);
            }

            dialogueFinished = true;
            gameObject.SetActive(false);
            bordecanvas.SetActive(false);
        }

        private void Desactivate()
        {
           for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }  
        }
    }
}
