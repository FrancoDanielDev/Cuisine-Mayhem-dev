using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] CanvasGroup loadScreen = null;
    [SerializeField] Image loadBar = null;

    //[SerializeField] AudioSource _audio;
    //[SerializeField] string _audio2;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string[] _tips;

    private void Start()
    {
        _text.text = _tips[Random.Range(0, _tips.Length)];
    }

    public void AsyncLoadScene(int level)
    {
        var async = SceneManager.LoadSceneAsync(level);
        //var async = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync(0);

        StartCoroutine(WaitToLoadScene(async));
    }

    IEnumerator WaitToLoadScene(AsyncOperation async)
    {
        async.allowSceneActivation = false;
        int frames = 0;
        loadScreen.alpha = 1;

        /*if (_audio != null)
            _audio.Stop();

        else if (_audio2 != null)
            AudioManager.instance.Stop(_audio2);*/

        while (async.progress < 0.89)
        {
            loadBar.fillAmount = async.progress;
            frames += 1;
            //Debug.Log(async.progress);
            yield return new WaitForEndOfFrame();
        }

        //Debug.Log("Tardó " + frames + " frames");
        while (frames < 300)
        {
            frames += 1;
            loadBar.fillAmount = async.progress;
            yield return new WaitForEndOfFrame();
        }

        loadScreen.alpha = 0;
        async.allowSceneActivation = true;
    }
}
