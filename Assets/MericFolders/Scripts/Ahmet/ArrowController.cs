using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Pool;

public class ArrowController : MonoBehaviour
{

    [Header("ArrowControl")]
    public GameObject arrowPrefab;
    public float distanceBetweenArrows;
    [SerializeField] private float startDistance = 10f;
    private ObjectPool<GameObject> arrowPool;
    private List<GameObject> activeArrowsList = new List<GameObject>();
    public int ArrowCount => activeArrowsList.Count;
    public static ArrowController Instance { get; private set; }
    public int baseArrowCount;
    public int arrowCount;
    public bool finalView;
    bool popuped;
    public GameObject gold;
    public GameObject winPoPUP;
    public Canvas endSCreen;

    [Header("Movement")]
    public float limitX;
    public static Kosu Current; // static deðiþkenler herhangi bir sýnýf tarafýndan eriþilebilir. 
    public float xSpeed;
    public float runningSpeed;
    private float _lastTouchedX;
    public bool _finished;
    public float newX = 0;
    Animator anim;

    [Header("Other")]
    public TextMeshProUGUI arrowCountText;
    public GameObject textObject;
    public GameObject slideText;
    public GameObject arrow2;
    public GameObject gameOverPanel;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI goldAllText;
    public AudioSource goldSound;
    public AudioClip goldClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //   GameManager.Instance.onGameStartEvent.AddListener(OnGameStart);



        CreateArrowPool();
        SpawnArrow(baseArrowCount);
    }

    public void Start()
    {
        PlayerPrefs.SetInt("LevelPara", 0);
        PlayerPrefs.SetInt("OyunBasladi", 0); //sadece level1
        //
        anim = GetComponent<Animator>();
        arrowCount = baseArrowCount;
        PlayerPrefs.SetInt("avaible", 1);
        //
        PlayerPrefs.SetInt("OyunDevam", 1); // Sil baþka scripte yaz
        Application.targetFrameRate = 60;
    }

    public void changeScale()
    {
        if (Mathf.Abs(transform.position.x) > 0.2f)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1 - Mathf.Abs(Mathf.Abs(this.transform.position.x));
            transform.localScale = scale;
        }
        else if (Mathf.Abs(transform.position.x) <= 0.2f && Mathf.Abs(transform.position.x) > 0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x = 1 - Mathf.Abs(Mathf.Abs(this.transform.position.x)/2f);
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
    }

    public void changeDistanceBetweenArrows()
    {
        if (arrowCount < 30 && arrowCount > 0)
        {

            distanceBetweenArrows = 0.06f;
        }
        else if (arrowCount < 60 && arrowCount > 30)
        {
            distanceBetweenArrows = 0.05f;

        }
        else if (arrowCount < 90 && arrowCount > 60)
        {
            distanceBetweenArrows = 0.04f;

        }
        else if (arrowCount < 120 && arrowCount > 90)
        {
            distanceBetweenArrows = 0.035f;

        }
        else if (arrowCount > 120)
        {
            distanceBetweenArrows = 0.03f;

        }
    }
    private void Update()
    {


        goldAllText.text = PlayerPrefs.GetInt("LevelPara").ToString();
        goldText.text = PlayerPrefs.GetInt("Para").ToString();

        if (arrowCount <= 0 && !finalView)
        {
            gameOverPanel.SetActive(true);
        }

        arrow2.transform.position = new Vector3(0f,transform.position.y,transform.position.z);
        
        //sadece level1
        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began && PlayerPrefs.GetInt("OyunBasladi") == 0)
        {
            textObject.SetActive(true);
            slideText.gameObject.SetActive(false);
            PlayerPrefs.SetInt("OyunBasladi", 1);
        }
        //sadece level1

        if (PlayerPrefs.GetInt("OyunBasladi") == 1) //sadece level1
        {

            if (!finalView)
            {
                textObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.04f, this.transform.position.z - 1f);
            }
            else
            {
                textObject.transform.position = new Vector3(0, this.transform.position.y - 0.11f, this.transform.position.z - 1f);
            }
           

            arrowCountText.text = arrowCount.ToString();


            changeScale();
            changeDistanceBetweenArrows();

            if (this.transform.position.y <= -0.2f)
            {
                PlayerPrefs.SetInt("OyunDevam", 0);
                Time.timeScale = 0;
            }


            //   Debug.Log(stackParent.gameObject.GetComponent<StackController>()._stack.Count);


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

            //ArrowCounter(); __________

        }
        ArrowScaling();
        ArrowStopper();

    }




    private void CreateArrowPool()
    {

        //   arrowPrefab = Resources.Load<GameObject>("Arrow");
        arrowPool = new ObjectPool<GameObject>(
            50,
            CreateFunction: () =>
            {
                GameObject _arrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.Euler(90, 0, 0), transform);
                _arrow.SetActive(false);
                return _arrow;
            },
            OnPush: _arrow =>
            {
                _arrow.SetActive(false);
            },
            OnPop: _arrow =>
            {
                _arrow.transform.localPosition = Vector3.zero;
            }
        );
    }

    private void OnGameStart()
    {

    }



    public void DivideArrows(int divider)
    {
        if (divider < 1)
        {
            return;
        }

        float reduceAmount = ArrowCount * (divider - 1) / (float)divider;
        float remaining = ArrowCount - reduceAmount;

        if (remaining < 1)
        {
            GameManager.Instance.EndGame(false);
            return;
        }

        ReduceArrow(Mathf.CeilToInt(reduceAmount));
    }


    public void MultiplyArrows(int times)
    {
        if (arrowCount < 50)
        {
            if (times < 2)
            {
                return;
            }

            SpawnArrow(ArrowCount * (times - 1));
        }
        ReorderArrows();
    }

    public void ReduceArrow(int amount)
    {
        if (amount >= ArrowCount)
        {
            GameManager.Instance.EndGame(false);
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject _arrow = activeArrowsList[0];
            activeArrowsList.Remove(_arrow);
            arrowPool.Push(_arrow);
        }

        ReorderArrows();
    }

    public void SpawnArrow(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if(arrowCount < 50)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject _arrow = arrowPool.Pop();
                _arrow.SetActive(true);
                activeArrowsList.Add(_arrow);
            }
          
        }

        ReorderArrows();

    }

    private void ReorderArrows()
    {
        if (ArrowCount == 0)
        {
            return;
        }

        CapsuleCollider col = GetComponent<CapsuleCollider>();

        activeArrowsList[0].transform.localPosition = Vector3.zero;
        int arrowIndex = 1;
        int circleOrder = 1;

        while (true)
        {
            col.radius = 0.14f * circleOrder;
            float radius = circleOrder * distanceBetweenArrows;

            for (int i = 0; i < (circleOrder + 1) * 4; i++)
            {
                if (arrowIndex == ArrowCount)
                {
                    return;
                }

                float radians = 2 * Mathf.PI / (circleOrder + 1) / 4 * i;
                float vertical = Mathf.Sin(radians);
                float horizontal = Mathf.Cos(radians);

                Vector3 dir = new Vector3(horizontal, vertical, 0f);
                Vector3 newPosition = dir * radius;

                GameObject _arrow = activeArrowsList[arrowIndex];

                if (_arrow != null)
                {
                    _arrow.transform.DOKill();
                    _arrow.transform.DOLocalMove(newPosition, 0.25f);
                }

                arrowIndex++;
            }

            circleOrder++;
        }
    }

    IEnumerator wait01sec()
    {
        PlayerPrefs.SetInt("avaible", 0);
        yield return new WaitForSeconds(0.2f);
        PlayerPrefs.SetInt("avaible", 1);
    }

    private void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);

            SetActiveAllChildren(child, value);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("Gold"))
        {
         

            other.transform.DOScale(0f, 0.15f).OnComplete(() => other.gameObject.SetActive(false));
        }
        */
        //if (PlayerPrefs.GetInt("avaible")==1)
        //{
        if (other.name == "FinalTrigger")
        {
            finalView = true;
            this.GetComponent<CapsuleCollider>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = true;
            runningSpeed = 3.5f;

            arrow2.transform.GetChild(3).gameObject.SetActive(true);
            arrow2.transform.GetChild(4).gameObject.SetActive(true);
            arrow2.transform.GetChild(5).gameObject.SetActive(true);
            SetActiveAllChildren(this.transform, false);
            this.transform.position = new Vector3(0f, this.transform.position.y, this.transform.position.z);



        }
        if (other.tag == "times2")
        {

            MultiplyArrows(2);
            arrowCount = arrowCount * 2;
            StartCoroutine("wait01sec");
        }

        else if (other.tag == "times4")
        {

            MultiplyArrows(4);
            arrowCount = arrowCount * 4;
            StartCoroutine("wait01sec");
        }


        else if (other.tag == "per2")
        {

            DivideArrows(2);
            arrowCount = arrowCount / 2;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "per3") //hatalý
        {

            DivideArrows(3);
            arrowCount = arrowCount / 3;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "per10") //hatalý
        {

            DivideArrows(10);
            arrowCount = arrowCount / 10;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus10") //hatalý
        {

            SpawnArrow(10);
            arrowCount = arrowCount + 10;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus 5") //hatalý
        {

            SpawnArrow(5);
            arrowCount = arrowCount + 5;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus 5") //hatalý
        {

            ReduceArrow(5);
            arrowCount = arrowCount - 5;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus10") //hatalý
        {

            ReduceArrow(10);
            arrowCount = arrowCount - 10;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus25") //hatalý
        {

            ReduceArrow(25);
            arrowCount = arrowCount - 25;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus25") //hatalý
        {

            SpawnArrow(25);
            arrowCount = arrowCount + 25;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus50") //hatalý
        {

            SpawnArrow(50);
            arrowCount = arrowCount + 50;
            StartCoroutine("wait01sec");
        }

        else if (other.tag == "plus15") //hatalý
        {

            SpawnArrow(15);
            arrowCount = arrowCount + 15;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus65") //hatalý
        {

            SpawnArrow(65);
            arrowCount = arrowCount + 65;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus20") //hatalý
        {

            SpawnArrow(20);
            arrowCount = arrowCount + 20;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus40") //hatalý
        {

            SpawnArrow(40);
            arrowCount = arrowCount + 40;
            StartCoroutine("wait01sec");
        }
        //asdasd


        else if (other.tag == "per20") 
        {

            DivideArrows(20);
            arrowCount = arrowCount / 20;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus3")
        {

            ReduceArrow(3);
            arrowCount = arrowCount - 3;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus16") 
        {

            SpawnArrow(16);
            arrowCount = arrowCount + 16;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus8")
        {

            SpawnArrow(8);
            arrowCount = arrowCount + 8;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus2")
        {

            ReduceArrow(2);
            arrowCount = arrowCount - 2;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "per12") 
        {

            DivideArrows(12);
            arrowCount = arrowCount / 12;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times20")
        {

            MultiplyArrows(20);
            arrowCount = arrowCount * 20;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times12")
        {

            MultiplyArrows(12);
            arrowCount = arrowCount * 12;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus30")
        {

            ReduceArrow(30);
            arrowCount = arrowCount - 30;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus100")
        {

            SpawnArrow(100);
            arrowCount = arrowCount + 100;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus15") 
        {

            ReduceArrow(15);
            arrowCount = arrowCount - 15;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times10")
        {

            MultiplyArrows(10);
            arrowCount = arrowCount * 10;
            StartCoroutine("wait01sec");
        }
       
        else if (other.tag == "plus30") 
        {

            SpawnArrow(30);
            arrowCount = arrowCount + 30;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times3")
        {

            MultiplyArrows(3);
            arrowCount = arrowCount * 3;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus3") 
        {

            SpawnArrow(3);
            arrowCount = arrowCount + 3;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus18")
        {

            SpawnArrow(18);
            arrowCount = arrowCount + 18;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus55") 
        {

            SpawnArrow(55);
            arrowCount = arrowCount + 55;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times15")
        {

            MultiplyArrows(15);
            arrowCount = arrowCount * 15;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus8")
        {

            ReduceArrow(8);
            arrowCount = arrowCount - 8;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times5")
        {

            MultiplyArrows(5);
            arrowCount = arrowCount * 5;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus12")
        {

            SpawnArrow(12);
            arrowCount = arrowCount + 12;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus22")
        {

            SpawnArrow(22);
            arrowCount = arrowCount + 22;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus2")
        {

            SpawnArrow(2);
            arrowCount = arrowCount + 2;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times24")
        {

            MultiplyArrows(24);
            arrowCount = arrowCount * 24;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times75")
        {

            MultiplyArrows(75);
            arrowCount = arrowCount * 75;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "per1")
        {

            DivideArrows(1);
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus18")
        {

            ReduceArrow(18);
            arrowCount = arrowCount - 18;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus1")
        {

            SpawnArrow(1);
            arrowCount = arrowCount + 1;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus42") 
        {

            SpawnArrow(42);
            arrowCount = arrowCount + 42;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus6")
        {

            SpawnArrow(6);
            arrowCount = arrowCount + 6;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus1")
        {

            ReduceArrow(1);
            arrowCount = arrowCount - 1;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus16") 
        {

            ReduceArrow(16);
            arrowCount = arrowCount - 16;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus72") 
        {

            SpawnArrow(72);
            arrowCount = arrowCount + 72;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus75")
        {

            SpawnArrow(75);
            arrowCount = arrowCount + 75;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus47")
        {

            SpawnArrow(47);
            arrowCount = arrowCount + 47;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus4")
        {

            SpawnArrow(4);
            arrowCount = arrowCount + 4;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus45")
        {

            SpawnArrow(45);
            arrowCount = arrowCount + 45;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus35")
        {

            SpawnArrow(35);
            arrowCount = arrowCount + 35;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus68")
        {

            SpawnArrow(68);
            arrowCount = arrowCount + 68;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus44")
        {

            SpawnArrow(44);
            arrowCount = arrowCount + 44;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus8")
        {

            ReduceArrow(8);
            arrowCount = arrowCount - 8;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus20")
        {

            ReduceArrow(20);
            arrowCount = arrowCount - 20;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "times7")
        {

            MultiplyArrows(7);
            arrowCount = arrowCount * 7;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus4")
        {

            SpawnArrow(4);
            arrowCount = arrowCount + 4;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus34")
        {

            SpawnArrow(34);
            arrowCount = arrowCount + 34;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus33")
        {

            SpawnArrow(33);
            arrowCount = arrowCount + 33;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus33")
        {

            ReduceArrow(33);
            arrowCount = arrowCount - 33;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus4")
        {

            ReduceArrow(4);
            arrowCount = arrowCount - 4;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "per4")
        {

            DivideArrows(4);
            arrowCount = arrowCount / 4;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "plus32")
        {

            SpawnArrow(32);
            arrowCount = arrowCount + 32;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "per6")
        {

            DivideArrows(6);
            arrowCount = arrowCount / 6;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus50")
        {

            ReduceArrow(50);
            arrowCount = arrowCount - 50;
            StartCoroutine("wait01sec");
        }
        else if (other.tag == "minus22")
        {

            ReduceArrow(22);
            arrowCount = arrowCount - 22;
            StartCoroutine("wait01sec");
        }







        if (other.tag == "Enemy")
        {
          
                if (other.name == "redEnemy")
                {
                goldSound.Play();
                
                PlayerPrefs.SetInt("LevelPara", PlayerPrefs.GetInt("LevelPara") + 3);
                    GameObject burstGold = Instantiate(gold, other.transform.position, Quaternion.Euler(-90, 0, 0));
                    Object.Destroy(burstGold, 4);
                    ReduceArrow(3);
                    arrowCount = arrowCount - 3;
                    other.gameObject.GetComponent<Animator>().SetTrigger("Dead");
                    other.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(true);
                }
                if (other.name == "purpleEnemy")
                {
                goldSound.Play();
                PlayerPrefs.SetInt("LevelPara", PlayerPrefs.GetInt("LevelPara") + 10);
                    GameObject burstGold = Instantiate(gold, other.transform.position, Quaternion.Euler(-90, 0, 0));
                    Object.Destroy(burstGold, 4);
                    arrowCount = arrowCount - 10;
                    other.gameObject.GetComponent<Animator>().SetTrigger("Dead");
                    other.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(true);
                }
                if (other.name == "blueEnemy")
                {
                goldSound.Play();
                PlayerPrefs.SetInt("LevelPara", PlayerPrefs.GetInt("LevelPara") + 20);
                    GameObject burstGold = Instantiate(gold, other.transform.position, Quaternion.Euler(-90, 0, 0));
                    Object.Destroy(burstGold, 4);
                    arrowCount = arrowCount - 20;
                    other.gameObject.GetComponent<Animator>().SetTrigger("Dead");
                    other.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(true);
                }
            
           
          
                if (other.name == "greenEnemy")
                {
               
                goldSound.Play();
                GameObject burstGold = Instantiate(gold, other.transform.position, Quaternion.Euler(-90, 0, 0));
                Object.Destroy(burstGold, 4);
                PlayerPrefs.SetInt("LevelPara", PlayerPrefs.GetInt("LevelPara") + 2);
                    ReduceArrow(2);
                    arrowCount = arrowCount - 2;
                other.gameObject.GetComponent<Animator>().SetBool("running", false);
                other.gameObject.GetComponent<Animator>().SetBool("Dead", true);

         //       other.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).gameObject.SetActive(true);
                }
               
            
           


            //GameObject.Find
            //StartCoroutine("wait01sec");
        }

        if(other.tag == "x1")
        {
            arrow2.transform.GetChild(2).gameObject.SetActive(true);
            arrow2.transform.GetChild(6).gameObject.SetActive(true);
            
        }
        else if (other.tag == "x1.2")
        {
            arrow2.transform.GetChild(1).gameObject.SetActive(true);
            arrow2.transform.GetChild(7).gameObject.SetActive(true);
           
        }
        else if (other.tag == "x1.4")
        {
            arrow2.transform.GetChild(0).gameObject.SetActive(true);
            arrow2.transform.GetChild(8).gameObject.SetActive(true);
          
        }
        else if (other.tag == "x1.6")
        {
            arrow2.transform.GetChild(9).gameObject.SetActive(true);
            arrow2.transform.GetChild(10).gameObject.SetActive(true);

        }
        else if (other.tag == "x1.8")
        {
            arrow2.transform.GetChild(11).gameObject.SetActive(true);
            arrow2.transform.GetChild(12).gameObject.SetActive(true);
    
        }
        //}

    }
    void ArrowScaling()
    {
        if (finalView == true)
        {
            //arrowPrefab.transform.localScale = 0.2f
        }
    }
    void ArrowStopper()
    {

        if (arrowCount < 0 && popuped == false)
        {
            textObject.SetActive(false);
            runningSpeed = 0;
        //    Instantiate(winPoPUP, this.gameObject.transform.position, Quaternion.identity);
            popuped = true;
            endSCreen.gameObject.SetActive(true);
            SetActiveAllChildren(arrow2.transform,false);
        }
    }
}
