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
    GameObject continueButton;
    GameObject dialougeBox;


    [SerializeField]GameObject contButt;

    [SerializeField]public Animator animator;

    
    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        continueButton = GameObject.FindGameObjectWithTag("Continue");

        Speak(false);
    }

    
    public void StartDialouge(Dialouge dialouge)
    {
        Debug.Log("Starting conversation with " + dialouge.name);
        
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

        contButt.transform.localScale = new Vector3(-10, 0, 0);
        Speak(false);
    }


    public void Speak(bool speaking){
        animator.SetBool("TalkingWith", speaking);
    }

}
