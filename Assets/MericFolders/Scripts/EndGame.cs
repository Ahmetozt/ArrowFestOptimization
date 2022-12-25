using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public GameObject Triggerzone;
    public GameObject popUp;
    Collider colliderz;
    Vector3 v3endgame;
    Vector3 v3enemy;
    Animator anim;
    Controller controller;
    public GameObject arrows;
    public bool IsDead;
    void Start()
    {
    }

    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        colliderz = GetComponent<Collider>();
        v3endgame = new Vector3(0, 1, 36.2f);
        if (colliderz.name == "TriggerZone")
        {
            GameObject.Find("Arrow").transform.DOMove(v3endgame, 1).SetEase(Ease.OutCubic).OnComplete(Shooting);
        }
    }
    void Shooting()
    {
        v3enemy = new Vector3(0, 1, 41.5f);
        GameObject.Find("Arrow").transform.DOMove(v3enemy, 1).OnComplete(OnOffArrow);
    }
    void OnOffArrow()
    {
        anim = GameObject.Find("Enemy").GetComponent<Animator>();
        controller = GameObject.FindObjectOfType<Controller>();
        GameObject.Find("Arrow").GetComponent<MeshRenderer>().enabled = false;
        controller.speedArrow = 0;
        arrows.SetActive(true);
        anim.SetBool("Dead", true);
        IsDead = true;
    }

}