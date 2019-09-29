using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    // Dimesion of the grid
    public static int X_DIMENSION = 100;
    public static int Z_DIMENSION = 100;

    // Distance to reach square's center
    public static float offset = 0.5f;

    // Grid as List of List of Nodes
    public List<List<Node>> grid = new List<List<Node>>(X_DIMENSION);

    private void Start()
    {
        // Initialise every square in the grid as Grass
        for (int x = 0; x < X_DIMENSION; x++)
        {
            grid.Add(new List<Node>());
            for (int z = 0; z < Z_DIMENSION; z++)
            {
                grid[x].Add(new Node(new Vector3(x, 0, z), Node.TerrainType.Grass));

                // Coste por Rol
                AgentNPC agent = gameObject.GetComponent<AgentNPC>();
                if (agent != null)
                    grid[x][z].Rol = agent.TipoR;

            }
        }

        generateGrid();

    }


    private void Update()
    {
       
    }



    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x);
        int yCount = Mathf.RoundToInt(position.y);
        int zCount = Mathf.RoundToInt(position.z);

        Vector3 result = new Vector3(
            (float)xCount + offset,
            (float)yCount + offset,
            (float)zCount + offset);

        result += transform.position;


        if (result.x >= X_DIMENSION -1) result.x = X_DIMENSION - 1;
        if (result.x <= 0) result.x = 0;
        if (result.z >= Z_DIMENSION -1) result.z = Z_DIMENSION - 1;
        if (result.z <= 0) result.z = 0;


        


        return result;
    }    

    private void generateGrid()
    {
        int sizeBigSquare = 10;
        int displacementX = sizeBigSquare;
        int displacementZ = sizeBigSquare*7;



        //Base
        for (int x = displacementX; x < sizeBigSquare*2  + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare*2 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.BaseA);


        displacementX = sizeBigSquare * 7;
        displacementZ = sizeBigSquare;
        for (int x = displacementX; x < sizeBigSquare * 2 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare * 2 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.BaseB);



        //Healing
        displacementX = sizeBigSquare * 9;
        displacementZ = sizeBigSquare * 4;
        for (int x = displacementX; x < sizeBigSquare + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare * 2 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.HealingA);


        displacementX = 0;
        displacementZ = sizeBigSquare * 4;
        for (int x = displacementX; x < sizeBigSquare + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare * 2 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.HealingB);




        // Water
        displacementX = sizeBigSquare;
        displacementZ = sizeBigSquare;
        int i = 0;
        while (i < 15)
        {
            if (i != 4 && i != 5 && i != 8 && i != 9)
                for (int x = displacementX; x < sizeBigSquare + displacementX; x++)
                    for (int z = displacementZ; z < sizeBigSquare / 2 + displacementZ; z++)
                        grid[x][z].setType(Node.TerrainType.Water);

            displacementX += sizeBigSquare / 2;
            displacementZ += sizeBigSquare / 2;
            i++;
        }



        //Pathway
        displacementX = 0;
        displacementZ = 0;
        for (int x = displacementX; x < sizeBigSquare/2 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare*3 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Pathway);

        displacementX = 0;
        displacementZ = 0;
        for (int x = displacementX; x < sizeBigSquare* 5 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare /2 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Pathway);



        displacementX = sizeBigSquare*9 + sizeBigSquare/2;
        displacementZ = sizeBigSquare*7;
        for (int x = displacementX; x < sizeBigSquare / 2 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare * 3 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Pathway);

        displacementX =  sizeBigSquare*5;
        displacementZ = sizeBigSquare * 9 + sizeBigSquare / 2;
        for (int x = displacementX; x < sizeBigSquare * 4 + sizeBigSquare/2 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 2 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Pathway);






        //Stone
        displacementX = sizeBigSquare*6;
        displacementZ = sizeBigSquare*8;
        for (int x = displacementX; x < sizeBigSquare/4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare/4 +displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);



        displacementX = sizeBigSquare * 6;
        displacementZ = sizeBigSquare * 8;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);




        displacementX = sizeBigSquare * 2;
        displacementZ = sizeBigSquare * 3;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);



        displacementX = sizeBigSquare * 7;
        displacementZ = sizeBigSquare * 9;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);


        displacementX = sizeBigSquare * 4;
        displacementZ = sizeBigSquare * 6;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);



        displacementX = sizeBigSquare * 6;
        displacementZ = sizeBigSquare * 4;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);


        displacementX = sizeBigSquare * 9;
        displacementZ = sizeBigSquare * 7;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);



        displacementX = sizeBigSquare * 8;
        displacementZ = sizeBigSquare * 6;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);

        displacementX = sizeBigSquare * 3;
        displacementZ = sizeBigSquare * 2;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);


        displacementX = sizeBigSquare * 8;
        displacementZ = sizeBigSquare * 3;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);


        displacementX = sizeBigSquare * 5;
        displacementZ = sizeBigSquare * 2;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);


        displacementX = sizeBigSquare * 1;
        displacementZ = sizeBigSquare * 4;
        for (int x = displacementX; x < sizeBigSquare / 4 + displacementX; x++)
            for (int z = displacementZ; z < sizeBigSquare / 4 + displacementZ; z++)
                grid[x][z].setType(Node.TerrainType.Stone);
    
    }


    public List<Vector3> getAdjacentLocations(Node fromNode)
    {
        List<Vector3> adj = new List<Vector3>();

        if (fromNode.location.x - 1 >= 0)
        {
            adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z));

        }

        if (fromNode.location.x + 1 < X_DIMENSION)
        {
            adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z));
        }


        if (fromNode.location.z - 1 >= 0)
            adj.Add(new Vector3(fromNode.location.x, 0, fromNode.location.z - 1));

        if (fromNode.location.z + 1 < Z_DIMENSION)
            adj.Add(new Vector3(fromNode.location.x, 0, fromNode.location.z + 1));




        if (fromNode.location.x == 0)
        {
            if (fromNode.location.z == 0)
            {
                adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z + 1));
            }
            else if (fromNode.location.z == Z_DIMENSION - 1)
            {
                adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z - 1));
            }
            else
            {
                adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z + 1));
                adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z - 1));
            }
        }

        else if (fromNode.location.x == X_DIMENSION - 1)
        {
            if (fromNode.location.z == 0)
            {
                adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z + 1));
            }
            else if (fromNode.location.z == Z_DIMENSION - 1)
            {
                adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z - 1));
            }
            else
            {
                adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z + 1));
                adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z - 1));
            }
        }

        else if (fromNode.location.z == 0)
        {
            adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z + 1));
            adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z + 1));
        }

        else if (fromNode.location.z == Z_DIMENSION - 1)
        {
            adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z - 1));
            adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z - 1));
        }

        else
        {
            adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z + 1));
            adj.Add(new Vector3(fromNode.location.x - 1, 0, fromNode.location.z - 1));

            adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z - 1));
            adj.Add(new Vector3(fromNode.location.x + 1, 0, fromNode.location.z - 1));


        }


        return adj;


    }

    public void resetStateGrid(){
        for (int x = 0; x < X_DIMENSION; x++)
        {
            for (int z = 0; z < Z_DIMENSION; z++)
            {
                grid[x][z].resetState();
            }
        }
    }

    public Node locationToNode(Vector3 location)
    {
        if (location.x >= X_DIMENSION || location.z >= Z_DIMENSION)
            return null;


        return grid[(int)location.x][(int)location.z];

    }

    



}
