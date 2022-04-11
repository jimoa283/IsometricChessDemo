using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PieceHealthBar : MonoBehaviour
{
    private Image healthBarW;
    private Image healthBarG;
    private Image healthBarR;
    private Piece piece;
    private float speed =0.5f;

    public void Init(Piece piece)
    {
        healthBarW = TransformHelper.GetChildTransform(transform, "HealthBarW").GetComponent<Image>();
        healthBarR = TransformHelper.GetChildTransform(transform, "HealthBarR").GetComponent<Image>();
        healthBarG = TransformHelper.GetChildTransform(transform, "HealthBarG").GetComponent<Image>();
        this.piece = piece;
        //piece = GetComponentInParent<Piece>();
    }

    public void SetHealthDown(int oriHealth,int changeValue,int maxHealth)
    {
        gameObject.SetActive(true);

        healthBarG.gameObject.SetActive(false);
        healthBarR.gameObject.SetActive(true);

        healthBarR.fillAmount =(float)oriHealth / maxHealth;
        healthBarW.fillAmount = (float)oriHealth / maxHealth;

        StartCoroutine(DoHealthChange(oriHealth, changeValue, maxHealth));
    }

    public void SetHealthUp(int oriHealth,int changeValue,int maxHealth)
    {
        gameObject.SetActive(true);

        healthBarG.gameObject.SetActive(true);
        healthBarR.gameObject.SetActive(false);

        healthBarG.fillAmount = (float)changeValue+oriHealth / maxHealth;
        healthBarW.fillAmount = (float)oriHealth / maxHealth;

        StartCoroutine(DoHealthChange(oriHealth, changeValue, maxHealth));
    }

    IEnumerator DoHealthChange(int oriHealth, int changeValue, int maxHealth)
    {
        float percent = 0;

        float currentHealth = oriHealth + changeValue >= 0 ? oriHealth + changeValue : 0;

        piece.pieceStatus.CurrentHealth = (int)currentHealth;

        while (percent<1)
        {
            yield return null;
            percent += 1 / speed*Time.deltaTime;
            healthBarW.fillAmount = Mathf.Lerp(((float)oriHealth / maxHealth), (currentHealth / maxHealth), percent);
        }

        healthBarW.fillAmount = (currentHealth / maxHealth);

        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
            
    }
}
