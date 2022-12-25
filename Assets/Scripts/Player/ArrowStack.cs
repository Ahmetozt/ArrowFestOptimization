using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowStack : MonoBehaviour
{

    [Header("Arrow Mechanics")]
    public TextMeshProUGUI arrowCountUI;
    public int baseArrowCount = 1;
    private int arrowCount;
    public List<GameObject> arrows = new List<GameObject>();
    public GameObject arrowObect;
    public Transform parent;
    public GameObject player;
    public float mesafe;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void MoveObjects(Transform objectTransform,float degree)
    {
        Vector3 pos = Vector3.zero;
        pos.x = Mathf.Cos(degree * Mathf.Deg2Rad);
        pos.y = Mathf.Sin(degree * Mathf.Deg2Rad);
        objectTransform.localPosition = pos * mesafe;
    }
   void Diz()
    {
        float angle = 1f;
        float arrowCount = arrows.Count;
        angle = 360 / arrowCount;

        for (int i =0; i<arrowCount; i++)
        {
            MoveObjects(arrows[i].transform,i*angle);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            GetRay();
        }

      //  arrowCountUI.text = player.GetComponent<Kosu>().arrowCount.ToString();
    }
    void GetRay()
    {
        Vector3 mousePos = Input.mousePosition;
       
    }
}
