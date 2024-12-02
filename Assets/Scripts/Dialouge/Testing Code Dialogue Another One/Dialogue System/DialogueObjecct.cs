using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialgue/DialogueObject")]
public class DialogueObjecct : ScriptableObject
{
   // [SerializeField] [TextArea] private string[] dialogue; //

    [SerializeField] private SentenceText[] sentenceTexts;

    [SerializeField] private Response[] responses;

  //  public string[] Dialogue => dialogue; //

    public SentenceText[] SentenceTexts => sentenceTexts;

    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}
