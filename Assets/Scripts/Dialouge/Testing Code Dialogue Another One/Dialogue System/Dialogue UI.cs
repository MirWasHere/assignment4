using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Inventory;
using Inventory.Model;
using UnityEngine.SceneManagement;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private Image charSprite;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameText;
    
    public DialogueObjecct currDialogue = null;

    private ResponseHandler responseHandler;
    
    private TypeWritterEffect typeWritterEffect;

    //private DialogueInteractable dialogueInteractable;

    private void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
       // dialogueInteractable = GetComponent<DialogueInteractable>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        
    }

    public void ShowDialogue(DialogueObjecct dialogueObject)
    {
        if (dialogueObject.secondaryDialogue != null && dialogueObject.readTimes >= 1) 
        {
            currDialogue = dialogueObject.secondaryDialogue;
            dialogueBox.SetActive(true);
            StartCoroutine(StepThroughDialogue(dialogueObject.secondaryDialogue));
        }
        else 
        {
            currDialogue = dialogueObject;
            dialogueBox.SetActive(true);
            //dialogueObject.readTimes += 1;
            StartCoroutine(StepThroughDialogue(dialogueObject));
        }

    }

    public void ClearResponses() {
        responseHandler.ClearResponses();
    }

    private  IEnumerator StepThroughDialogue(DialogueObjecct dialogueObject)
    {
        if (dialogueObject.secondaryDialogue != null)
            dialogueObject.readTimes = 1;
        for( int i = 0; i < dialogueObject.SentenceTexts.Length; i ++)
        {
            //string dialogue = dialogueObject.Dialogue[i];
            string dialogue = dialogueObject.SentenceTexts[i].Sentences;
            nameText.text = dialogueObject.SentenceTexts[i].CharName;

            if(dialogueObject.SentenceTexts[i].CharSprite == null)
            {
                charSprite.color = new Color(0, 0, 0, 0);
            }
            else
            {
                charSprite.sprite = dialogueObject.SentenceTexts[i].CharSprite;
                charSprite.color = Color.white;
            }
            
            yield return typeWritterEffect.Run(dialogue, textLabel);

            if(i == dialogueObject.SentenceTexts.Length - 1 && dialogueObject.HasResponses) break;


            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            
        }
        if(dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            if (currDialogue != null)
                Debug.Log(currDialogue.giving);
            if (currDialogue != null && currDialogue.giving) {

                InventoryController inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
                Debug.Log(inventory.addItem(currDialogue.item, currDialogue.giveNum));

            }
            if (dialogueObject.finalDialogueInTown) 
            {
                SceneManager.LoadScene("Final");
                GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(7.65f, 7.9f, 12f);
                GameObject.FindGameObjectWithTag("Player").transform.Rotate(0, 90, 0);
            }
            if (dialogueObject.finalDialogueCompletely) 
            {
                Debug.Log(GameObject.FindGameObjectWithTag("Final"));
                GameObject.FindGameObjectWithTag("Final").transform.GetChild(0).gameObject.SetActive(true);
            }
            CloseDialogueBox();
            
            DialogueInteractable.inConversation = false;
            
        }
        
        Debug.Log(dialogueObject.readTimes);
    }

    private void CloseDialogueBox()
    {
        currDialogue = null;
        dialogueBox.SetActive(false);
        textLabel.text = "";

    }


}
