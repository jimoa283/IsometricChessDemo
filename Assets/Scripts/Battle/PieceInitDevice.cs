using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceInitDevice : MonoBehaviour
{
    public Piece[] pieces;
    public Cell[] cells;

    public void PieceInit(UnityAction action)
    {
        StartCoroutine(DoPieceInit(action));
    }

    IEnumerator DoPieceInit(UnityAction action)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            CameraController.Instance.MoveMainCamera(cells[i].transform.position + Vector3.up * 0.6f);
            yield return new WaitForSeconds(0.2f);
            Piece piece = Instantiate(pieces[i].gameObject).GetComponent<Piece>();
            piece.Init();
            piece.SetBattlePos(cells[i]);
            piece.gameObject.SetActive(false);
            GameObject burstInVFX = PoolManager.Instance.GetObj("BurstInVFX");
            burstInVFX.transform.position = piece.transform.position;
            SortingLayerSlover.GetSortingLayerName(burstInVFX.GetComponent<SpriteRenderer>(), piece.floorIndex,1);
            yield return new WaitForSeconds(0.1f);
            piece.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            PoolManager.Instance.PushObj("BurstInVFX", burstInVFX);
        }

        PieceQueueManager.Instance.SetPieceActionQueueByDie();
        yield return new WaitForSeconds(0.5f);
        //TimeLineManager.Instance.ResumeTimeLine();
        action?.Invoke();
    }
}
