using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class PieceIntroduceUI : MonoBehaviour
{
    private Image pieceImage;
    private Image pieceCampFlagImage;
    private Text pieceInfoText;
    private UnityAction action;
    private CanvasGroup canvasGroup;

    public void Init()
    {
        pieceImage = TransformHelper.GetChildTransform(transform, "PieceImage").GetComponent<Image>();
        pieceCampFlagImage = TransformHelper.GetChildTransform(transform, "PieceCampFlag").GetComponent<Image>();
        pieceInfoText = TransformHelper.GetChildTransform(transform, "PieceInfoText").GetComponent<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
         if(Input.GetKeyDown(KeyCode.J))
        {
            ExitPieceIntroduce();
        }
    }

    public void SetPieceIntroduce(PieceIntroduceData pieceIntroduceData,UnityAction action)
    {
        gameObject.SetActive(true);
        pieceImage.sprite = pieceIntroduceData.PieceSprite;
        pieceCampFlagImage.sprite = pieceIntroduceData.PieceCampFlag;
        pieceInfoText.text = pieceIntroduceData.PieceInfo;
        this.action = action;
        transform.localScale = new Vector3(1, 0.5f, 1);
        canvasGroup.alpha = 0;
        transform.DOScaleY(1, 0.2f);
        canvasGroup.DOFade(1, 0.2f);
    }

    private void ExitPieceIntroduce()
    {
        StartCoroutine(DoExitPieceIntroduce());
    }

    IEnumerator DoExitPieceIntroduce()
    {
        canvasGroup.DOFade(0, 0.2f);
        transform.DOScaleY(0.5f, 0.2f);
        yield return new WaitForSeconds(0.25f);
        action?.Invoke();
        gameObject.SetActive(false);
    }
}
