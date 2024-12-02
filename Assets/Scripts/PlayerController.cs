
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
        winTwice = false;
        wonThrice = false;
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
                if(count >= 9){
                    Debug.Log("YAY!");
                    won = true;
                }
                if(count >= 14){
                    Debug.Log("um");
                    winTwice = true;
                }
                if(count >= 19)
                {
                    Debug.Log("...");
                    wonThrice = true;
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
        if(wonThrice)
        {
            countText.text = "You know you don't get anything extra for getting all of them, right..?\n";
            countText.text += "Count: " + count.ToString();
        }
        
        else if(winTwice)
        {
            countText.text = "You Can Stop Now...\n";
            countText.text += "Count: " + count.ToString();

        }
        
        else if(won)
        {
            countText.text = "You Win!!\n";
            countText.text += "Count: " + count.ToString();
        }
        else
        {
            countText.text = "Count: " + count.ToString();
        }


    }
}