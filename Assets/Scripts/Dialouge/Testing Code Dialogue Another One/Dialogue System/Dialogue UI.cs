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

    [SerializeField] private DialogueObjecct finalDialogue;
    
    public DialogueObjecct currDialogue = null;

    private ResponseHandler responseHandler;
    
    private TypeWritterEffect typeWritterEffect;

    private void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        
    }

    public void ShowDialogue(DialogueObjecct dialogueObject)
    {
        currDialogue = dialogueObject;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void ClearResponses() {
        responseHandler.ClearResponses();
    }

    private  IEnumerator StepThroughDialogue(DialogueObjecct dialogueObject)
    {
        dialogueObject.readTimes ++;
        Debug.Log(DialogueInteractable.objectTag);

        // if(dialogueObject.readTimes == 2 && DialogueInteractable.objectTag == "Sign")
        // {
        //     for( int i = 0; i < dialogueObject.SecondaryDialogues.Length; i ++)
        //         {
        //             //string dialogue = dialogueObject.Dialogue[i];
        //             string dialogue = dialogueObject.SecondaryDialogues[i].Sentences;
        //             nameText.text = dialogueObject.SecondaryDialogues[i].CharName;

        //             if(dialogueObject.SecondaryDialogues[i].CharSprite == null)
        //             {
        //                 charSprite.color = new Color(0, 0, 0, 0);
        //             }
        //             else
        //             {
        //                 charSprite.sprite = dialogueObject.SecondaryDialogues[i].CharSprite;
        //                 charSprite.color = Color.white;
        //             }
                    
        //             yield return typeWritterEffect.Run(dialogue, textLabel);

        //             if(i == dialogueObject.SecondaryDialogues.Length - 1 && dialogueObject.HasResponses) break;


        //             yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        //     }
        // }


















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
            Debug.Log(currDialogue.giving);
            if (currDialogue.giving) {

                InventoryController inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
                Debug.Log(inventory.addItem(currDialogue.item, currDialogue.giveNum));

            }
            CloseDialogueBox();
            DialogueInteractable.inConversation = false;
            
            if (dialogueObject.finalDialogueInTown) {
                Debug.Log(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Final");

                transform.position = new Vector3(3f, 3.0f, 3f);
                ShowDialogue(finalDialogue);
            }

            if (dialogueObject.finalDialogueCompletely) {
                GameObject.FindGameObjectWithTag("CanvasFinal").SetActive(true);
            }
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
