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
            Debug.Log("In Range: " + playerInRange + "\nIn Conversation: " + inConversation);
            visualCue.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E)){
                TriggerDialogueObject();
                inConversation = true;
            }
        }
        else
        {
            Debug.Log("In Range: " + playerInRange + "\nIn Conversation: " + inConversation);
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

    public bool TriggerDialogueObject(DialogueObjecct dialogue, DialogueObjecct dontGive) {

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
        else if(dialogueUI.currDialogue != null && !dialogueUI.currDialogue.givable)
        {
            // make the character sprite = to character of previously running dialogue
            dontGive.sentenceTexts[0].charSprite = dialogueUI.currDialogue.sentenceTexts[0].charSprite;
            dialogueUI.ShowDialogue(dontGive);
            // reset the character sprite back to nothing
            dontGive.sentenceTexts[0].charSprite = null;
            return false;
        }

        // if dialogue is running and you CAN give, run dialogue of the object (which was passed in)
        dialogueUI.ShowDialogue(dialogueObject);
        return true;
    }
}
