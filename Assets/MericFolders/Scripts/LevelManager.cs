using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public GameObject winPopUps;
    EndGame endgame;
    bool ýncreased;
    public TextMeshProUGUI textLevel;
    bool IsEnded;
    SceneManagers scenemanagers;
    UIManager uimanager;
    private int sahne;

    void Start()
    {
        uimanager = GameObject.FindObjectOfType<UIManager>();
        scenemanagers = GameObject.FindObjectOfType<SceneManagers>();
        endgame = GameObject.FindObjectOfType<EndGame>();
        DisplayProgress();
    }


    void Update()
    {
        //Final();
    }
    void Final()
    {
        if (endgame.IsDead == true && ýncreased == false)
        {
            GameObject objectPopUp = Instantiate(winPopUps, Vector3.zero, Quaternion.Euler(0, 0, 0));
            uimanager.LevelCompleted();
            Object.Destroy(objectPopUp, 15);            
            ýncreased = true;
            endgame.IsDead = false;
            IsEnded = true;
            StartCoroutine(Nextlevel());

        }
    }
    void DisplayProgress()
    {
        sahne = (SceneManager.GetActiveScene().buildIndex + 1);
        textLevel.text = "LEVEL " + sahne;
    }
    IEnumerator Nextlevel()
    {
        yield return new WaitForSeconds(10);
        scenemanagers.LoadNextLevel();
    }

}
