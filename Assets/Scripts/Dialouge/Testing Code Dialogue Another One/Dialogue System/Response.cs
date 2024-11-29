
using UnityEngine;

[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObjecct dialogueObject;

    public string ResponseText => responseText;
    public DialogueObjecct DialogueObjecct => dialogueObject;

}
