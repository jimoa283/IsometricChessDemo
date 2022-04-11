using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangePos : MonoBehaviour
{
    public string changeSceneName;
    public int passWord;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            MainPlayerController.Instance.passWord = passWord;
            //BattleGameManager.Instance.SmallSceneChangeEvent(changeSceneName);
            EventCenter.Instance.EventTrigger("SmallSceneChangeFadeOut", changeSceneName);
        }
    }
}
