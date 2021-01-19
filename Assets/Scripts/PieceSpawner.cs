using UnityEngine;

public class PieceSpawner : MonoBehaviour
{

    public PieceType pieceType;
    private Piece currentPiece;

    public void Spawn()
    {
        int numPieces = 0;

        switch (pieceType)
        {
            case PieceType.jump:
                numPieces = LevelManager.Instance.jumps.Count;
                break;

            case PieceType.slide:
                numPieces = LevelManager.Instance.slides.Count;
                break;

            case PieceType.car:
                numPieces = LevelManager.Instance.cars.Count;
                break;

            case PieceType.carReversed:
                numPieces = LevelManager.Instance.carsReversed.Count;
                break;

            case PieceType.van:
                numPieces = LevelManager.Instance.vans.Count;
                break;

            case PieceType.vanReversed:
                numPieces = LevelManager.Instance.vansReversed.Count;
                break;

            case PieceType.bus:
                numPieces = LevelManager.Instance.buses.Count;
                break;

            case PieceType.busReversed:
                numPieces = LevelManager.Instance.busesReversed.Count;
                break;

            case PieceType.truck:
                numPieces = LevelManager.Instance.trucks.Count;
                break;

            case PieceType.truckReversed:
                numPieces = LevelManager.Instance.trucksReversed.Count;
                break;
        }

        currentPiece = LevelManager.Instance.GetPiece(pieceType, Random.Range(0, numPieces));

        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }

}
