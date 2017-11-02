using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class Save_Load_Data{

    public static String Testpath = Application.streamingAssetsPath + "/test.json";


    public static String Tutorial = Application.streamingAssetsPath + "/Tutorial.json";
    public static String Level_one_TrackData = Application.streamingAssetsPath + "/TrackData_one.json";
    public static String Level_two_TrackData = Application.streamingAssetsPath + "/TrackData_two.json";
    public static String Level_three_TrackData = Application.streamingAssetsPath + "/TrackData_three.json";
    public static String Level_four_TrackData = Application.streamingAssetsPath + "/TrackData_four.json";



    public static bool Check_SaveFile(String path)
    {
        if (!System.IO.File.Exists(path))
        {
            return false;
        }
        else
            return true;
    }

    public static String load(String path)
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

        stream.Dispose();

        return response;
    }

    public static void Save(String path, String content)
    {
        FileStream stream = File.Create(path);
        byte[] contentBytes = new UTF8Encoding(true).GetBytes(content);
        stream.Write(contentBytes, 0, contentBytes.Length);
        stream.Dispose();
    }
}
