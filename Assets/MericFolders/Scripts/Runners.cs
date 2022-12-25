using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Runners : MonoBehaviour
{
    public GameObject arrow;
    Animator anim;
    float  _distance;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_distance);
        EscapeFromArrow();
    }
    void EscapeFromArrow()
    {
        _distance = Vector3.Distance(arrow.transform.position, this.gameObject.transform.position);
        if (_distance <= 5)
        {
            Debug.Log("Kaçýyom");
            GameObject.Find("Runner").GetComponent<Animator>().SetBool("IsRunning", true);
            GameObject.Find("Runner").transform.DORotate(new Vector3(0, 180, 0), 1);
            this.gameObject.transform.DOMove(new Vector3(0,0.45f,40), 30f);
        }
    }
}

