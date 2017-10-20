using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class Save_Load_Data{

    private static String path = Application.streamingAssetsPath + "/test.json";

    public static bool Check_SaveFile()
    {
        if (!System.IO.File.Exists(path))
        {
            return false;
        }
        else
            return true;
    }

    public static String load()
    {
        if(!System.IO.File.Exists(path))
        {
            return null;
        }

        StreamReader reader = new StreamReader(path);
        String response = "";
        while (!reader.EndOfStream)
        {
            Debug.Log("hi");
            response += reader.ReadLine();
        }

        return response;
    }

    public static void Save(String content)
    {
        FileStream stream = File.Create(path);
        byte[] contentBytes = new UTF8Encoding(true).GetBytes(content);
        stream.Write(contentBytes, 0, contentBytes.Length);
    }
}
