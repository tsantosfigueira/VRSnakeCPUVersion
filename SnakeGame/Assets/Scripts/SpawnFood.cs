using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public GameObject foodPrefab;

    // Borders
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;

    [HideInInspector] public int foodX;
    [HideInInspector] public int foodY;

    void Start()
    {
        Spawn();
    }

    // Spawn one piece of food
    public void Spawn()
    {
        // x position between left & right border
        foodX = (int)Random.Range(borderLeft.position.x,
                                  borderRight.position.x);

        // y position between top & bottom border
        foodY = (int)Random.Range(borderBottom.position.y,
                                  borderTop.position.y);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(foodX, foodY),
                    Quaternion.identity); // default rotation
    }
}
