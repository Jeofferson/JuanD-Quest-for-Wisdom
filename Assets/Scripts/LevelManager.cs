using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{


    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    public List<Piece> ramps = new List<Piece>();
    public List<Piece> shortBlocks = new List<Piece>();

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

                case PieceType.ramp:
                    pieceTemporary = ramps[visualIndex].gameObject;
                    break;

                case PieceType.shortBlock:
                    pieceTemporary = shortBlocks[visualIndex].gameObject;
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
