using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        // if(SceneManager.GetActiveScene().name == "forestAndTown")
        // {
        //     transform.eulerAngles.y = player.transform.eulerAngles.y;
        //     transform.eulerAngles.x = player.transform.eulerAngles.x;
        //     transform.eulerAngles.z = player.transform.eulerAngles.z;
        // }
    }
}
