using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPMovement : MonoBehaviour
{
    public Vector3 myDirection;
    public Quaternion rotat;

    [SerializeField] protected ParticleSystem _part1;
    [Space]
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected float _timeOnAir = 1.5f;
    [Space]

    private Vector3 _direction;
    private float _radiusFix = 0.01f;

    private delegate void MyDelegate();
    private MyDelegate _myDelegate = delegate { };

    private void Update()
    {
        _myDelegate();
    }

    public IEnumerator TP()
    {
        _part1.Play();

        yield return new WaitForSeconds(3f);

        AirMovement();

        yield return new WaitForSeconds(_timeOnAir);

        LandMovement();
    }

    private void AirMovement()
    {
        Vector3 movement = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        _direction = (movement - transform.position).normalized;

        _myDelegate = delegate { };
        _myDelegate = Movement;
    }

    private void LandMovement()
    {
        transform.position = new Vector3
            (myDirection.x, transform.position.y, myDirection.z);

        transform.rotation = rotat;

        _myDelegate = SecondMovement;
    }

    private void SecondMovement()
    {
        if (Vector3.Distance(myDirection, transform.position) > _radiusFix)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, myDirection, _speed * Time.deltaTime);
        }
        else
        {
            _part1.Stop();
            _myDelegate = delegate { };
        }
    }

    private void Movement()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
