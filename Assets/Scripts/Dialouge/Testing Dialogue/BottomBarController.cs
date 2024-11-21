using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomBarControllers : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;

    private int sentenceIndex = -1;
    public StoryScene currentScene;
    private string NPCname;


    private State state = State.COMPLETED;

    private enum State
    {
        PLAYING, COMPLETED
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
    }

    private IEnumerator TypeText(string text)
    {
        state = State.PLAYING;
        int wordIndex = 0;

        while(state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            yield return new WaitForSeconds(0.05f);
            if(++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }    
    }
}
