using UnityEngine;
using System.IO;
using System;

public class ScriptLineCounter : MonoBehaviour
{
    public string folderPath; // Path to the folder containing your scripts

    private void Start()
    {
        int totalLines = CountLinesInFolder(folderPath);
        Debug.Log("Total lines of code in folder: " + totalLines);
    }

    private int CountLinesInFolder(string path)
    {
        int totalLines = 0;

        // Check if the directory exists
        if (Directory.Exists(path))
        {
            Debug.Log("Counting");
            string[] files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    int linesInFile = File.ReadAllLines(file).Length;
                    totalLines += linesInFile;
                }
            }
        }
        else
        {
            Debug.LogError("Folder not found: " + path);
        }

        return totalLines;
    }
}
