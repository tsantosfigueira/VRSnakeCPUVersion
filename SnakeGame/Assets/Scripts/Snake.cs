using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{

    // Tail Prefab
    public GameObject tailPrefab;
    public SpawnFood spawnFood;

    enum currentDirection
    {
        left,
        right,
        up,
        down
    }

    Vector2 dir = Vector2.right;
    List<Transform> tail = new List<Transform>();
    bool ate = false;
    currentDirection snakeDirection;
    int currentSnakeX;
    int currentSnakeY;

    void Start()
    {
        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.3f, 0.3f);
        snakeDirection = currentDirection.right;
    }

    int currentDistanceToFood(int x, int y)
    {
        return Mathf.Abs(spawnFood.foodX - x) + Mathf.Abs(spawnFood.foodY - y);
    }

    void Update()
    {
        currentSnakeX = (int)transform.position.x;
        currentSnakeY = (int)transform.position.y;

   /*     Debug.DrawLine(new Vector2(currentSnakeX, currentSnakeY), new Vector2(currentSnakeX + 1, currentSnakeY), Color.red);
        Debug.DrawLine(new Vector2(currentSnakeX, currentSnakeY), new Vector2(currentSnakeX, currentSnakeY + 1), Color.red);
        Debug.DrawLine(new Vector2(currentSnakeX, currentSnakeY), new Vector2(currentSnakeX, currentSnakeY - 1), Color.red); */

        switch (snakeDirection)
        {
            case currentDirection.right:
                // Move right
                if (currentDistanceToFood(currentSnakeX + 1, currentSnakeY) < currentDistanceToFood(currentSnakeX, currentSnakeY + 1) &&
                   currentDistanceToFood(currentSnakeX + 1, currentSnakeY) < currentDistanceToFood(currentSnakeX, currentSnakeY - 1))
                {
                    dir = Vector2.right;
                    snakeDirection = currentDirection.right;
                }

                // Move Down
                else if (currentDistanceToFood(currentSnakeX, currentSnakeY - 1) < currentDistanceToFood(currentSnakeX + 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX, currentSnakeY - 1) < currentDistanceToFood(currentSnakeX, currentSnakeY + 1))
                {
                    dir = -Vector2.up;
                    snakeDirection = currentDirection.down;
                }

                // Move Up
                else if (currentDistanceToFood(currentSnakeX, currentSnakeY + 1) < currentDistanceToFood(currentSnakeX + 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX, currentSnakeY + 1) < currentDistanceToFood(currentSnakeX, currentSnakeY - 1))
                {
                    dir = Vector2.up;
                    snakeDirection = currentDirection.up;
                }
                break;

            case currentDirection.left:
                // Move left
                if (currentDistanceToFood(currentSnakeX - 1, currentSnakeY) < currentDistanceToFood(currentSnakeX, currentSnakeY + 1) &&
                   currentDistanceToFood(currentSnakeX - 1, currentSnakeY) < currentDistanceToFood(currentSnakeX, currentSnakeY - 1))
                {
                    dir = Vector2.left;
                    snakeDirection = currentDirection.left;
                }

                // Move Up
                else if (currentDistanceToFood(currentSnakeX, currentSnakeY + 1) < currentDistanceToFood(currentSnakeX - 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX, currentSnakeY + 1) < currentDistanceToFood(currentSnakeX, currentSnakeY - 1))
                {
                    dir = Vector2.up;
                    snakeDirection = currentDirection.up;
                }

                // Move down
                else if (currentDistanceToFood(currentSnakeX, currentSnakeY - 1) < currentDistanceToFood(currentSnakeX - 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX, currentSnakeY - 1) < currentDistanceToFood(currentSnakeX, currentSnakeY + 1))
                {
                    dir = -Vector2.up;
                    snakeDirection = currentDirection.down;
                }
                break;

            case currentDirection.up:
                // Move left
                if (currentDistanceToFood(currentSnakeX - 1, currentSnakeY) < currentDistanceToFood(currentSnakeX, currentSnakeY + 1) &&
                   currentDistanceToFood(currentSnakeX - 1, currentSnakeY) < currentDistanceToFood(currentSnakeX + 1, currentSnakeY))
                {
                    dir = Vector2.left;
                    snakeDirection = currentDirection.left;
                }

                // Move Up
                else if (currentDistanceToFood(currentSnakeX, currentSnakeY + 1) < currentDistanceToFood(currentSnakeX + 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX, currentSnakeY + 1) < currentDistanceToFood(currentSnakeX - 1, currentSnakeY))
                {
                    dir = Vector2.up;
                    snakeDirection = currentDirection.up;
                }

                // Move right
                else if (currentDistanceToFood(currentSnakeX + 1, currentSnakeY) < currentDistanceToFood(currentSnakeX - 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX + 1, currentSnakeY) < currentDistanceToFood(currentSnakeX, currentSnakeY + 1))
                {
                    dir = Vector2.right;
                    snakeDirection = currentDirection.right;
                }
                break;

            case currentDirection.down:
                // Move left
                if (currentDistanceToFood(currentSnakeX - 1, currentSnakeY) < currentDistanceToFood(currentSnakeX + 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX - 1, currentSnakeY) < currentDistanceToFood(currentSnakeX, currentSnakeY - 1))
                {
                    dir = Vector2.left;
                    snakeDirection = currentDirection.left;
                }

                // Move Down
                else if (currentDistanceToFood(currentSnakeX, currentSnakeY - 1) < currentDistanceToFood(currentSnakeX + 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX, currentSnakeY - 1) < currentDistanceToFood(currentSnakeX - 1, currentSnakeY))
                {
                    dir = -Vector2.up;
                    snakeDirection = currentDirection.down;
                }

                // Move right
                else if (currentDistanceToFood(currentSnakeX + 1, currentSnakeY) < currentDistanceToFood(currentSnakeX - 1, currentSnakeY) &&
                   currentDistanceToFood(currentSnakeX + 1, currentSnakeY) < currentDistanceToFood(currentSnakeX, currentSnakeY - 1))
                {
                    dir = Vector2.right;
                    snakeDirection = currentDirection.right;
                }
                break;
        }
    }

    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("FoodPrefab"))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);

            spawnFood.Spawn();
        }
        // Collided with Tail or Border
        else
        {
            // ToDo 'You lose' screen
        }
    }


}
