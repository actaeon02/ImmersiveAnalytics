using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChart3D : MonoBehaviour
{
    public GameObject dataPointPrefab;
    public GameObject lineRendererPrefab;

    public string inputfile;

    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;

    // Full column namess
    public string xName;
    public string yName;
    public string zName;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;

    private List<Vector3> dataPoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("Length: " + pointList.Count);

        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];

        for (var i = 0; i < 10; i++)
        {
            Debug.Log("X: " + pointList[i][xName] + " Y: " + pointList[i][yName] + " Z: " + pointList[i][zName]);

            //Debug.Log("Type X: " + pointList[i][xName]);
            int x = (int)pointList[i][xName];

            //Debug.Log("Type Y: " + pointList[i][yName].GetType());
            string y = pointList[i][yName].ToString();

            //Debug.Log("Type Z: " + pointList[i][zName].GetType());
            int z = (int)pointList[i][zName];

            // Convert the string time value to a numerical format (e.g., float)
            Debug.Log("Here " + y);
            int time = ConvertTimeStringToNumerical(y);
            Debug.Log("Time: " + time);

            dataPoints.Add(new Vector3(x, z, time));

            GameObject dataPoint = Instantiate(dataPointPrefab, new Vector3(x, z, time), Quaternion.identity);
            dataPoint.GetComponentInChildren<TextMesh>().text = z.ToString();
        }

        // Instantiate line renderer prefab
        GameObject lineRenderer = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity);
        LineRenderer lr = lineRenderer.GetComponent<LineRenderer>();
        lr.positionCount = dataPoints.Count;
        lr.SetPositions(dataPoints.ToArray());

        Debug.Log("Done!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Convert string time value to numerical format (e.g., float)
    private int ConvertTimeStringToNumerical(string timeString)
    {
        DateTime dateTime = DateTime.ParseExact(timeString, "M/d/yyyy h:mm:ss tt", null);
        int numericalTime = (int)dateTime.TimeOfDay.TotalMinutes; // Convert to total minutes, for example
        return numericalTime;
    }
}
