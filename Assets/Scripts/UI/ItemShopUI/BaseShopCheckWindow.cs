using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BaseShopCheckWindow : MonoBehaviour
{
    protected Text itemNameText;
    protected Text itemPriceText;
    protected GameObject leftArrow;
    protected GameObject rightArrow;
    protected Text itemNumText;
    protected Text surplusNumText;
    protected Text totalMoneyText;

    protected int itemNum;

    protected WindowButton actionButton;
    protected WindowButton cancelButton;
    protected WindowButton[] windowButtons;

    protected int buttonIndex;

    protected Item item;

    [SerializeField]protected bool isChangeItemNumTurn;
    [SerializeField]protected bool isChangeButtonTurn;

    public int ButtonIndex
    {
        get => buttonIndex;
        set
        {
            windowButtons[buttonIndex].HideButton();
            buttonIndex = (value + windowButtons.Length) % windowButtons.Length;
            windowButtons[buttonIndex].ShowButton();
        }
    }
    public int ItemNum
    {
        get => itemNum;
        set
        {
            ChangeBuyNum(value);
        }
    }

    public void Init()
    {
        SpecialInit();
        itemNameText = transform.GetChildTransform( "ItemName").GetComponent<Text>();
        itemPriceText = transform.GetChildTransform("ItemPrice").GetComponent<Text>();
        leftArrow = transform.GetChildTransform("LeftArrow").gameObject;
        rightArrow = transform.GetChildTransform("RightArrow").gameObject;
        itemNumText = transform.GetChildTransform("ItemNumText").GetComponent<Text>();
        surplusNumText = transform.GetChildTransform("SurplusNum").GetComponent<Text>();
        totalMoneyText = transform.GetChildTransform("TotalNum").GetComponent<Text>();

        actionButton = transform.GetChildTransform("ActionButton").GetComponent<WindowButton>();
        actionButton.Init(ConfirmAction);

        cancelButton = transform.GetChildTransform("CancelButton").GetComponent<WindowButton>();
        cancelButton.Init(ConfirmCancel);

        windowButtons = new WindowButton[] { actionButton, cancelButton };
        
    }

    protected virtual void SpecialInit()
    {

    }

    private void Update()
    {
        if (isChangeItemNumTurn)
        {
            if (Input.GetKeyDown(KeyCode.A) && leftArrow.activeInHierarchy)
            {
                --ItemNum;
            }

            if (Input.GetKeyDown(KeyCode.D) && rightArrow.activeInHierarchy)
            {
                ++ItemNum;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                ChangeToSetButton();
            }
        }
        else if (isChangeButtonTurn)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                --ButtonIndex;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ++ButtonIndex;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                ChangeToSetNum();
            }

            if(Input.GetKeyDown(KeyCode.J))
            {
                windowButtons[buttonIndex].DoAction();
            }
        }
    }

    public virtual void SetItemShopWindow(Item item)
    {
        ChangeToSetNum();
        this.item = item;
        itemNameText.text = item.Name;
        //itemPriceText.text = item.BuyPrice.ToString();

        ItemNum = 1;

        transform.localScale = new Vector3(0.1f, 0.1f);
        transform.DOScale(Vector3.one, 0.3f);
    }

    protected void ChangeToSetButton()
    {
        isChangeItemNumTurn = false;
        isChangeButtonTurn = true;

        ButtonIndex = 0;
    }

    protected void ChangeToSetNum()
    {
        isChangeButtonTurn = false;
        isChangeItemNumTurn = true;
        foreach(var button in windowButtons)
        {
            button.HideButton();
        }
    }

    protected virtual void ChangeBuyNum(int num)
    {
       /* itemNum = num;
        itemNumText.text = num.ToString();
        surplusNumText.text = "剩余" + (item.Number - num);
        if (num == 1)
            leftArrow.SetActive(false);
        else
            leftArrow.SetActive(true);

        int totalPrice = num * item.BuyPrice;
        totalMoneyText.text = totalPrice.ToString();
        if (num == item.Number || totalPrice + item.BuyPrice > GameManager.Instance.money)
            rightArrow.SetActive(false);
        else
            rightArrow.SetActive(true);*/
    }

    protected virtual void ConfirmAction()
    {
        
    }

    protected virtual void ConfirmCancel()
    {
        
    }
}
