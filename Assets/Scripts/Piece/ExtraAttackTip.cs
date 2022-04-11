using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ExtraAttackTip : MonoBehaviour
{
    private Image extraAttackBG;
    private Image extraAttackImage;

    public void Init()
    {
        extraAttackBG = TransformHelper.GetChildTransform(transform, "ExtraAttackBG").GetComponent<Image>();
        extraAttackImage = TransformHelper.GetChildTransform(transform, "ExtraAttackImage").GetComponent<Image>();
    }

    public void ShowExtraAttack()
    {
        StopCoroutine("DoCloseExtraAttack");
        gameObject.SetActive(true);
        transform.DOLocalMoveX(0.4f, 0.3f);
        extraAttackBG.DOFade(1, 0.3f);
        extraAttackImage.DOFade(1, 0.3f);
    }

    public void CloseExtraAttack()
    {
        if(gameObject.activeInHierarchy)
        StartCoroutine("DoCloseExtraAttack");
    }

    IEnumerator DoCloseExtraAttack()
    {
        transform.DOLocalMoveX(0, 0.3f);
        extraAttackBG.DOFade(0, 0.3f);
        extraAttackImage.DOFade(0, 0.3f);
        yield return new WaitForSeconds(0.35f);
        gameObject.SetActive(false);
    }
}
