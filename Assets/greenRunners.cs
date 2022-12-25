using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenRunners : MonoBehaviour
{
    public GameObject arrowParent;
    public bool yasiyor=true;
    public float hiz;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Mathf.Abs(this.transform.position.z - arrowParent.transform.position.z) <= 3)
            {
            if(anim.GetBool("Dead")== false)
            {
                anim.SetBool("running", true);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + hiz * Time.deltaTime);
            }
               

            
            }

        
      
    }
}
