using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    
    // Keeps track of how many times the camera has changed
    // to prevent a mess
    private int tracker;


    // Start is called before the first frame update
    void Start()
    {
        tracker = 0;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = player.transform.position + offset;

        // if(SceneManager.GetActiveScene().name == "forestAndTown" && tracker < 3)
        // {
        //     Debug.Log("New Scene, New Camera Angle");
        //     transform.Rotate(15, -90, 15);
        //     transform.position = new Vector3(140f, 15f, -82f);

        //     Debug.Log("Camera x: " + transform.position.x);
        //     tracker ++;
        // }
        // else if(SceneManager.GetActiveScene().name != "forestAndTown")
        // {
        //     transform.position = player.transform.position + offset;
        // }
        // else
        // {
        //     transform.position = player.transform.position + offset;
        // }
        
    }
}
