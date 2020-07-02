using UnityEngine;


public enum PieceType
{

    none = -1,
    jump = 0,
    slide = 1,
    car = 2,
    carReversed = 3,
    van = 4,
    vanReversed = 5,
    bus = 6,
    busReversed = 7,
    truck = 8,
    truckReversed = 9,

}


public class Piece : MonoBehaviour
{


    public PieceType pieceType;
    public int visualIndex;


}
