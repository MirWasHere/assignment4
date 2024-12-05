
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using Inventory.Model;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float turnSpeed = 45f;
    private Rigidbody rb;

    [SerializeField]
    private InventorySO inventoryData;

    public TextMeshProUGUI countText;
    private int count;
    private bool won;
    private bool winTwice;
    private bool wonThrice;

    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        won = false;
    }

    void Update()
    {
        // Player cannot move if in a conversation
        if(!DialogueInteractable.inConversation)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed);
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed);
            // Shift to sprint
            if(Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed *= 2;
            }
            if(Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed /= 2;
            }
        }
        // if(SceneManager.GetActiveScene().name == "Alchemy Lab")
        // {
        //    transform.localScale += new Vector3(1, 1, 1);
        // }
        // else{
        //     transform.localScale += new Vector3(0, 0, 0);
        // }

        if(SceneManager.GetActiveScene().name == "Final")
        {
            transform.position = new Vector3(7.65f, 7.9f, 12f);
            transform.Rotate(90, 0, 0);

        }
    }


    void OnTriggerEnter(Collider other) 
    {
     // Check if the object the player collided with has the "DoorOpen" tag.
        if (other.gameObject.CompareTag("DoorOpen")) 
           {
                Debug.Log(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Scene2");

                 transform.position = new Vector3(3f, 3.0f, 3f);
          }
          else if (other.gameObject.CompareTag("DoorOpen2")) 
           {
                Debug.Log(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Scene3");
                transform.position = new Vector3(8.95f, 11.0f, -10f);
                
          }
          else if (other.gameObject.CompareTag("MayorDoor")) 
           {
                Debug.Log(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Alchemy Lab");
                transform.position = new Vector3(0.68f, 3.81469f, -10f);
                
          }
          else if (other.gameObject.CompareTag("MayorDoorExit")) 
           {
                Debug.Log(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("forestAndTown");
                transform.position = new Vector3(-350.59f, 2.71f, 32.92f);
                
          }
          else if (other.gameObject.CompareTag("Collectible")) 
           {
                other.gameObject.SetActive(false);
                count = count + 1;
                SetCountText();
                if(count >= 2)
                {
                    Debug.Log("Strawberries Aquired?");
                    won = true;
                }

                Item item = other.GetComponent<Item>();
                if (item != null)
                {
                    int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
                    if (reminder != 0)
                        item.Quantity = reminder;
                }
          }
      }

    void SetCountText()
    {
        if(won)
        {   countText.text = "Count: " + count.ToString();

            // Setting new scene (until transition is added. maybe??)
            Debug.Log(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("forestAndTown");

            transform.position = new Vector3(-185f, 12f, 34.68f);
        }
        else
        {

            countText.text = "COLLECT STRAWBERRIES!\nCount: " + count.ToString() + "/15";
        }


    }
}

// 122f, 15f, -82f