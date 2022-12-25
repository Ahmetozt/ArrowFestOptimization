using System.Collections.Generic;
using UnityEngine;


    public class StackController : MonoBehaviour
    {
        [SerializeField] private Transform _pickUpParent;
        [SerializeField] private float _spaceBetweenNodes;
        [SerializeField] private float _lerpDuration;

        private Transform _stackParent;
        public Transform _playerTransform;
        public List<Transform> _stack = new List<Transform>();
    public GameObject etPrefab;
    public Vector3 nodePosition;
        private void Awake()
        {
        PlayerPrefs.SetInt("silme", 0);
            _stackParent = transform;
            _playerTransform = FindObjectOfType<PlayerController>().transform;
            _stack.Add(_playerTransform);

        
        PickUp.OnInteract += UpdateStack;
        }

        private void Update()
        {
            // Simple fix for the child-parent-parent position relationship.
            transform.localPosition = new Vector3(_playerTransform.position.x * -1, 0, 0);

        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeSelf)
            {
                this._stack.Remove(child);
            }
        }

    }

        private void FixedUpdate()
        {
            WaveNodes();
        }

    
        private void WaveNodes()
        {
            for (int i = 1; i < _stack.Count; i++)
            {
            //   Vector3 nodePosition = _stack[i].localPosition;
        
                nodePosition = _stack[i].localPosition;
                Vector3 previousNodePosition = _stack[i - 1].localPosition;
              /* 
            nodePosition = new Vector3(
                    Mathf.Lerp(nodePosition.x, previousNodePosition.x, Time.deltaTime * _lerpDuration),
                    nodePosition.y,
                    i * _spaceBetweenNodes);
            */
                _stack[i].localPosition = nodePosition;
            
           
            }
        }
    
        private void UpdateStack(bool isPickedUp, Transform node)
        {
            if (isPickedUp)
            {
               _stack.Add(node);
            
                node.SetParent(_stackParent);
            }
            else
            {
                _stack.Remove(node);
                node.SetParent(_pickUpParent);
            }
        }
    }
