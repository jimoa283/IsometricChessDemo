using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthChangeNumText : MonoBehaviour
{
    private Text value1;
    private Text value2;
    private Text value3;
    private GameObject critical;
    public Color red;
    public Color green;
    public Color yellow;

    public void Init(bool isRed,Piece piece,int num)
    {
       
        if(value1==null)
        value1 = TransformHelper.GetChildTransform(transform, "Value1").GetComponent<Text>();

        if(value2==null)
        value2 = TransformHelper.GetChildTransform(transform, "Value2").GetComponent<Text>();

        if(value3==null)
        value3 = TransformHelper.GetChildTransform(transform, "Value3").GetComponent<Text>();

        if (critical == null)
            critical = TransformHelper.GetChildTransform(transform, "Critical").gameObject;

        Color temp;
        if (num < 10000)
        {
            temp = isRed ? red : green;
            critical.SetActive(false);
        }
        else
        {
            critical.SetActive(true);
            temp = yellow;
            while(num>10000)
            {
                num -= 10000;
            }
        }

        string _num = num.ToString();
        char[] tempString = _num.ToCharArray();


        value1.color = temp;
        value2.color = temp;
        value3.color = temp;

        //transform.SetParent(piece.transform);
        //transform.localPosition = Vector3.zero;
        transform.position = piece.transform.position;

        if (tempString.Length==1)
        {
            value2.gameObject.SetActive(false);
            value3.gameObject.SetActive(false);
            value1.text = tempString[0].ToString();
            StartCoroutine(DoOneShake());
        }
         else if(tempString.Length==2)
        {
            value3.gameObject.SetActive(false);
            value2.gameObject.SetActive(true);

            value2.text = tempString[1].ToString();
            value1.text = tempString[0].ToString();
            StartCoroutine(DoTwoShake());
        }
        else
        {
            value3.gameObject.SetActive(true);
            value2.gameObject.SetActive(true);

            value3.text = tempString[2].ToString();
            value2.text = tempString[1].ToString();
            value1.text = tempString[0].ToString();
            StartCoroutine(DoThreeShake());
        }
        
       
        //StartCoroutine(DoFade());
    }

    /*IEnumerator DoFade()
    {
        transform.DOShakePosition(0.3f, new Vector3(1f, 1f, 1f));
        yield return new WaitForSeconds(0.3f);
        value1.DOFade(0, 0.4f);
        yield return new WaitForSeconds(0.45f);
        PoolManager.Instance.PushObj("HealthChangeNumText", gameObject);
    }*/

    IEnumerator DoOneShake()
    {
        value1.transform.DOShakePosition(0.3f, new Vector3(1, 0, 0));
        yield return new WaitForSeconds(0.3f);
        value1.DOFade(0, 0.4f);
        yield return new WaitForSeconds(0.45f);
        PoolManager.Instance.PushObj("HealthChangeNumText", gameObject);
    }

    IEnumerator DoTwoShake()
    {
        value1.transform.DOShakePosition(0.25f, Vector3.up*300,6,100,true).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.05f);
        value2.transform.DOShakePosition(0.25f, Vector3.up*200,6,100,true).SetEase(Ease.OutQuart);
        yield return new WaitForSeconds(0.2f);
        value1.DOFade(0,0.3f).SetEase(Ease.InQuint);
        value2.DOFade(0,0.3f).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(0.35f);
        PoolManager.Instance.PushObj("HealthChangeNumText", gameObject);
    }

    IEnumerator DoThreeShake()
    {
        value1.transform.DOShakePosition(0.3f, new Vector3(0, 0, 0.3f));
        yield return new WaitForSeconds(0.1f);
        value2.transform.DOShakePosition(0.3f, new Vector3(0, 0, 0.3f));
        yield return new WaitForSeconds(0.1f);
        value3.transform.DOShakePosition(0.3f, new Vector3(0, 0, 0.3f));
        yield return new WaitForSeconds(0.1f);

        value1.DOFade(0, 0.4f);
        value2.DOFade(0, 0.4f);
        value3.DOFade(0, 0.4f);
        yield return new WaitForSeconds(0.45f);
        PoolManager.Instance.PushObj("HealthChangeNumText", gameObject);
    }
}
