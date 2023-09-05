using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LineChart : MonoBehaviour
{
    public string inputfile;
    public Transform dataPointPrefab;
    public Transform dataLinePrefab;

    // Indices for columns to be assigned
    public int columnX = 1;
    public int columnY = 2;
    public int columnZ = 3;

    // Full column namess
    public string xName;
    public string yName;
    public string zName;

    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;

    private List<Vector3> dataPoints = new List<Vector3>();
    private List<string> labels = new List<string>(); // List to store labels

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

        for (var i = 0; i < 100; i++)
        {
            Debug.Log("X: " + pointList[i][xName] + " Y: " + pointList[i][yName] + " Z: " + pointList[i][zName]);
            float x = Convert.ToSingle(pointList[i][xName]);
            string y = Convert.ToString(pointList[i][yName]);
            float z = Convert.ToSingle(pointList[i][zName]);

            Debug.Log(i);
            dataPoints.Add(new Vector3(x, z, 0));
            labels.Add(y); // Store time as a label

            // Create a data point GameObject at the calculated position
            Transform dataPoint = Instantiate(dataPointPrefab, new Vector3(x, z, 0), Quaternion.identity);

            // Set the label as TextMesh text
            TextMesh labelMesh = dataPoint.GetComponentInChildren<TextMesh>();
            if (labelMesh != null) // Ensure TextMesh component is present
            {
                labelMesh.text = y;
            }
        }

        // Create a line renderer GameObject and set its positions
        Transform lineRenderer = Instantiate(dataLinePrefab, Vector3.zero, Quaternion.identity);
        LineRenderer lr = lineRenderer.GetComponent<LineRenderer>();
        lr.positionCount = dataPoints.Count();
        lr.SetPositions(dataPoints.ToArray());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
