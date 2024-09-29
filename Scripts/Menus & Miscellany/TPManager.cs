using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPManager : MonoBehaviour
{
    [SerializeField] private bool _tpAvailable = false;
    [Space]
    [SerializeField] private TPMovement[] _batch1;
    [SerializeField] private TPMovement[] _batch2;
    [SerializeField] private TPMovement[] _batch3;
    [SerializeField] private TPMovement[] _batch4;
    [Space]
    [SerializeField] private float _cooldown = 32f;

    private bool _step;
    private bool _step2;

    private bool _theStep;

    private float _time;

    private void Start()
    {
        var ran = Random.Range(0, 2);      

        if (ran == 0)
            _step = true;
        else
            _step = false;


        var ran2 = Random.Range(0, 2);

        if (ran2 == 0)
            _step2 = true;
        else
            _step2 = false;

        var ran3 = Random.Range(0, 2);

        if (ran3 == 0)
            _theStep = true;
        else
            _theStep = false;
    }

    private void Update()
    {
        if (!_tpAvailable) return;

        _time += Time.deltaTime;

        if (_time >= _cooldown)
        {
            _time = 0;

            if (_theStep)
            {
                TPStuff();
            }
            else
            {
                TPDelivery();
            }

            _theStep = !_theStep;
        }
    }

    private void TPStuff()
    {
        _time = 0;

        AudioManager.instance.Play("Magic1");

        if (_step)
        {
            var w = _batch1[0].transform.position;
            var y = _batch1[0].transform.rotation;

            _batch1[0].myDirection = _batch1[1].transform.position;
            _batch1[0].rotat = _batch1[1].transform.rotation;

            _batch1[1].myDirection = w;
            _batch1[1].rotat = y;

            StartCoroutine(_batch1[0].TP());
            StartCoroutine(_batch1[1].TP());
        }
        else
        {
            var w = _batch2[0].transform.position;
            var y = _batch2[0].transform.rotation;

            _batch2[0].myDirection = _batch2[1].transform.position;
            _batch2[0].rotat = _batch2[1].transform.rotation;

            _batch2[1].myDirection = w;
            _batch2[1].rotat = y;

            StartCoroutine(_batch2[0].TP());
            StartCoroutine(_batch2[1].TP());
        }

        _step = !_step;
    }

    private void TPDelivery()
    {
        AudioManager.instance.Play("Magic2");

        if (_step2)
        {
            var w = _batch3[0].transform.position;
            var y = _batch3[0].transform.rotation;

            _batch3[0].myDirection = _batch3[1].transform.position;
            _batch3[0].rotat = _batch3[1].transform.rotation;

            _batch3[1].myDirection = w;
            _batch3[1].rotat = y;

            StartCoroutine(_batch3[0].TP());
            StartCoroutine(_batch3[1].TP());
        }
        else
        {
            var w = _batch4[0].transform.position;
            var y = _batch4[0].transform.rotation;

            _batch4[0].myDirection = _batch4[1].transform.position;
            _batch4[0].rotat = _batch4[1].transform.rotation;

            _batch4[1].myDirection = w;
            _batch4[1].rotat = y;

            StartCoroutine(_batch4[0].TP());
            StartCoroutine(_batch4[1].TP());
        }

        _step2 = !_step2;
    }
}
