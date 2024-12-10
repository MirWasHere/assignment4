using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialougeTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    public Dialouge dialouge;



    private bool playerInRange;
    private Animator animator;    

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        // When the player is in range
        if(playerInRange)
        {
            visualCue.gameObject.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E)){
                TriggerDialouge();
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

    public void TriggerDialouge()
    {
        FindObjectOfType<DialougeManager>().StartDialouge(dialouge);
        
    }

}
