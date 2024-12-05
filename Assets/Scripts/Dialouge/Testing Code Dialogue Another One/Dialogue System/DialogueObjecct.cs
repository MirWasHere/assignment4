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

    [SerializeField] public bool givable = false;

    [SerializeField] public bool giving = false;

    [SerializeField] public ItemSO item;

  //  public string[] Dialogue => dialogue; //

    public SentenceText[] SentenceTexts => sentenceTexts;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}
