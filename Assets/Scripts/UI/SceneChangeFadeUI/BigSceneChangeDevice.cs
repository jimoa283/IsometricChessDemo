using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigSceneChangeDevice : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DoSceneChange());
    }

    IEnumerator DoSceneChange()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(BattleGameManager.Instance.NextSceneName);
        operation.allowSceneActivation = false;

        while(!operation.isDone)
        {
            if(operation.progress>=0.9f)
            {
                operation.allowSceneActivation = true;
            }
                
            yield return null;
        }
    }
}
