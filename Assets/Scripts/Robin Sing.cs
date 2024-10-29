using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobinSing : MonoBehaviour
{

    [SerializeField]public Animator birbAnim;
    private bool yeah;

    // Start is called before the first frame update
    private void Awake()
    {
        birbAnim.SetBool("PlayerInRange", false);
        yeah = false;
    }

    private void Update()
    {
        birbAnim.SetBool("PlayerInRange", yeah);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            yeah = true;
            Debug.Log("I'm TRIGGGERREEDD!!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            yeah = false;
        }
    }

}
