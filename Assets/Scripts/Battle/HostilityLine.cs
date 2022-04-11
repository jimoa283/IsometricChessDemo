using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostilityLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float centerPoint = 0.1f;

    private int _segmentNum = 20;

    /* void Start()
     {
         if (!lineRenderer)
         {
             lineRenderer = GetComponent<LineRenderer>();
         }
         //lineRenderer.sortingLayerID = layerOrder;
         //调用画贝斯尔线
         GetBeizerList(controlPoints[0].position, (controlPoints[0].position + controlPoints[1].position) * 0.5f + new Vector3(0, centerPoint, 0), controlPoints[1].transform.position, _segmentNum);
     }*/

    public void Init()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = _segmentNum + 1;
    }

    public void SetLine(Vector3 startPos,Vector3 endPos)
    {
        gameObject.SetActive(true);
        GetBeizerList(startPos, (startPos + endPos) * 0.5f + new Vector3(0, centerPoint, 0), endPos, _segmentNum);
    }

    public void ClearLine()
    {
        gameObject.SetActive(false);
        //lineRenderer.positionCount = 0;
    }

    /// <summary>
    /// 根据T值，计算贝塞尔曲线上面相对应的点
    /// </summary>
    /// <param name="t"></param>T值
    /// <param name="p0"></param>起始点
    /// <param name="p1"></param>控制点
    /// <param name="p2"></param>目标点
    /// <returns></returns>根据T值计算出来的贝赛尔曲线点
    private static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        p += new Vector3(0, 0, -0.01f);
        return p;
    }

    /// <summary>
    /// 获取存储贝塞尔曲线点的数组
    /// </summary>
    /// <param name="startPoint"></param>起始点
    /// <param name="controlPoint"></param>控制点
    /// <param name="endPoint"></param>目标点
    /// <param name="segmentNum"></param>采样点的数量
    /// <returns></returns>存储贝塞尔曲线点的数组
    public void GetBeizerList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
    {
        for (int i = 0; i <=segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = CalculateCubicBezierPoint(t, startPoint,
                controlPoint, endPoint);
            lineRenderer.SetPosition(i, pixel);
        }

    }

}
