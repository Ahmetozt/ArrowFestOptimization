using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagers : MonoBehaviour
{
    private int sceneNumber;
    bool IsStarted;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject); 
    }

    void Update()
    {
        
    }
    //void Load()
    //{
    //    switch (gameProgress)
    //    {
    //        case (gameProgress == 0):
    //            SceneManager.LoadScene("Scene01");
    //            break;
    //        case 2:
    //            SceneManager.LoadScene("Scene02");
    //            break;
    //        case 3:
    //            SceneManager.LoadScene("Scene03");
    //            break;
    //        case 4:
    //            SceneManager.LoadScene("Scene04");
    //            break;
    //        case 5:
    //            SceneManager.LoadScene("Scene05");
    //            break;
    //        case 6:
    //            SceneManager.LoadScene("Scene06");
    //            break;
    //        case 7:
    //            SceneManager.LoadScene("Scene07");
    //            break;
    //        case 8:
    //            SceneManager.LoadScene("Scene08");
    //            break;
    //        case 9:
    //            SceneManager.LoadScene("Scene09");
    //            break;
    //        case 10:
    //            SceneManager.LoadScene("Scene10");
    //            break;
    //    }
    //}
    public void LoadNextLevel()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;

            if (sceneNumber == 0)
            {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(1);
           
          
            }
            else if (sceneNumber == 1)
            {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(2);
           
        }
            else if (sceneNumber == 2)
            {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(3);
            
        }
            else if (sceneNumber == 3)
            {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(4);
            }
        else if (sceneNumber == 4)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(5);
        }
        else if (sceneNumber == 5)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(6);
        }
        else if (sceneNumber == 6)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(7);
        }
        else if (sceneNumber == 7)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(8);
        }
        else if (sceneNumber == 8)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(9);
        }
    }

    public void LoadCurrentLevel()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;

        if (sceneNumber == 0)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));

            SceneManager.LoadScene(0);

        }
        else if (sceneNumber == 1)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));

            SceneManager.LoadScene(1);

        }
        else if (sceneNumber == 2)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));

            SceneManager.LoadScene(2);

        }
        else if (sceneNumber == 3)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(3);
        }
        else if (sceneNumber == 4)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(4);
        }
        else if (sceneNumber == 5)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(5);
        }
        else if (sceneNumber == 6)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(6);
        }
        else if (sceneNumber == 7)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(7);
        }
        else if (sceneNumber == 8)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(8);
        }
        else if (sceneNumber == 9)
        {
            PlayerPrefs.SetInt("Para", PlayerPrefs.GetInt("LevelPara") + PlayerPrefs.GetInt("Para"));
            SceneManager.LoadScene(9);
        }
    }
}

