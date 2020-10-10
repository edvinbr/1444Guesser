using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Versioning;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class CSVParser : MonoBehaviour {
    public TextAsset csvFile; // Reference of CSV file
    public InputField rollNoInputField;// Reference of rollno input field
    public InputField nameInputField; // Reference of name input filed
    public List<List<string>> countryColors = new List<List<string>>(); // Reference of contentArea where records are displayed
    //public List<List<string>> pixelCountries = new List<List<string>>();
    public List<List<string>> countryPixels = new List<List<string>>();
    public List<int[]> countryPixelsOnly;
    public List<(int, int, int)> colorsOnly;
    public List<string> countriesOnly;

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ';'; // It defines field seperate chracter

    void Awake() {
        csvFile = Resources.Load<TextAsset>("countryColors");
        readData(countryColors);
        //csvFile = Resources.Load<TextAsset>("pixelCountries");
        //readData(pixelCountries);
        //UnityEngine.Debug.Log(pixelCountries[0][0]);
        csvFile = Resources.Load<TextAsset>("countryPixels");
        readData(countryPixels);
        countryPixelsOnly = new List<int[]>();

        for (int i = 0; i < countryPixels.Count(); i++) {
            if (i == 167 || i == 381) { //167 = land, 381 = water
                countryPixelsOnly.Add(new int[1]);
                continue;
            }
            countryPixelsOnly.Add(parseNumbers(countryPixels[i][2]));
        }

        colorsOnly = new List<(int, int, int)>();
        countriesOnly = new List<string>();

        for (int i = 0; i < countryColors.Count(); i++) {
            countriesOnly.Add(countryColors[i][1]);
            int[] numbers = parseNumbers(countryColors[i][2]);
            colorsOnly.Add((numbers[0], numbers[1], numbers[2]));
        }

    }

    void Start() {
        
    }
    // Read data from CSV file
    private void readData(List<List<string>> output) {
        string[] records = csvFile.text.Split(lineSeperater);
        foreach (string record in records) {
            string[] fields = record.Split(fieldSeperator);
            List<string> tempList = new List<string>();
            foreach (string field in fields) {
                tempList.Add(field);
            }
            output.Add(tempList);
        }
    }

    private int[] parseNumbers(string input) {
        // Split on one or more non-digit characters.
        string[] stringnumbers = Regex.Split(input, @"\D+");
        int[] numbers = new int[stringnumbers.Count()];
        int idx = 0;
        foreach (string value in stringnumbers) {
            if (!string.IsNullOrEmpty(value)) {
                int i = int.Parse(value);
                numbers[idx] = i;
                idx++;
                //Console.WriteLine("Number: {0}", i);
            }
        }
        return numbers;
    }

    // Get path for given CSV file
    private static string getPath() {
#if UNITY_EDITOR
        return Application.dataPath + "/" + "countryColors.csv";;
#elif UNITY_ANDROID
        return Application.persistentDataPath;// +fileName;
#else
        return Application.dataPath + "/" + "countryColors.csv";// +"/"+ fileName;
#endif
    }
}
