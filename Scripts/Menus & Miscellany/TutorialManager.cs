using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] private bool _containsTutorial = false;
    [Space]
    [SerializeField] private GameObject _tutorial;
    [Space]
    [SerializeField] private GameObject _one;
    [SerializeField] private GameObject _two;
    [Space]
    [SerializeField] private GameObject _chest1;
    [SerializeField] private GameObject _chest2;
    [Space]
    [SerializeField] private GameObject _cabinet1;
    [SerializeField] private GameObject _cabinet2;
    [Space]
    [SerializeField] private GameObject _nine;
    [Space]
    [SerializeField] private GameObject _tutoCard;
    [SerializeField] private Animator _tutoCardAnimator;
    [SerializeField] private TMP_Text _tutoCardText;

    private Action _function;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        if (!_containsTutorial) return;

        _tutorial.SetActive(true);
        NextStep(One);
        TriggerFunction();
    }

    public void TriggerFunction()
    {
        _function.Invoke();
    }

    private void NextStep(Action method)
    {
        _function = method;
    }

    private void One()
    {
        Time.timeScale = 0f;
        _one.SetActive(true);

        NextStep(Two);
    }

    private void Two()
    {
        _one.SetActive(false);
        _two.SetActive(true);

        NextStep(Three);
    }

    private void Three()
    {
        _two.SetActive(false);
        Time.timeScale = 1f;

        _tutoCard.SetActive(true);

        _tutoCardText.text = "Go to a chest and grab meet.";

        NextStep(Four);
    }

    private void Four()
    {
        _chest1.GetComponent<BoxCollider>().enabled = false;
        _chest2.GetComponent<BoxCollider>().enabled = false;

        _tutoCardAnimator.SetTrigger("Change");
        _tutoCardText.text = "Go to the oven and cook the meat.";

        AudioManager.instance.Play("Correct");

        NextStep(Five);
    }

    private void Five()
    {
        _cabinet1.GetComponent<BoxCollider>().enabled = true;
        _cabinet2.GetComponent<BoxCollider>().enabled = true;

        _tutoCardAnimator.SetTrigger("Change");
        _tutoCardText.text = "Grab a Plate.";

        AudioManager.instance.Play("Correct");

        NextStep(Six);
    }

    private void Six()
    {
        _tutoCardAnimator.SetTrigger("Change");
        _tutoCardText.text = "Go and take the cooked meat.";

        AudioManager.instance.Play("Correct");

        NextStep(Seven);
    }

    private void Seven()
    {
        _tutoCardAnimator.SetTrigger("Change");
        _tutoCardText.text = "Go to the Delivery Table and deliver the order.";

        AudioManager.instance.Play("Correct");

        NextStep(Eight);
    }

    private void Eight()
    {
        _tutoCardAnimator.SetTrigger("Change");
        _tutoCardText.text = "Grab the plate and wash it.";

        NextStep(Nine);
    }

    private void Nine()
    {
        AudioManager.instance.Play("Correct");
        _tutoCard.SetActive(false);

        Time.timeScale = 0f;
        _nine.SetActive(true);

        NextStep(TheEnd);
    }

    private void TheEnd()
    {
        Time.timeScale = 1f;
    }
}
