using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CheckBuyWindow : BaseShopCheckWindow
{
    /*private Text itemNameText;
    private Text itemPriceText;
    private GameObject leftArrow;
    private GameObject rightArrow;
    private Text itemNumText;
    private Text surplusNumText;
    private Text totalMoneyText;

    private int itemNum;

    private WindowButton buyButton;
    private WindowButton cancelButton;
    private WindowButton[] windowButtons;

    private int buttonIndex;

    private Item item;

    private bool isChangeItemNumTurn;
    private bool isChangeButtonTurn;*/

    /*public int ButtonIndex { get => buttonIndex;
       set
        {
            windowButtons[buttonIndex].HideButton();
            buttonIndex = (value + windowButtons.Length) % windowButtons.Length;
            windowButtons[buttonIndex].ShowButton();
        }   
     }
    public int ItemNum { get => itemNum;
        set
        {
            ChangeBuyNum(value);
        }
    }*/

    private WindowButton buyButton;

   /* public void Init()
    {
        itemNameText = TransformHelper.GetChildTransform(transform, "ItemName").GetComponent<Text>();
        itemPriceText = TransformHelper.GetChildTransform(transform, "ItemPrice").GetComponent<Text>();
        leftArrow = TransformHelper.GetChildTransform(transform, "LeftArrow").gameObject;
        rightArrow = TransformHelper.GetChildTransform(transform, "RightArrow").gameObject;
        itemNumText = TransformHelper.GetChildTransform(transform, "ItemNumText").GetComponent<Text>();
        surplusNumText = TransformHelper.GetChildTransform(transform, "SurplusNum").GetComponent<Text>();
        totalMoneyText = TransformHelper.GetChildTransform(transform, "TotalNum").GetComponent<Text>();

        buyButton = TransformHelper.GetChildTransform(transform, "BuyButton").GetComponent<WindowButton>();
        buyButton.Init(ConfirmBuy);

        cancelButton = TransformHelper.GetChildTransform(transform, "CancelButton").GetComponent<WindowButton>();
        cancelButton.Init(ConfirmCancel);

        
    }*/

    /*protected override void SpecialInit()
    {
        buyButton = TransformHelper.GetChildTransform(transform, "BuyButton").GetComponent<WindowButton>();
        buyButton.Init(ConfirmBuy);

        windowButtons = new WindowButton[] { buyButton, cancelButton };
    }*/

    /*private void Update()
    {
        if(isChangeItemNumTurn)
        {
            if (Input.GetKeyDown(KeyCode.A) && leftArrow.activeInHierarchy)
            {
                --ItemNum;
            }

            if (Input.GetKeyDown(KeyCode.D) && rightArrow.activeInHierarchy)
            {
                ++ItemNum;
            }

            if(Input.GetKeyDown(KeyCode.J))
            {
                ChangeToSetButton();
            }
        }
       
        if(isChangeButtonTurn)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                ++ButtonIndex;
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                --ButtonIndex;
            }

            if(Input.GetKeyDown(KeyCode.I))
            {
                ChangeToSetNum();
            }
        }
    }*/

    /*public void SetBuyWindow(Item item)
    {
        this.item = item;
        itemNameText.text = item.Name;
        itemPriceText.text = item.BuyPrice.ToString();

        ItemNum = 1;

        transform.localScale = new Vector3(0.1f, 0.1f);
        transform.DOScale(Vector3.one, 0.3f);
    }*/

    /*private void ChangeToSetButton()
    {
        isChangeItemNumTurn = false;
        isChangeButtonTurn = true;

        ButtonIndex = 0;
    }

    private void ChangeToSetNum()
    {
        isChangeButtonTurn = false;
        isChangeItemNumTurn = true;
    }*/

    protected override void ChangeBuyNum(int num)
    {
        itemNum = num;
        itemNumText.text = num.ToString();
        surplusNumText.text = "剩余"+(item.Number - num);
        if (num == 1)
            leftArrow.SetActive(false);
        else
            leftArrow.SetActive(true);

        int totalPrice = num * item.BuyPrice;
        totalMoneyText.text = totalPrice.ToString();
        if (num == item.Number || totalPrice+item.BuyPrice > GameManager.Instance.money)
            rightArrow.SetActive(false);
        else
            rightArrow.SetActive(true);
    }

    protected override void ConfirmAction()
    {

        ItemManager.Instance.ChangeItemNum(ItemManager.Instance.GetNumItem(item.ID,0),itemNum);

        item.Number -= itemNum;
        GameManager.Instance.money -= itemNum * item.BuyPrice;

        UIManager.Instance.PopPanel(UIPanelType.CheckBuyNumUI);
    }


    protected override void ConfirmCancel()
    {
        UIManager.Instance.PopPanel(UIPanelType.CheckBuyNumUI);
    }

    public void OpenBuyItemShopWindow(Item item)
    {
        SetItemShopWindow(item);
    }

    protected override void SpecialInit()
    {
        EventCenter.Instance.AddEventListener<Item>("OpenBuyItemShopWindow", OpenBuyItemShopWindow);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEvent<Item>("OpenBuyItemShopWindow", OpenBuyItemShopWindow);
    }

    public override void SetItemShopWindow(Item item)
    {
        itemPriceText.text = item.BuyPrice.ToString();
        base.SetItemShopWindow(item);
    }
}
