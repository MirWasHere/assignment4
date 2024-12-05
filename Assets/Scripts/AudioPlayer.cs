using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class AudioPlayer : MonoBehaviour
{

    [SerializeField] AudioClip[] songs = new AudioClip[6];
    private AudioSource audioSource;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent <AudioSource>();
        audioSource.clip = songs[0];
        audioSource.Play();
        count = 0;
    }

    void OnTriggerEnter(Collider other) 
    {
     // Check if the object the player collided with has the "DoorOpen" tag.
        if (other.gameObject.CompareTag("DoorOpen")) 
           {
                audioSource.clip = songs[1];
                audioSource.Play();
          }
          else if (other.gameObject.CompareTag("DoorOpen2")) 
           {
                audioSource.clip = songs[2];
                audioSource.Play();
                
          }
          else if (other.gameObject.CompareTag("MayorDoor")) 
           {
                audioSource.clip = songs[4];
                audioSource.Play();
                
          }
          else if (other.gameObject.CompareTag("MayorDoorExit")) 
           {
                audioSource.clip = songs[3];
                audioSource.Play();
                
          }
          else if (other.gameObject.CompareTag("Collectible")) 
           {
                count = count + 1;
                if(count > 2)
                {
                    audioSource.clip = songs[3];
                    audioSource.Play();
                }
          }
      }

}
