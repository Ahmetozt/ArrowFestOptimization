using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalMultiplier : MonoBehaviour
{
    public GameObject prefabArrow;
    public TextMeshProUGUI arrowCounter;
    public int numberArrow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        arrowCounter.text = (numberArrow.ToString());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Cube")
        {
            Debug.Log("bire girdim.");
            numberArrow = (int)(numberArrow * 1.2f);
            //GameObject fakearrow1 = Instantiate(prefabArrow, GameObject.Find("Arrow").transform.position+new Vector3(0.2f,0f,0f), GameObject.Find("Arrow").transform.rotation);
            //GameObject fakearrow2 = Instantiate(prefabArrow, GameObject.Find("Arrow").transform.position + new Vector3(-0.2f, 0f, 0f), GameObject.Find("Arrow").transform.rotation);
            //fakearrow1.transform.parent = GameObject.Find("Arrow").transform;
            //fakearrow2.transform.parent = GameObject.Find("Arrow").transform;

        }
        if (other.gameObject.name == "Cube (6)")
        {
            Debug.Log("bire girdim.");
        }
        if (other.gameObject.name == "Minnion")
        {
            numberArrow -= 3;
            Debug.Log("Minniona girdim.");
        }
    }
}
