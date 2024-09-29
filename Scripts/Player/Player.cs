using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IObserver
{
    [SerializeField] private float _speed;

    [Header("DRAGGABLE VARIABLES")]
    [SerializeField] private Transform _playerHand;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _animator;

    PlayerModel _model;
    public PlayerView view;
    PlayerController _controller;

    private GameObject _interactableObj;

    private delegate void MyDelegate();
    private MyDelegate _myDelegate = delegate { };

    public Transform PlayerHand
    {
        get { return _playerHand; }
    }

    private void Awake()
    {
        _model = new PlayerModel(transform, _rb, _speed);
        view = new PlayerView(_animator);
        _controller = new PlayerController(_model, view);

        //_model.PowerUpEvent += _view.PowerUp;
    }

    private void Start()
    {
        InteractionButton.instance.Subscribe(this);
        ActivateMovement();
    }

    private void Update()
    {
        _myDelegate();
    }

    #region Interactions

    public void StopMovement()
    {
        _myDelegate = delegate { };
    }

    public void ActivateMovement()
    {
        _myDelegate = _controller.ArtificialUpdate;
    }

    public void Notify(string action)
    {
        if (action == "Interact" && _interactableObj) _model.ExecuteInteraction(_interactableObj, this, _playerHand);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            _interactableObj = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable") && _interactableObj != null)
        {
            if (other != _interactableObj.GetComponent<Collider>()) return;

            _interactableObj = null;
        }
    }

    #endregion

}
