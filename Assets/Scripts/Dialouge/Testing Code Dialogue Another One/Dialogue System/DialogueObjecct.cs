using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory.Model;

[CreateAssetMenu(menuName = "Dialgue/DialogueObject")]
public class DialogueObjecct : ScriptableObject
{
   // [SerializeField] [TextArea] private string[] dialogue; //

    [SerializeField] public SentenceText[] sentenceTexts;

    [SerializeField] private Response[] responses;

<<<<<<< Updated upstream
    [SerializeField] public SecondaryDialogue[] secondaryDialogue;
=======
    [SerializeField] public bool finalDialogueInTown = false;

    [SerializeField] public bool finalDialogueCompletely = false;
>>>>>>> Stashed changes

    [SerializeField] public bool givable = false;

    [SerializeField] public bool giving = false;

    [SerializeField] public int giveNum = 0;

    [SerializeField] public ItemSO item;

  //  public string[] Dialogue => dialogue; //

    public SentenceText[] SentenceTexts => sentenceTexts;

    public SecondaryDialogue[] SecondaryDialogues => secondaryDialogue;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;


    public int readTimes = 0;
}
