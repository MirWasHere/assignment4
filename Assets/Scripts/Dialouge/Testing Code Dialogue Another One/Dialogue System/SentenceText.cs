using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SentenceText
{
    [SerializeField] private string sentence;
    [SerializeField] private string charName;
    [SerializeField] public Sprite charSprite;

    public string Sentences => sentence;
    public string CharName => charName;
    public Sprite CharSprite => charSprite;

}
