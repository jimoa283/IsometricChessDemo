using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace KasperDev.DialogueEditor
{

    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueUI;
        [Header("Text")]
        [SerializeField] private Text textName;
        [SerializeField] private Text textBox;

        [Header("Image")]
        [SerializeField] private Image leftImage;
        //[SerializeField] private GameObject leftImageGO;
        [SerializeField] private Image rightImage;
        //[SerializeField] private GameObject rightImageGO;

        [Header("Button")]
        [SerializeField] private Button button01;
        [SerializeField] private Text buttonText01;

        [Space]
        [SerializeField] private Button button02;
        [SerializeField] private Text buttonText02;

        [Space]
        [SerializeField] private Button button03;
        [SerializeField] private Text buttonText03;

        /* [Space]
         [SerializeField] private Button button04;
         [SerializeField] private Text buttonText04;*/

        [Header("Continue")]
        [SerializeField] private Button buttonContinue;

        [Header("diable interactiable")]
        [SerializeField] private Color textDisableColor;
        [SerializeField] private Color buttonDisableColor;

        [Header("interactable")]
        [SerializeField] private Color textInteractableColor;

        private List<Button> buttons = new List<Button>();
        private List<Text> buttonTexts = new List<Text>();

        private void Awake()
        {
            ShowDialogue(false);

            buttons.Add(button01);
            buttons.Add(button02);
            buttons.Add(button03);

            buttonTexts.Add(buttonText01);
            buttonTexts.Add(buttonText02);
            buttonTexts.Add(buttonText03);
        }

        public void ShowDialogue(bool _show)
        {
            dialogueUI.SetActive(_show);
        }

        public void SetText(string _textBox)
        {
            textBox.text = _textBox;
        }

        public void SetName(string text)
        {
            textName.text = text;
        }

        public void SetImage(Sprite leftImage,Sprite rightImage)
        {
            if (leftImage != null)
                this.leftImage.sprite = leftImage;

            if (leftImage != null)
                this.rightImage.sprite = rightImage;
        }

        public void HideButtons()
        {
            buttons.ForEach(button => button.gameObject.SetActive(false));
            buttonContinue.gameObject.SetActive(false);
        }

        public void SetButtons(List<DialogueButtonContainer> dialogueButtonContainers)
        {
            HideButtons();
            for (int i = 0; i < dialogueButtonContainers.Count; i++)
            {
                buttons[i].onClick = new Button.ButtonClickedEvent();
                buttons[i].interactable = true;
                buttonTexts[i].color = textInteractableColor;

                if(dialogueButtonContainers[i].ConditionCheck||dialogueButtonContainers[i].ChoiceState==ChoiceStateType.GrayOut)
                {
                    buttonTexts[i].text = $"{i + 1}:" + dialogueButtonContainers[i].Text;
                    buttons[i].gameObject.SetActive(true);

                    if(!dialogueButtonContainers[i].ConditionCheck)
                    {
                        buttons[i].interactable = false;
                        buttonTexts[i].color = textDisableColor;
                        var colors = buttons[i].colors;
                        colors.disabledColor = buttonDisableColor;
                        buttons[i].colors = colors;
                    }
                    else
                    {
                        buttons[i].onClick.AddListener(dialogueButtonContainers[i].UnityAction);
                    }
                }
            }
        }

        public void SetContinue(UnityAction unityAction)
        {
            buttonContinue.onClick = new Button.ButtonClickedEvent();
            buttonContinue.onClick.AddListener(unityAction);
            buttonContinue.gameObject.SetActive(true);
        }
    }
}
