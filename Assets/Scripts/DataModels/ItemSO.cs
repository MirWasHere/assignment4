using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemSO : ScriptableObject, IItemAction
    {
        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        public string ActionName => "Give Item";

        [field: SerializeField]
        public AudioClip actionSFX {get; private set;}

        [field: SerializeField]
        public DialogueObjecct dialogueObject;

        [field: SerializeField]
        public DialogueObjecct dontGiveMe;

        private DialogueInteractable dialogueInteractable;

        [field: SerializeField]
        public string characterName;

        [field: SerializeField]
        public string characterName2;

        public bool PerformAction(GameObject character)
        {
            // code to give to merchant
            // create another method on dialogue code end, and call it by passing item
            // so smth like
            // if (dialogueGive(this.Name) == true)
            // return true
            // return false outside of the if

            Debug.Log(GameObject.FindGameObjectWithTag("Trigger"));

            Debug.Log(GameObject.FindGameObjectWithTag("Trigger").GetComponent<DialogueInteractable>());

            dialogueInteractable = GameObject.FindGameObjectWithTag("Trigger").GetComponent<DialogueInteractable>();

            bool gave = dialogueInteractable.TriggerDialogueObject(dialogueObject, dontGiveMe, characterName, characterName2);

            if (gave)
                return true;

            return false;
        }

    }

    public interface IItemAction
    {
        public string ActionName { get; }

        public AudioClip actionSFX { get; }

        bool PerformAction(GameObject character);
    }
}
