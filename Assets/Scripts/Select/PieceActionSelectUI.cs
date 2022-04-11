using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceActionSelectUI : MonoBehaviour
{
    private GameObject confirmObj;
    private GameObject actionFinish;
    private GameObject showPieceInfo;
    //private GameObject otherPieceInfo;
    private GameObject settingChange;
    private GameObject remove;
    private SimplePieceInformationShow simplePieceInformationShow;

    private UnityAction<Cell> setActiveSelectAction;

    public UnityAction<Cell> SetActiveSelectAction { get => setActiveSelectAction;  }

    public void Init()
    {
        confirmObj = TransformHelper.GetChildTransform(transform, "Confirm").gameObject;
        actionFinish = TransformHelper.GetChildTransform(transform, "ActionFinish").gameObject;
        showPieceInfo = TransformHelper.GetChildTransform(transform, "ShowPieceInfo").gameObject;
        settingChange = TransformHelper.GetChildTransform(transform, "SettingChange").gameObject;
        remove = TransformHelper.GetChildTransform(transform, "Remove").gameObject;
        simplePieceInformationShow = TransformHelper.GetChildTransform(transform, "OtherPieceInfo").GetComponent<SimplePieceInformationShow>();
        simplePieceInformationShow.Init();
    }


    public void ChangeSetPieceSelectAction()
    {
        confirmObj.SetActive(false);
        actionFinish.SetActive(false);
        setActiveSelectAction = SetActiveSelectBeforeBattle;
    }

    public void ChangeChangePlayerSettingPosAction()
    {
        actionFinish.SetActive(false);
        showPieceInfo.SetActive(false);
        settingChange.SetActive(false);
        setActiveSelectAction = SetActiveChangePlayerSettingPos;
    }

    public void ChangeChooseActionSelectAction()
    {
        gameObject.SetActive(true);
        settingChange.SetActive(false);
        setActiveSelectAction = SetActiveSelectForChooseAction;
    }

    public void ChangeChooseActionPosSelectAction()
    {
        gameObject.SetActive(true);
        actionFinish.SetActive(false);
        setActiveSelectAction = SetActiveSelectForChooseActionPos;
    }

    public void SetActiveSelect(Cell cell)
    {
        setActiveSelectAction?.Invoke(cell);
    }

    public void SetActiveSelectBeforeBattle(Cell cell)
    {
        if(cell.currentPiece!=null)
        {
            /*if (cell.currentPiece != LevelManager.Instance.CurrentPiece)
                EventCenter.Instance.EventTrigger<int>("ShowSelectPieceUIPlace", PieceQueueManager.Instance.GetPieceActionIndex(cell.currentPiece));
            else
                EventCenter.Instance.EventTrigger("ReturnToFirstPiece");*/
            gameObject.SetActive(true);
            if (cell.currentPiece.CompareTag("Player"))
            {
                if (PieceQueueManager.Instance.PlayerList.Count > 1&&!BattleGameManager.Instance.importantPlayerList.Contains(cell.currentPiece as Player))
                    remove.SetActive(true);
                else
                    remove.SetActive(false);

                settingChange.SetActive(true);
            }              
            else
            {
                settingChange.SetActive(false);
                remove.SetActive(false);
            }                
            showPieceInfo.SetActive(true);
            //simplePieceInformationShow.gameObject.SetActive(true);
            simplePieceInformationShow.ShowPieceSimpleInformation(cell.currentPiece);
            //otherPieceInfo.SetActive(true);
        }
        else
        {
            //EventCenter.Instance.EventTrigger("ReturnToFirstPiece");
            gameObject.SetActive(false);
        }
    }

    public void SetActiveChangePlayerSettingPos(Cell cell)
    {
        if(cell.currentPiece!=null)
        {
            gameObject.SetActive(true);
            simplePieceInformationShow.ShowPieceSimpleInformation(cell.currentPiece);

            if(cell.currentPiece.CompareTag("Player")&&cell.currentPiece!=Select.Instance.ChangePiece)
            {
                confirmObj.SetActive(true);               
            }
            else
            {
                confirmObj.SetActive(false);
            }
        }
        else
        {
            simplePieceInformationShow.gameObject.SetActive(false);
            confirmObj.SetActive(false);
        }
        //gameObject.SetActive(false);
    }

    public void SetActiveSelectForChooseAction(Cell cell)
    {
        if (cell.canMove || cell.currentPiece == LevelManager.Instance.CurrentPiece||(!cell.canMove&&cell.currentPiece!=null))
            gameObject.SetActive(true);
        else
        {
            gameObject.SetActive(false);
            return;
        }

        settingChange.gameObject.SetActive(false);

        if (cell.currentPiece == null)
        {
            actionFinish.SetActive(true);
            confirmObj.SetActive(true);
            showPieceInfo.SetActive(false);
            //otherPieceInfo.SetActive(false);
            simplePieceInformationShow.gameObject.SetActive(false);
            //EventCenter.Instance.EventTrigger<List<Piece>>("SetPiecesUI", PieceQueueManager.Instance.GetPieceQueue());
            return;
        }
        else if (cell.currentPiece != LevelManager.Instance.CurrentPiece)
        {
            showPieceInfo.SetActive(true);
            simplePieceInformationShow.ShowPieceSimpleInformation(cell.currentPiece);
            //otherPieceInfo.SetActive(true);
            actionFinish.SetActive(false);
            confirmObj.SetActive(false);
            //EventCenter.Instance.EventTrigger<int>("ShowSelectPieceUIPlace", PieceQueueManager.Instance.GetPieceActionIndex(cell.currentPiece));
            return;
        }
        else
        {
            actionFinish.SetActive(true);
            confirmObj.SetActive(true);
            showPieceInfo.SetActive(true);
            simplePieceInformationShow.gameObject.SetActive(false);
            //otherPieceInfo.SetActive(false);
            return;
        }            
    }

    public void SetActiveSelectForChooseActionPos(Cell cell)
    {

        if (EffectRangeManager.Instance.CheckRightPos()&& LevelManager.Instance.CurrentPiece.CompareTag("Player"))
        {
            confirmObj.SetActive(true);
        }
        else
        {
            confirmObj.SetActive(false);
        }
        

        if(cell.currentPiece!=null)
        {
            if (LevelManager.Instance.CurrentPiece.CompareTag("Player"))
                showPieceInfo.SetActive(true);
            else
                showPieceInfo.SetActive(false);

            if(cell.currentPiece!=LevelManager.Instance.CurrentPiece)
            {
                simplePieceInformationShow.ShowPieceActionEffectInformation(cell.currentPiece);
            }
            else
            {
                simplePieceInformationShow.gameObject.SetActive(false);
            }
        }
        else
        {
            showPieceInfo.SetActive(false);
            //otherPieceInfo.SetActive(false);
            simplePieceInformationShow.gameObject.SetActive(false);
        }
    }


    public void ShowPieceInfo()
    {
        if (!showPieceInfo.activeInHierarchy)
            return;
        UIManager.Instance.PushPanel(UIManager.Instance.GetPanel(UIPanelType.PieceInformationUI));
        Select.Instance.canMove = false;
        gameObject.SetActive(false);
    }

    public bool ShowPieceInfoActive()
    {
        return showPieceInfo.activeInHierarchy;
    }

    public bool ConfirmObjActive()
    {
        return confirmObj.activeInHierarchy;
    }

    public bool ActionFinishActive()
    {
        return actionFinish.activeInHierarchy;
    }

    public bool SettingChangeActive()
    {
        return settingChange.activeInHierarchy;
    }

    public bool RemoveActive()
    {
        return remove.activeInHierarchy;
    }
}
