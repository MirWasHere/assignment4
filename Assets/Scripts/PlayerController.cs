
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float turnSpeed = 45f;
    private Rigidbody rb;

    public TextMeshProUGUI countText;
    private int count;

    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed);
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);
    }


        void OnTriggerEnter(Collider other) 
      {
     // Check if the object the player collided with has the "DoorOpen" tag.
        if (other.gameObject.CompareTag("DoorOpen")) 
           {
                Debug.Log(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Scene2");

                 transform.position = new Vector3(3f, 3.0f, 8f);
          }
          else if (other.gameObject.CompareTag("DoorOpen2")) 
           {
                Debug.Log(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Scene3");
                transform.position = new Vector3(8.95f, 11.0f, -10f);
                
          }
          else if (other.gameObject.CompareTag("Collectible")) 
           {
                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();
                if(count >= 10){
                    Debug.Log("YAY!");
                }
          }
      }

      void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

    }
}