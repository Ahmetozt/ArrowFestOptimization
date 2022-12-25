using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    
    public GameObject arrow;
    UIManager uimanager;
    public float speedArrow =0;
    private Transform playerTransform;
    public float limitValue;
    public bool IsStart;
    void Start()
    {
        uimanager = GameObject.FindObjectOfType<UIManager>();
        playerTransform = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        uimanager.Starting();        
        ForwardMovement();
        HorizontalMovement();
    }
    void ForwardMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speedArrow);
    }
    void HorizontalMovement()
    {
        if (Input.GetMouseButton(0))
        {
            float halfScreen = Screen.width / 2;
            float xPos = (Input.mousePosition.x - halfScreen) / halfScreen;
            float finalXPos = Mathf.Clamp(xPos * limitValue, -limitValue, limitValue);
            playerTransform.localPosition = new Vector3(finalXPos, transform.position.y, transform.position.z);
        }

    }

    
}
