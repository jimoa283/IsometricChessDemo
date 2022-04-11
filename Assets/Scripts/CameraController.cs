using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CameraController : MSingleton<CameraController>
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera battleCamera;

    public LayerMask SpecialBattlePieceLayer;
    public LayerMask OriginalLayer;

    public List<Piece> temp = new List<Piece>();
    private Vector3 OriginalPos;

    public bool hasBattleMoved;

    private List<Piece> battlePieces;

    protected override void Init()
    {
        mainCamera = transform.Find("Main Camera").GetComponent<Camera>();
        battleCamera = transform.Find("BattleCamera").GetComponent<Camera>();
        battlePieces = new List<Piece>();
    }

    private void Update()
    {
        /* if(BattleGameManager.Instance.isBattling)
         {
             if (Input.GetMouseButtonDown(0))
             {
                 SpecialBattleShot(temp);
             }

             if (Input.GetMouseButtonDown(1))
             {
                 ExitSpecialBattleShot(temp);
             }
         }*/

    }

    public void MoveMainCamera(Vector3 pos)
    {
        pos = new Vector3(pos.x, pos.y, mainCamera.transform.position.z);
        mainCamera.transform.DOMove(pos, 0.2f, true);
    }

    public void CameraShake()
    {
        if (battleCamera.gameObject.activeInHierarchy)
            battleCamera.DOShakePosition(0.2f, new Vector3(0.3f, 0.3f, 0.3f));
        else
            mainCamera.DOShakePosition(0.2f, new Vector3(0.3f, 0.3f, 0.3f));
    }

    /*private void BattleCameraShake()
    {
        battleCamera.DOShakePosition(0.1f,new Vector3(0.3f,0.3f,0.3f));
    }

    private void MainCameraShake()
    {
        mainCamera.DOShakePosition(0.3f, new Vector3(0.3f, 0.3f, 0.3f));
    }*/

    public void SpecialBattleShot(UnityAction action) //Piece owner, List<Piece> list,
    {
        hasBattleMoved = true;

        float tempX = 0;
        float tempY = 0;
        foreach (var piece in battlePieces)
        {
            piece.gameObject.layer = LayerMask.NameToLayer("BattlePiece");
            tempX += piece.transform.position.x;
            tempY += piece.transform.position.y;
        }
        battleCamera.gameObject.SetActive(true);
        Vector3 temp3 = new Vector3(tempX / (battlePieces.Count + 1), tempY / (battlePieces.Count + 1), -6);
        battleCamera.transform.position = mainCamera.transform.position;
        battleCamera.transform.DOMove(temp3, 0.3f);
        StartCoroutine(DoSpecialBattleShot(action));
    }

    IEnumerator DoSpecialBattleShot(UnityAction action)
    {
        yield return new WaitForSeconds(0.35f);
        action?.Invoke();
    }

    public void ExitSpecialBattleShot(UnityAction action)
    {
        if (hasBattleMoved)
            StartCoroutine(DoExitSpecialBattleShot(action));
        else
            action?.Invoke();
    }

    public void AddBattlePiece(Piece piece)
    {
        //piece.gameObject.layer = LayerMask.NameToLayer("BattlePiece");
        battlePieces.Add(piece);
    }

    public void AddRangeBattlePieces(List<Piece> pieces)
    {
        battlePieces.AddRange(pieces);
    }

    IEnumerator DoExitSpecialBattleShot(UnityAction action)
    {
        battleCamera.transform.DOMove(mainCamera.transform.position, 0.5f);
        yield return new WaitForSeconds(0.7f);
        battleCamera.gameObject.SetActive(false);
        //owner.gameObject.layer = LayerMask.NameToLayer("Piece");
        foreach (var piece in battlePieces)
        {
            if (piece == null)
                continue;
            piece.gameObject.layer = LayerMask.NameToLayer("Piece");
        }
        battlePieces.Clear();
        action?.Invoke();
    }

    public void MapEventSceneChange(Vector3 pos, string sceneName)
    {
        StartCoroutine(DoMapEventSceneChange(pos, sceneName));
    }

    IEnumerator DoMapEventSceneChange(Vector3 pos, string sceneName)
    {
        Vector3 realPos = new Vector3(pos.x, pos.y, mainCamera.transform.position.z);
        mainCamera.transform.DOLocalMove(realPos, 0.5f);
        yield return new WaitForSeconds(0.55f);
        mainCamera.transform.DOMoveZ(-0.3f, 0.3f);
        yield return new WaitForSeconds(0.35f);
        BattleGameManager.Instance.BigSceneChangeEvent(sceneName);
        yield return new WaitForSeconds(0.1f);
        mainCamera.transform.position = new Vector3(0, 0, -8.6f);
    }

}
