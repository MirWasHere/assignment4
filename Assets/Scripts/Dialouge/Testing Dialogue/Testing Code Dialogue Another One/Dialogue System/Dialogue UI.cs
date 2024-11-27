using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private Image charSprite;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private DialogueObjecct testDialogue;
    

    private ResponseHandler responseHandler;
    
    private TypeWritterEffect typeWritterEffect;

    private void Start()
    {
        typeWritterEffect = GetComponent<TypeWritterEffect>();
        responseHandler = GetComponent<ResponseHandler>();
        CloseDialogueBox();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObjecct dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private  IEnumerator StepThroughDialogue(DialogueObjecct dialogueObject)
    {
        for( int i = 0; i < dialogueObject.SentenceTexts.Length; i ++)
        {
            //string dialogue = dialogueObject.Dialogue[i];
            string dialogue = dialogueObject.SentenceTexts[i].Sentences;
            nameText.text = dialogueObject.SentenceTexts[i].CharName;
            charSprite.sprite = dialogueObject.SentenceTexts[i].CharSprite;
            
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
            CloseDialogueBox();
        }
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = "";
    }

}
