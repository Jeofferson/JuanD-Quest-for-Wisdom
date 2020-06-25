using UnityEngine;


public enum PieceType
{
    none = -1,
    jump = 0,
    slide = 1,
    ramp = 2,
    shortBlock = 3,
}


public class Piece : MonoBehaviour
{


    public PieceType pieceType;
    public int visualIndex;


}
