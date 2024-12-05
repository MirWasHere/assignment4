using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        // Getting the player's tag
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = player.transform.position + offset;
        
    }
}
