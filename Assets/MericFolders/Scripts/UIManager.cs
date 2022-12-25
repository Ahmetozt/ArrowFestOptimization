using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject currentLevel;
    public GameObject levelCompletePopUp;
    public GameObject hintFinger;
    Controller controller;

    void Start()
    {
        controller = GameObject.FindObjectOfType<Controller>();
        levelCompletePopUp.SetActive(false);
    }

    
    void Update()
    {
        
    }
    public void LevelCompleted()
    {
        levelCompletePopUp.SetActive(true);
    }
    public void Starting()
    {
        if (Input.GetMouseButton(0) && controller.IsStart == false)
        {
            controller.speedArrow = 3;
            hintFinger.SetActive(false);
            controller.IsStart = true;
        }

    }

}
