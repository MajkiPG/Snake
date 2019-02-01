using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// <summary>
// Game controller class.
// Fields and methods necesary for gameplay.
// </summary>
public class GameControler : MonoBehaviour
{
    enum Direction { up, right, down, left}      //Enum with all possible snake directions

    public Snake snake;                          //snake instance
   // private GameObject food;                     //food instance
    //private GameObject superFood;                //super food instance

    public Text scoreText;                       //GUI score text

    [Range(0.1f, 1f)]
    public float snakeSpeed;                     //Time between snake movements. Slower time = faster snake

    private int snakeDirection;                  //stores actual snake direction
    private int snakePreviousDirection;          //stores previous snake direction
    private int score;                           //stores actual game score

    public Queue<int> movements;                 //queue with moves to execute

    // <summary>
    // Adds collision action to segment class.
    // </summary>
    private void OnEnable()
    {
        Segment.collision += Collision;
    }

    // <summary>
    // Substracts collision action from segment class.
    // </summary>
    private void OnDisable()
    {
        Segment.collision -= Collision;
    }

    // <summary>
    // Initializes GameScene.
    // Adds all game objects and starts timers.
    // </summary>
    private void Start ()
    {
        movements = new Queue<int>();
        InvokeRepeating("SnakeMovementTimer", 0, snakeSpeed);

        AddFood();
        Invoke("AddSuperFood", (float)Random.Range(15, 25));
    }
	
    // <summary>
    // Method called at each game frame.
    // Executes methods responsible for input control.
    // </summary>
	private void Update ()
    {
        CheckKeyboardInput();
	}

    // <summary>
    // Method executed every x seconds repeatedly.
    // Checks in movements queue if direction has changed.
    // Calls move method.
    // </summary>
    private void SnakeMovementTimer()
    {
        snakePreviousDirection = snakeDirection;

        if (movements.Count != 0)
        {
            snakeDirection = movements.Dequeue();
        }

        snake.Move(snakeDirection, snakePreviousDirection);
    }

    // <summary>
    // Checks input from WSAD keys on keyboard.
    // Checks space in movements queue and adds new direction command if can.
    // </summary>
    private void CheckKeyboardInput()
    {
        if (movements.Count < 2)
        {
            if (Input.GetKeyDown(KeyCode.W) && (snakeDirection != (int)Direction.down || movements.Count == 1))
            {
                movements.Enqueue((int)Direction.up);
            }

            if (Input.GetKeyDown(KeyCode.S) && (snakeDirection != (int)Direction.up || movements.Count == 1))
            {
                movements.Enqueue((int)Direction.down);
            }

            if (Input.GetKeyDown(KeyCode.D) && (snakeDirection != (int)Direction.left || movements.Count == 1))
            {
                movements.Enqueue((int)Direction.right);
            }

            if (Input.GetKeyDown(KeyCode.A) && (snakeDirection != (int)Direction.right || movements.Count == 1))
            {
                movements.Enqueue((int)Direction.left);
            }
        }
    }

    // <summary>
    // Adds food to game board.
    // Generates random position that is free within board.
    // Instantiates food on board.
    // </summary>
    private void AddFood()
    {
        Instantiate(Globals.foodPrefab, GenerateFoodPosition(), Quaternion.identity);
    }

    // <summary>
    // Adds super food to game board.
    // Generates random position that is free within board.
    // Instantiates super food on board.
    // Sets timer for next AddSuperFood event.
    // </summary>
    private void AddSuperFood()
    {
        Instantiate(Globals.superFoodPrefab, GenerateFoodPosition(), Quaternion.identity);

        int timeToNextSuperFood = Random.Range(15, 25) + 10;
        Invoke("AddSuperFood", timeToNextSuperFood);

    }

    // <summary>
    // Handles snakes collision with objects on board.
    // Matches collision action with collided object tag.
    // </summary>
    private void Collision(string obstacle)
    {
        if (obstacle == "Food")
        {
            snake.Grow();
            score++;
            AddFood();
        }

        if (obstacle == "SuperFood")
        {
            snake.Grow();
            score += 10;
        }

        if (obstacle == "Snake" || obstacle == "Border")
        {
            GameOver();
        }

        scoreText.text = score.ToString();
    }

    // <summary>
    // End of game procedure.
    // Stops all working timers, saves last game score and loads GameOver scene.
    // </summary>
    private void GameOver()
    {
        PlayerPrefs.SetInt("LastScore", score);
        CancelInvoke("SnakeMovementTimer");
        CancelInvoke("AddSuperFood");
        SceneManager.LoadScene(2);
    }

    // <summary>
    // Returns random position on game board that is not colliding with snake.
    // </summary>
    private Vector2 GenerateFoodPosition()
    {
        Vector2 foodPosition;

        do
        {
            foodPosition = new Vector2(Random.Range(-5, 4) + 0.5f, Random.Range(-9, 5) + 0.5f);
        } while (snake.ISColliding(foodPosition));

        return foodPosition;
    }





    // <summary>
    // Checks input from touch panel on screen.
    // </summary>
    public void CheckTouchPanelInput(int touchButtonID)
    {
        int newDirection;

        if (movements.Count == 0)
        {
            newDirection = snakeDirection + touchButtonID;
        }
        else
        {
            newDirection = movements.Peek() + touchButtonID;
        }

        if (newDirection == 4)
        {
            newDirection = 0;
        }

        if (newDirection == -1)
        {
            newDirection = 3;
        }

        movements.Enqueue(newDirection);
    }

}
