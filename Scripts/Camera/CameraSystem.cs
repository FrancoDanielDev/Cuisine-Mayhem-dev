using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        gameObject.transform.parent = null;
    }

    private void Update()
    {
        transform.position = _target.position + _offset;
    }
}
