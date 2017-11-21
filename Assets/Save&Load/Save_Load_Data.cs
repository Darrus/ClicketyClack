/** 
*  @file    Save_Load_Data.cs
*  @author  Yin Shuyu (150713R) 
*  
*  @brief Contain static class Save_Load_Data
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

/**
*  @brief A Static class for Save and Load Game Data
*/
public static class Save_Load_Data{

    public static String Test_path = Application.streamingAssetsPath + "/test.json"; ///< String path for a test json file


    public static String Tutorial_path = Application.streamingAssetsPath + "/Tutorial.json"; ///< String path for a Tutorial Data json file
    public static String Level_one_TrackData_path = Application.streamingAssetsPath + "/TrackData_one.json"; ///< String path for a Level 1 Data json file
    public static String Level_two_TrackData_path = Application.streamingAssetsPath + "/TrackData_two.json"; ///< String path for a Level 2 Data json file
    public static String Level_three_TrackData_path = Application.streamingAssetsPath + "/TrackData_three.json"; ///< String path for a Level 3 Data json file
    public static String Level_four_TrackData_path = Application.streamingAssetsPath + "/TrackData_four.json"; ///< String path for a Level 4 Data json file


    /**
   *   @brief A function to check whether Save file exists
   *  
   *   @param String path, which path file to check
   *   
   *   @return false, if dont exists
   *   @return true, if exists
   */
    public static bool Check_SaveFile(String path)
    {
        if (!System.IO.File.Exists(path))
        {
            return false;
        }
        else
            return true;
    }

    /**
    *   @brief A function to load file data
    *  
    *   @param String path, which path file to load data from
    *   
    *   @return null, if file dont exists
    *   @return String response, Data string load out from path
    */
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

    /**
    *   @brief A function to save file data
    *  
    *   @param String path, which path file to save data to
    *   
    *   @param String content, Data String to save to path
    *   
    *   @return nothing
    */
    public static void Save(String path, String content)
    {
        FileStream stream = File.Create(path);
        byte[] contentBytes = new UTF8Encoding(true).GetBytes(content);
        stream.Write(contentBytes, 0, contentBytes.Length);
        stream.Dispose();
    }
}
