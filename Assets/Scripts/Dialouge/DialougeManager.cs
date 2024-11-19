using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class DialougeManager : MonoBehaviour
{

    public TMP_Text nameText;
    public TMP_Text dialougeText;
    


    [SerializeField]public Animator friendAnimator;
    public Animator dialougeAnim;

    DialougeTrigger dialougeTrig;
    
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

        Speak(false);
    }

    
    public void StartDialouge(Dialouge dialouge)
    {
        dialougeAnim.SetBool("IsOpen", true);
        
        Speak(true);

        nameText.text = dialouge.name;

        sentences.Clear();
        

        foreach(string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialouge();
            return;
        }

        string sentence = sentences.Dequeue();
        dialougeText.text = sentence;
    }

    void EndDialouge()
    {
        Debug.Log("Ending dialouge.");
        dialougeAnim.SetBool("IsOpen", false);
        Speak(false);
    }


    public void Speak(bool speaking){
        friendAnimator.SetBool("TalkingWith", speaking);
    }

}
