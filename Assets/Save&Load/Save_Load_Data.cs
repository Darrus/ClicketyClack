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


        //// convert string to stream
        //byte[] byteArray = Encoding.UTF8.GetBytes(path);
        ////byte[] byteArray = Encoding.ASCII.GetBytes(contents);
        //MemoryStream stream = new MemoryStream(byteArray);
        //// convert stream to string
        //StreamReader reader = new StreamReader(stream);
        //string response = reader.ReadToEnd();


        FileStream stream = File.OpenRead(path);
        String response = "";
       

        byte[] b = new byte[stream.Length];
        UTF8Encoding temp = new UTF8Encoding(true);

        while (stream.Read(b, 0, b.Length) > 0)
        {
            response += temp.GetString(b);
        }
        Debug.Log(response);

        stream.Dispose();

        return response;
    }

    public static void Save(String content)
    {
        FileStream stream = File.Create(path);
        byte[] contentBytes = new UTF8Encoding(true).GetBytes(content);
        stream.Write(contentBytes, 0, contentBytes.Length);
        stream.Dispose();
    }
}
