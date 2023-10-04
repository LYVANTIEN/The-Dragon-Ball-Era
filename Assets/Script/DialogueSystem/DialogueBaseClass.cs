using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        protected IEnumerator WriteText(string input, Text textHolder, Font textFont, Color textColor, float delay, float delayBetweenLines, AudioClip sound)
        {
            textHolder.font = textFont;
            textHolder.color = textColor;

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
              SoundManager.instance.playSound(sound);
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitUntil(()  => Input.GetMouseButton(0));
            finished = true;
        }
    }
}

