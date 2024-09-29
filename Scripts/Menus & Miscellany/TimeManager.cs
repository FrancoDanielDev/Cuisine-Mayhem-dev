using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Image _blur;

    private float _timeWhenBlur = 30; // seconds

    private delegate void MyDelegate();
    private MyDelegate _myDelegate = delegate { };

    private void Start()
    {
        if (LevelManager.instance.timerOn) 
            _myDelegate = TimerRunning;
        else
            _uiManager.timerText.text = "";
    }

    private void Update()
    {
        _myDelegate();
    }

    #region Modules

    public void StopTime()
    {
        _myDelegate = delegate { };
    }

    public void ActivateTime()
    {
        _myDelegate = TimerRunning;
    }

    private void TimerRunning()
    {
        if (LevelManager.instance.levelDuration > 0.1f)
        {
            LevelManager.instance.levelDuration -= Time.deltaTime;
            UpdateTimer(LevelManager.instance.levelDuration);

            if (LevelManager.instance.levelDuration < _timeWhenBlur)
            {
                var color = new Color(_blur.color.r, _blur.color.g, _blur.color.b, _blur.color.a + 0.035f * Time.deltaTime);
                _blur.color = color;
            }
        }
        else
        {
            LevelManager.instance.levelDuration = 0;
            EventManager.instance.Trigger(EventManager.NameEvent.FinishLevel);
        }

        //if (_timeLeft <= 10 && _timeLeft >= 9.5f) CameraShake.instance.ShakeCamera();
    }

    private void UpdateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        _uiManager.timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    #endregion

}
