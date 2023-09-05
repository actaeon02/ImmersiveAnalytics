using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataPlotter : MonoBehaviour
{
    // Name of the input file, no extension
    public string inputfile;
    
    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;

    // Indices for columns to be assigned
    public int columnX = 1;
    public int columnY = 2;
    public int columnZ = 3;

    // Full column namess
    public string xName;
    public string yName;
    public string zName;

    // The prefab for the data points to be instantiated
    public GameObject PointPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject PointHolder;

    public float plotScale = 1;
    
    // Use this for initialization, Start is called before the first frame update
    void Start () {
    
        // Set pointlist to results of function Reader with argument inputfile
        pointList = CSVReader.Read(inputfile);
        
        //Log to console
        Debug.Log(pointList);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(pointList[1].Keys);
    
        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in CSV");

        foreach (string key in columnList)
            Debug.Log("Column name is " + key);

        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];
        
        // Get maxes of each axis
        float xMax = FindMaxValue(xName);
        float yMax = FindMaxValue(yName);
        float zMax = FindMaxValue(zName);
        
        // Get minimums of each axis
        float xMin = FindMinValue(xName);
        float yMin = FindMinValue(yName);
        float zMin = FindMinValue(zName);

        //Loop through Pointlist
        for (var i = 0; i < pointList.Count; i++)
        {            
            // Get value in poinList at ith "row", in "column" Name, normalize
            float x = (Convert.ToSingle(pointList[i][xName]) - xMin) / (xMax - xMin);
            
            float y = (Convert.ToSingle(pointList[i][yName]) - yMin) / (yMax - yMin);
            
            float z = (Convert.ToSingle(pointList[i][zName]) - zMin) / (zMax - zMin);

            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
                PointPrefab, 
                new Vector3(x, y, z) * plotScale, 
                Quaternion.identity);

            // Make dataPoint child of PointHolder object 
            dataPoint.transform.parent = PointHolder.transform;

            // Assigns original values to dataPointName
            string dataPointName = pointList[i][xName] + " " + pointList[i][yName] + " " + pointList[i][zName];

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;

            // Gets material color and sets it to a new RGBA color we define
            dataPoint.GetComponent<Renderer>().material.color = new Color(x,y,z, 1.0f);
        }
    }

    private float FindMaxValue(string columnName)
    {
        // Set initial value to first value
        float maxValue = Convert.ToSingle(pointList[0][columnName]);
    
        // Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < pointList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(pointList[i][columnName]))
                maxValue = Convert.ToSingle(pointList[i][columnName]);
        }
    
        //Spit out the max value
        return maxValue;
    }

    private float FindMinValue(string columnName)
    {
    
        float minValue = Convert.ToSingle(pointList[0][columnName]);
    
        // Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < pointList.Count; i++)
        {
            if (Convert.ToSingle(pointList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(pointList[i][columnName]);
        }
    
        return minValue;
    }
}
