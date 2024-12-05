using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueInteractable : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private DialogueObjecct dialogueObject;

    public DialogueUI dialogueUI;
    
    private bool playerInRange;
    public static bool inConversation;
    // Add animator if time

    // private void Start(){
    //     dialogueObject = new DialogueObjecct[10];
    // }

    private void Awake()
    {
        playerInRange = false;
        inConversation = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        // When the player is in range
        if(playerInRange && !inConversation)
        {
            visualCue.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E)){
                TriggerDialogueObject();
                inConversation = true;
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
    
    // Begins dialogue given by trigger
    public bool TriggerDialogueObject()
    {
        visualCue.SetActive(false);
        dialogueUI.ShowDialogue(dialogueObject);
        return true;
    }

    public bool TriggerDialogueObject(DialogueObjecct dialogue, DialogueObjecct dontGive, string name) {

        dialogueObject = dialogue;
        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUI>();
        
        // if dialogue is currently NOT running, don't let the character give anything
        if (dialogueUI.currDialogue == null) 
        {
            return false;
        }
        // if dialogue IS running, but it's not time to give yet, 
        // don't let the character give anything + make the character say:
        // "DONT GIVE ME ANYTHING"
        else if(dialogueUI.currDialogue != null && !dialogueUI.currDialogue.givable 
            && !name.Equals(dialogueUI.currDialogue.sentenceTexts[0].CharName))
        {
            // make the character sprite = to character of previously running dialogue
            dontGive.sentenceTexts[0].setCharSprite(dialogueUI.currDialogue.sentenceTexts[0].CharSprite);
            dontGive.sentenceTexts[0].setCharName(dialogueUI.currDialogue.sentenceTexts[0].CharName);
            dialogueUI.ShowDialogue(dontGive);
            return false;
        }

        // if dialogue is running and you CAN give, run dialogue of the object (which was passed in)
        dialogueUI.ShowDialogue(dialogueObject);
        return true;
    }
}
