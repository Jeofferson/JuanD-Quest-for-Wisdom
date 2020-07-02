using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{


    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    public List<Piece> cars = new List<Piece>();
    public List<Piece> carsReversed = new List<Piece>();
    public List<Piece> vans = new List<Piece>();
    public List<Piece> vansReversed = new List<Piece>();
    public List<Piece> buses = new List<Piece>();
    public List<Piece> busesReversed = new List<Piece>();
    public List<Piece> trucks = new List<Piece>();
    public List<Piece> trucksReversed = new List<Piece>();

    public List<Piece> pieces = new List<Piece>();


    public Piece GetPiece(PieceType pieceType, int visualIndex)
    {

        Piece piece = pieces.Find(x => !x.gameObject.activeSelf && x.pieceType == pieceType && x.visualIndex == visualIndex);

        if (piece == null)
        {

            GameObject pieceTemporary = null;

            switch (pieceType)
            {

                case PieceType.jump:
                    pieceTemporary = jumps[visualIndex].gameObject;
                    break;

                case PieceType.slide:
                    pieceTemporary = slides[visualIndex].gameObject;
                    break;

                case PieceType.car:
                    pieceTemporary = cars[visualIndex].gameObject;
                    break;

                case PieceType.carReversed:
                    pieceTemporary = carsReversed[visualIndex].gameObject;
                    break;

                case PieceType.van:
                    pieceTemporary = vans[visualIndex].gameObject;
                    break;

                case PieceType.vanReversed:
                    pieceTemporary = vansReversed[visualIndex].gameObject;
                    break;

                case PieceType.bus:
                    pieceTemporary = buses[visualIndex].gameObject;
                    break;

                case PieceType.busReversed:
                    pieceTemporary = busesReversed[visualIndex].gameObject;
                    break;

                case PieceType.truck:
                    pieceTemporary = trucks[visualIndex].gameObject;
                    break;

                case PieceType.truckReversed:
                    pieceTemporary = trucksReversed[visualIndex].gameObject;
                    break;

            }

            pieceTemporary = Instantiate(pieceTemporary);
            piece = pieceTemporary.GetComponent<Piece>();

            pieces.Add(piece);

        }

        return piece;

    }


    public static LevelManager Instance { set; get; }


}
