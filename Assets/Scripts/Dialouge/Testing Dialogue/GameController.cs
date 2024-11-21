using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;



public class GameController : MonoBehaviour
{
    // Scene variables
    public StoryScene currentScene;
    public BottomBarControllers bottomBar;

    void Start()
    {
        bottomBar.PlayScene(currentScene);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(bottomBar.IsCompleted())
            {
                if(bottomBar.IsLastSentence())
                {
                    currentScene = currentScene.nextScene;
                    bottomBar.PlayScene(currentScene);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
    }
}
