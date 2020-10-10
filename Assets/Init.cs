﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class CSVParsing : MonoBehaviour {
    public TextAsset csvFile; // Reference of CSV file
    public InputField rollNoInputField;// Reference of rollno input field
    public InputField nameInputField; // Reference of name input filed
    public Text contentArea; // Reference of contentArea where records are displayed

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter

    void Start() {
        readData();
    }
    // Read data from CSV file
    private void readData() {
        string[] records = csvFile.text.Split(lineSeperater);
        foreach (string record in records) {
            string[] fields = record.Split(fieldSeperator);
            foreach (string field in fields) {
                contentArea.text += field + "\t";
            }
            contentArea.text += '\n';
        }
    }
    // Get path for given CSV file
    private static string getPath() {
    #if UNITY_EDITOR
        return Application.dataPath;
    #elif UNITY_ANDROID
        return Application.persistentDataPath;// +fileName;
    #else
        return Application.dataPath + "/" + "countryColors.csv";// +"/"+ fileName;
    #endif
    }
}
