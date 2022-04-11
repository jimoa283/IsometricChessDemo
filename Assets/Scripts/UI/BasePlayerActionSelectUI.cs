using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerActionSelectUI : MonoBehaviour
{
    protected GameObject arrow;
    protected BBActionSlot[] bBActionSlots;
    protected BasePanel basePanel;
    protected int actionIndex;

    private int ActionIndex
    {
        get => actionIndex;
        set
        {
            ResetLastAction();
            actionIndex = (value + bBActionSlots.Length) % bBActionSlots.Length;
            ChangeSelectAction();
        }
    }

    public void Init()
    {
        basePanel = GetComponentInParent<BasePanel>();
        SpecialInit();
        arrow = transform.Find("arrow").gameObject;

        actionIndex = 0;
        ChangeSelectAction();
    }

    protected virtual void SpecialInit()
    {

    }

    void Update()
    {
        if (basePanel != UIManager.Instance.GetTopPanel())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
            --ActionIndex;
        if (Input.GetKeyDown(KeyCode.S))
            ++ActionIndex;

        if (Input.GetKeyDown(KeyCode.J))
        {
            bBActionSlots[actionIndex].DoAction();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            PopThisPanel();
        }
    }

    public virtual void OpenSelectUI()
    {
        ActionIndex = 0;        
    }

    public void ChangeSelectAction()
    {
        bBActionSlots[actionIndex].BeSelected();
        arrow.transform.position = new Vector3(arrow.transform.position.x, bBActionSlots[actionIndex].transform.position.y);
    }

    public void ResetLastAction()
    {
        bBActionSlots[actionIndex].ResetSlot();
    }

    public virtual void PopThisPanel()
    {

    }
}
