using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBase : MonoBehaviour
    {
        public bool finished{get; protected set;}

        protected IEnumerator WriteText(string input, Text textHolder,  Font textFont, float delay)
        {
            textHolder.font = textFont;
            for(int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
           }
            yield return new WaitUntil(() => Input.GetKey(KeyCode.E));
            finished = true;

        }

    }
}
