using UnityEngine;

public class PieceSpawner : MonoBehaviour
{


    public PieceType pieceType;

    private Piece currentPiece;


    public void Spawn()
    {

        currentPiece = LevelManager.Instance.GetPiece(pieceType, 0);

        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);

    }


    public void Despawn()
    {

        currentPiece.gameObject.SetActive(false);

    }


}
