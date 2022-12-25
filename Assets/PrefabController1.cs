using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController2 : MonoBehaviour
{
    int y = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
    }

    public void Update()
    {
        
        
        if (transform.parent != null && transform.parent.tag == "stckParent")
        {
            y++;
            if (y > 10)
            {
                this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
              
            }
            
        }
        else
        {
            this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }
        
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("aa");
        if(collision.gameObject.tag == "player")
        {
            this.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aa");
        if (other.gameObject.tag == "player")
        {
            this.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }
    */
}
