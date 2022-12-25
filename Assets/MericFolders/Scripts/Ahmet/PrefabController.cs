using System;
using UnityEngine;
using Interfaces;


public class PrefabContollerv1 : MonoBehaviour, IInteract
{
    public static Action<bool, Transform> OnInteract;

    private bool _isPickedUp;
    private Collider _collider;
    public int y = 1;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }


    public void Update()
    {
        if (transform.parent != null && transform.parent.tag == "stckParent")
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void Interact()
    {
        if (!_isPickedUp)
        {
            AddToStack();
        }
        else
        {
            RemoveFromStack();
        }

        OnInteract.Invoke(_isPickedUp, transform);
    }

    private void AddToStack()
    {
        _isPickedUp = true;
        _collider.isTrigger = false;
    }

    public void RemoveFromStack()
    {
        _isPickedUp = false;
        _collider.isTrigger = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "times2")
        {
            _isPickedUp = true;
            _collider.isTrigger = false;
            Debug.Log("çalýþtý");
        }

    }
}
