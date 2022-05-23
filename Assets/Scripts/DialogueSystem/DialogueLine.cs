using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBase
    {
        private Text textHolder;
        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Font textFont;

        [Header ("Time parameters")]
        [SerializeField] private float delay;

        [Header ("Character")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        private IEnumerator lineAppear;


        private void Awake()
        {
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void OnEnable()
        {
            ResetLines();
            lineAppear = WriteText(input, textHolder, textFont, delay);
            StartCoroutine(lineAppear);
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(1))
            {
                if(textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }
                else
                    finished = true;
            }
        }

        private void ResetLines()
        {
            textHolder = GetComponent<Text>();
            textHolder.text = "";
            finished = false; 
        }
    }
}
