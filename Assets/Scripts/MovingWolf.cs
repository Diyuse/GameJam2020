using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWolf : MonoBehaviour
{
    private int row;
    private int col;
    private int gridSize;
    private float tileSize;
    private bool justFlipped;
    // Start is called before the first frame update
    private void Start()
    {
            row = 2;
            col = 7;
            //gridSize = Grid.gridSize;
            //tileSize = Grid.tileSize;
    }

    // Update is called once per frame
    void Update()
    {
        int random = (int)Random.Range(1, 4);
        if (random == 1)
        {
            transform.Translate(row-1, 0, col);
        }
        else if(random == 2)
        {
            transform.Translate(row + 1, 0, col);
        }
        else if(random == 3)
        {
            transform.Translate(row, 0, col-1);
        }
        else
        {

        }

    }
}
