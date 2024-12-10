using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SentenceText
{
    [TextArea(3, 10)]
    [SerializeField] private string sentence;
    [SerializeField] private string charName;
    [SerializeField] private Sprite charSprite;

    public string Sentences => sentence;
    public string CharName => charName;
    public Sprite CharSprite => charSprite;

    public void setCharName(string name) {

        charName = name;

    }

    public void setCharSprite(Sprite sprite) {

        charSprite = sprite;

    }

}
