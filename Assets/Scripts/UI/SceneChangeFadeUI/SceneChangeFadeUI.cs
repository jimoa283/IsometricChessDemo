using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class SceneChangeFadeUI : MonoBehaviour
{
    private Image fadeImage;
    public float sceneChangeTime;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);
        EventCenter.Instance.AddEventListener<string>("BigSceneChangeFadeOut", BigSceneChangeFadeOut);
        EventCenter.Instance.AddEventListener<string>("SmallSceneChangeFadeOut", SmallSceneChangeFadeOut);
        EventCenter.Instance.AddEventListener<UnityAction>("SceneStartFadeIn", SceneStartFadeIn);
    }


    private void BigSceneChangeFadeOut(string sceneName)
    {
        UIManager.Instance.ClearDic();
        StartCoroutine(DoSceneChangeFadeOut(BattleGameManager.Instance.BigSceneChangeEvent,sceneName));
    }

    private void SmallSceneChangeFadeOut(string sceneName)
    {
        StartCoroutine(DoSceneChangeFadeOut(BattleGameManager.Instance.SmallSceneChangeEvent, sceneName));
    }

    IEnumerator DoSceneChangeFadeOut(UnityAction<string> action,string sceneName)
    {
        fadeImage.color = new Color(0,0,0,0);
        fadeImage.DOFade(1,sceneChangeTime).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(sceneChangeTime+0.05f);
        action?.Invoke(sceneName);
    }

    private void SceneStartFadeIn(UnityAction action)
    {
        StartCoroutine(DoSceneChangeFadeIn(action));
    }

    IEnumerator DoSceneChangeFadeIn(UnityAction action)
    {
        fadeImage.color = Color.black;
        fadeImage.DOFade(0,sceneChangeTime).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(sceneChangeTime/2);
        action?.Invoke();
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<string>("BigSceneChangeFadeOut", BigSceneChangeFadeOut);
        EventCenter.Instance.RemoveEvent<string>("SmallSceneChangeFadeOut", SmallSceneChangeFadeOut);
        EventCenter.Instance.RemoveEvent<UnityAction>("SceneStartFadeIn", SceneStartFadeIn);
    }
}
