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
    
    private State state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE
    }

    void Start()
    {
        bottomBar.PlayScene(currentScene);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(state == State.IDLE && bottomBar.IsCompleted())
            {
                if(bottomBar.IsLastSentence())
                {
                    PlayScene(currentScene.nextScene);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
    }

    public void PlayScene(StoryScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(StoryScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;
        bottomBar.Hide();
        yield return new WaitForSeconds(1f);
        bottomBar.ClearText();
        bottomBar.PlayScene(scene);
        yield return new WaitForSeconds(1f);
        bottomBar.Show();
        
        state = State.IDLE;
    }
}
