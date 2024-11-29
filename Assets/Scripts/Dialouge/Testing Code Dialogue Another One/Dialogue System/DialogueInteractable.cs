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
        if(playerInRange)
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
            inConversation = false;
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
    public void TriggerDialogueObject()
    {
        visualCue.SetActive(false);
        dialogueUI.ShowDialogue(dialogueObject);
    }
}
