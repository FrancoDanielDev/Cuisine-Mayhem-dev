using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text counterText;
    public Text satisfiedCustomerText;
    public Text coins;
    public GameObject finishedLevelScreen;

    private void Start()
    {
        StartCoroutine(Tutorial());
    }

    #region Modules

    private IEnumerator Tutorial()
    {
        yield return new WaitForEndOfFrame();

        if (SaveWithPlayerPrefs.instance.hasSeenTutorial == 0)
        {
            ShowTutorial();
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void ShowTutorial()
    {
        Time.timeScale = 0;

        SaveWithPlayerPrefs.instance.hasSeenTutorial = 1;
        SaveWithPlayerPrefs.instance.SaveGame();       
    }

    public void ActivateLevel()
    {
        Time.timeScale = 1;
    }

    #endregion

}
