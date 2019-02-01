using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Globals
{
    public static GameObject snakePrefab = Resources.Load<GameObject>("Prefabs/SnakeSegment");
    public static GameObject foodPrefab = Resources.Load<GameObject>("Prefabs/Food");
    public static GameObject superFoodPrefab = Resources.Load<GameObject>("Prefabs/SuperFood");

    public static Sprite snakeSegmentSprite = Resources.Load<Sprite>("Sprites/SnakeSegment");
    public static Sprite snakeHeadSprite = Resources.Load<Sprite>("Sprites/SnakeHead");
    public static Sprite snakeSegmentTurnSprite = Resources.Load<Sprite>("Sprites/SnakeSegmentTurn");
    public static Sprite snakeTailSprite = Resources.Load<Sprite>("Sprites/SnakeTail");

}
