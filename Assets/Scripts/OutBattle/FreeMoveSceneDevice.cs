using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreeMoveSceneDevice : MonoBehaviour
{
    public Entrance[] entrances;

    void Start()
    {
        MainPlayerController.Instance.gameObject.SetActive(true);
        MainPlayerController.Instance.canControll = false;
        if(entrances.Length>0)
        {
            int temp = MainPlayerController.Instance.passWord;
            MainPlayerController.Instance.transform.position = entrances[temp].enterPos;
            MainPlayerController.Instance.SetIdleDirection(entrances[temp].lookDirection);
        }
        
        EventCenter.Instance.EventTrigger<UnityAction>("SceneStartFadeIn", Init);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
          EventCenter.Instance.EventTrigger("BigSceneChangeFadeOut", "Map");
    }

    protected virtual void Init()
    {
        MainPlayerController.Instance.canControll = true;
    }
}

[System.Serializable]
public class Entrance
{
    public Vector3 enterPos;
    public LookDirection lookDirection;
}
