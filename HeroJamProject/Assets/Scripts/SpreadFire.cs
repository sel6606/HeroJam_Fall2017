using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadFire : MonoBehaviour
{
    private float timeSinceSpreadCall;
    private bool neighborsRetrieved;
    private List<ForestCell> neighbors;

    public float secToWait;

    // Use this for initialization
    void Start()
    {
        neighbors = new List<ForestCell>();
        neighborsRetrieved = false;
        timeSinceSpreadCall = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameInfo.instance.ForestGenerated && !neighborsRetrieved)
        {
            GetNeighbors();
        }

        //If this cell is not yet on fire, do spread calculations
        if (!gameObject.GetComponent<ForestCell>().OnFire && timeSinceSpreadCall >= secToWait)
        {
            Spread();
            timeSinceSpreadCall = 0.0f;
        }


        timeSinceSpreadCall += Time.deltaTime;

    }

    /// <summary>
    /// Simulates the spread of fires to each neighbor
    /// </summary>
    private void Spread()
    {
        int neighborsBurning = 0;

        if (neighborsRetrieved)
        {
            //Determine how many neighbors are already burning
            foreach (ForestCell neighbor in neighbors)
            {
                if (neighbor.OnFire)
                {
                    neighborsBurning++;
                }
            }

            //Determine the proportion of neigbors on fire to neighbors not on fire
            float burningProp = 0.0f;
            int neighborsNotBurning = neighbors.Count - neighborsBurning;

            burningProp = (float)neighborsBurning / neighborsNotBurning;

            if (burningProp >= 0.25)
            {
                float rand = Random.Range(0.0f, 0.1f);

                //If the random number is less than the probability, start burning
                if (rand < GameInfo.instance.BurnChance)
                {
                    gameObject.GetComponent<ForestCell>().SetFire();
                }
            }



        }
    }

    /// <summary>
    /// Retrieves all the neighbors of this cell and adds them to the neighbors list
    /// </summary>
    private void GetNeighbors()
    {
        //The coordinates of this cell
        IntVector2 position = gameObject.GetComponent<ForestCell>().coordinates;
        //The forest this cell is in
        ForestGenerator forest = gameObject.GetComponentInParent<ForestGenerator>();

        //Check every direction
        for (int i = 0; i < 4; i++)
        {
            //Calculate the coordinates of each direction
            IntVector2 cellToGet = position + ForestDirections.ToIntVector2((Direction)i);

            //If the coordinates calculated are part of the forest, retrieve that cell and add it to the list
            if (forest.ContainsCoordinates(cellToGet))
            {
                ForestCell temp = forest.GetCell(cellToGet);
                neighbors.Add(temp);
            }

        }

        neighborsRetrieved = true;

    }
}
