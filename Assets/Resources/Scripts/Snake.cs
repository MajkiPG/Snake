using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// <summary>
// Snake class containing snake behaviours and segments.
// </summary>
public class Snake : MonoBehaviour
{
    public int length;                          //Determines actuall snake length.

    private Queue<GameObject> _segmentList;     //Queue list containing all existing snake segments.
    private Queue<int> _segmentTurnList;        //Queue list containing history of all segment turns. Value tells direction after turn.
    private GameObject _head;                   //Pointer to snake head segment.


    // <summary>
    // Initializes new snake, creates lists and adds firs snake segment to board.
    // </summary>
    private void Start()
    {
        _segmentList = new Queue<GameObject>();
        _segmentTurnList = new Queue<int>();

        GameObject newSegment = Instantiate(Globals.snakePrefab, new Vector2(0.5f, 0.5f), Quaternion.identity);
        _segmentList.Enqueue(newSegment);
        _head = newSegment;
    }

    // <summary>
    // Sets new sprite for old head segment.
    // Determines what sprite to use and sets its angle.
    // </summary>
    private void SetSegmentAfterHead(int newDirection, int previousDirection)
    {
        if (previousDirection != newDirection)
        {
            _segmentTurnList.Enqueue(newDirection);

            _head.GetComponent<Segment>().transform.tag = "SnakeTurn";
            _head.GetComponent<Segment>().ChangeSprite(Globals.snakeSegmentTurnSprite);

            if ((previousDirection == 0 && newDirection == 1) || (previousDirection == 3 && newDirection == 2))
            {
                _head.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if ((previousDirection == 2 && newDirection == 1) || (previousDirection == 3 && newDirection == 0))
            {
                _head.transform.rotation = Quaternion.Euler(0, 0, 90);
            }

            if ((previousDirection == 1 && newDirection == 0) || (previousDirection == 2 && newDirection == 3))
            {
                _head.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            if ((previousDirection == 0 && newDirection == 3) || (previousDirection == 1 && newDirection == 2))
            {
                _head.transform.rotation = Quaternion.Euler(0, 0, 270);
            }
        }
        else
        {
            _head.GetComponent<Segment>().ChangeSprite(Globals.snakeSegmentSprite);
        }
    }

    // <summary>
    // Sets tail sprite on last snake segment.
    // </summary>
    private void SetTailSegment()
    {
        if (_segmentList.Peek().tag == "SnakeTurn" && _segmentTurnList.Count != 0)
        {
            _segmentList.Peek().transform.rotation = Quaternion.Euler(0, 0, SegmentRotationAngle(_segmentTurnList.Dequeue()));
        }

        _segmentList.Peek().GetComponent<Segment>().transform.tag = "Snake";
        _segmentList.Peek().GetComponent<Segment>().ChangeSprite(Globals.snakeTailSprite);
    }

    // <summary>
    // Returns segment rotation value determined by snake direction.
    // </summary>
    private float SegmentRotationAngle(int direction)
    {
        switch (direction)
        {
            case 0:
                return 0f;
            case 1:
                return 270f;
            case 2:
                return 180f;
            case 3:
                return 90f;
            default:
                return 0f;
        }
    }






    // <summary>
    // Moves snake in direction from input.
    // Sets new head pointer.
    // Destorys tail segment if snake didnt grow.
    // </summary>
    public void Move(int direction, int previousDirection)
    {
        GameObject newSegment;

        Vector2 headPosition = _head.transform.position;
        Vector2 newHeadPosition = headPosition;

        switch(direction)
        {
            case 0:
                newHeadPosition = new Vector2(headPosition.x, headPosition.y + 1);
                break;
            case 1:
                newHeadPosition = new Vector2(headPosition.x + 1, headPosition.y);
                break;
            case 2:
                newHeadPosition = new Vector2(headPosition.x, headPosition.y - 1);
                break;
            case 3:
                newHeadPosition = new Vector2(headPosition.x - 1, headPosition.y );
                break;
        }

        SetSegmentAfterHead(direction, previousDirection);

        newSegment = (GameObject)Instantiate(Globals.snakePrefab, newHeadPosition, Quaternion.Euler(0, 0, SegmentRotationAngle(direction)));
        _segmentList.Enqueue(newSegment);
        _head = newSegment;

        if (length < _segmentList.Count)
        {
            Destroy(_segmentList.Dequeue());
        }

        SetTailSegment();

    }

    // <summary>
    // Increments snake length.
    // </summary>
    public void Grow()
    {
        length++;
    }

    // <summary>
    // Checks if position on game board is colliding with snake.
    // Returns true if is colliding.
    // </summary>
    public bool ISColliding(Vector2 positionToCheck)
    {
        bool colliding = false;

        foreach (GameObject segment in _segmentList)
        {
            if (segment.transform.position.x == positionToCheck.x && segment.transform.position.y == positionToCheck.y)
            {
                colliding = true;
            }
        }

        return colliding;
    }

}
