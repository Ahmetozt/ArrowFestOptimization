using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
public class timesPer : MonoBehaviour
{
    public GameObject stackParent;
    public int childNumber;
    public int asilSayi = 0;
    // Start is called before the first frame update
    void Start()
    {
        childNumber = stackParent.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

       
        

        

      
    }

    /*
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "times2")
        {
            Debug.Log("calisiyor");
            asilSayi = asilSayi * 2;
        }
    }
    */
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteract interactable))
        {
            interactable.Interact();
        }
    }
    */
}
