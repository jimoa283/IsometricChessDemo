using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIDevice : MonoBehaviour
{
    void Start()
    {
        MainPlayerController.Instance.gameObject.SetActive(false);
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.StartGameUI));
    }

   
}
