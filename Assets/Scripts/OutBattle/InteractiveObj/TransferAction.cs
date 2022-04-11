using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferAction : InteractiveAction
{
    public TransferAction theOtherTransferDevice;
    public Vector3 setPos;
    public Vector3 actionPos;
    public LookDirection lookDirection;
    public string animName;
    public int speed;

    public override void DoAction()
    {
        interactiveObject.BaseActionOnClick();
        BaseTransferAction();
    }


    private void BaseTransferAction()
    {
        MainPlayerController.Instance.GetComponent<BoxCollider2D>().enabled = false;
        MainPlayerController.Instance.SetIdleDirection(lookDirection);
        MainPlayerController.Instance.transform.position = actionPos;        
        MainPlayerController.Instance.GetComponent<SpriteRenderer>().sortingLayerName = "Select";
        if (animName != "")
        {
            MainPlayerController.Instance.PlayAnim(animName);
        }

        StartCoroutine(DoTransferAction());
    }

    IEnumerator DoTransferAction()
    {
        while(Vector2.Distance(MainPlayerController.Instance.transform.position,theOtherTransferDevice.actionPos)>0.01f)
        {
            yield return null;
            MainPlayerController.Instance.transform.position = Vector2.MoveTowards(MainPlayerController.Instance.transform.position, theOtherTransferDevice.actionPos, Time.deltaTime * speed);
        }

        MainPlayerController.Instance.SetIdle();
        MainPlayerController.Instance.transform.position = theOtherTransferDevice.setPos;
        MainPlayerController.Instance.GetComponent<BoxCollider2D>().enabled = true;
        ActionAfterAllSpeak();
    }
}
