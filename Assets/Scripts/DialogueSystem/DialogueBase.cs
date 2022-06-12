using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace DialogueSystem
{
    public class DialogueBase : MonoBehaviour
    {
        public bool finished{get; protected set;}
        public bool pasar = true;


        protected IEnumerator WriteText(string input, Text textHolder,  Font textFont, float delay)
        {
            textHolder.font = textFont;
            for(int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
           }
            if(SystemInfo.deviceType == DeviceType.Handheld)
            {
                yield return new WaitUntil(() => pasar);
            }else{
                yield return new WaitUntil(() => InputSystem.GetDevice<Keyboard>()[Key.E].wasReleasedThisFrame);
            }

            finished = true;

        }

    }
}
