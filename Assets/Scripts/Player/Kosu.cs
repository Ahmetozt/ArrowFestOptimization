using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using TMPro;

public class Kosu : MonoBehaviour
{
    //rocket
    [Header("Movement")]
    public float limitX;
    public static Kosu Current; // static deðiþkenler herhangi bir sýnýf tarafýndan eriþilebilir. 
    public float xSpeed;
    public float runningSpeed;
    private float _currentRunningSpeed;
    private float _lastTouchedX;
    public bool _finished;

    [Header("Other")]
    public GameObject kulak;
    private int x = 0;
    public GameObject etPrefab;
    public GameObject stackParent;
    public int silmeSayisi = 0;
    public int sira = 0;
    public int kere = 0;
    public int keree = 0;
    public int kereee = 0;
    

    [Header("Arrow Mechanics")]
    public TextMeshProUGUI arrowCountUI;
    public int baseArrowCount=1;
    public int arrowCount;
    public List<GameObject> arrows = new List<GameObject>();
    public GameObject arrowObect;
    public Transform parent;
    
    // Update is called once per frame
    public void Start()
    {
        //
        arrowCount = baseArrowCount;
        //
        PlayerPrefs.SetInt("OyunDevam", 1); // Sil baþka scripte yaz
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        //
      //  arrowCountUI.text = arrowCount.ToString(); 
        //
        if(this.transform.position.y <= -0.2f)
        {
            PlayerPrefs.SetInt("OyunDevam", 0);
            Time.timeScale = 0;
        }

        Debug.Log(PlayerPrefs.GetInt("silme"));
     //   Debug.Log(stackParent.gameObject.GetComponent<StackController>()._stack.Count);

        float newX = 0;
        float touchXDelta = 0;
        if (Input.touchCount > 0) // parmakla dokunma
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                _lastTouchedX = Input.GetTouch(0).position.x;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchXDelta = 5 * (Input.GetTouch(0).position.x - _lastTouchedX) / Screen.width;
                _lastTouchedX = Input.GetTouch(0).position.x;
            }


        }
        else if (Input.GetMouseButton(0)) // mouse ile týklama
        {
            touchXDelta = Input.GetAxis("Mouse X");

        }
        newX = transform.position.x + xSpeed * touchXDelta * Time.deltaTime;
        newX = Mathf.Clamp(newX, -limitX, limitX);

        // karakter hareket ettirme kodu
        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z + runningSpeed * Time.deltaTime);
        transform.position = newPosition;

        
    

        Debug.Log(this.transform.position.z > kulak.transform.position.z + 10f);
        if (this.transform.position.z >= kulak.transform.position.z)
        {
            Debug.Log("oyun bitti");
            Time.timeScale = 0;
        }
    }
    public void ChangedSpeed(float value)
    {
        _currentRunningSpeed = value;
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "times2")
        {
            /*
            Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z),Quaternion.EulerRotation(0,0,0));
            //Time.timeScale = 0;
            Debug.Log("Çalýþtý2");
            */
            arrowCount = arrowCount * 2;
        }

        if (other.tag == "times4")
        {
            /*
            StartCoroutine("ExampleCoroutine4");
            /*
            Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
            Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
            Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
            //Time.timeScale = 0;
            */
            Debug.Log("Çalýþtý4");
            arrowCount = arrowCount * 4;

        }
        /*
        if (other.tag == "collectable")
        {
            Debug.Log(stackParent.gameObject.GetComponent<StackController>()._stack.Count);
            if (stackParent.gameObject.GetComponent<StackController>()._stack.Count != 0)
            {
                other.gameObject.SetActive(false);
                Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
            }
        }
        */

        if(other.tag == "per2")
        {
            /*
            sira += 1;

            if(sira >= 2)
            {
                stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
                silmeSayisi += 0;
                PlayerPrefs.SetInt("silme", silmeSayisi);
                sira = 0;
            }
            

           // TryGetComponent(out IInteract interactable);
            

            //   Destroy(stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - 1].gameObject);


            */
            arrowCount = arrowCount / 2;
        }
        if (other.tag == "per3") //hatalý
        {
            /*
            sira += 2;

            if (sira >= 3)
            {
                if (stackParent.gameObject.GetComponent<StackController>()._stack.Count != 0)
                {
                    stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
                    silmeSayisi += 0;
                    PlayerPrefs.SetInt("silme", silmeSayisi);
                    sira -= 3;
                }
                    
            }
            */
            arrowCount = arrowCount / 3;
        }
        if (other.tag == "per10") //hatalý
        {
            /*
            sira += 9;

            if (sira >= 10)
            {

                if(stackParent.gameObject.GetComponent<StackController>()._stack.Count != 0)
                {
                    stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
                    silmeSayisi += 0;
                    PlayerPrefs.SetInt("silme", silmeSayisi);
                    sira -= 10;
                }
                
            }
            */
            arrowCount = arrowCount / 10;
        }
        if (other.tag == "plus10") //hatalý
        {
            if (kere == 0)
            {
                /*
                kere++;
                StartCoroutine("plus10");
                */
            }

            /*
            kere += 1;
            stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
            silmeSayisi += 0;
            PlayerPrefs.SetInt("silme", silmeSayisi);
            */


            arrowCount = arrowCount + 10;


        }
        if (other.tag == "minus25") //hatalý
        {
            if (keree == 0)
            {
                /*
                keree++;
                StartCoroutine("minus25");
                */
            }
            arrowCount = arrowCount - 25;
        }

        if(other.tag == "control25" && kereee ==0)
        {
            kereee = 1;
            Debug.Log("çalýþtý1");
            if(stackParent.gameObject.GetComponent<StackController>()._stack.Count > 25)
            {
                Debug.Log("çalýþtý+25");
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
                
            }
            else if (stackParent.gameObject.GetComponent<StackController>()._stack.Count <= 25)
            {
                Debug.Log("çalýþtý-25");
               
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

    }
        public void OnTriggerStay(Collider other)
    {
       

    }
   


    public void Die()
    {
       
        gameObject.layer = 6;
      //  Camera.main.transform.SetParent(null);
       // LevelController.Current.GameOver();
    }

   
  

    IEnumerator ExampleCoroutine4()
    {

        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);

    }

    IEnumerator plus10()
    {

        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        Instantiate(etPrefab, new Vector3(this.transform.position.x, 0.1875f, this.transform.position.z), Quaternion.EulerRotation(0, 0, 0));
        yield return new WaitForSeconds(0.05f);
        
    }

    IEnumerator minus25()
    {

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

        stackParent.gameObject.GetComponent<StackController>()._stack[stackParent.gameObject.GetComponent<StackController>()._stack.Count - (silmeSayisi + 1)].gameObject.SetActive(false);
        silmeSayisi += 0;
        PlayerPrefs.SetInt("silme", silmeSayisi);
        yield return new WaitForSeconds(0.05f);

    }

}
