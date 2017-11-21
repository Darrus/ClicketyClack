/** 
*  @file    VictimManager.cs
*  @author  Yin Shuyu (150713R) 
*  @date    21/11/2017
*  @brief Contain static class VictimManager
*  
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*  @brief A static Class to manage All the Save-able Victim in the game
*/
public static class VictimManager
{

    /**
    *  @brief A struct for single Victim status data
    */
    [System.Serializable]
    public struct VictimData
    {
        public GameObject theVictim; ///< GameObject of the victim
        public int Victim_ID; ///< id of the victim
        public AppManager.GameScene Level; ///< level of the victim belong to 
        public bool Saved; ///< bool trigger whether victim saved

        /**
        *  @brief to set Saved bool to true
        *  
        *  @return null
        */
        public void gotSave()
        {
            Saved = true;
        }
    };

    /**
    *  @brief A struct for Level Victim list data
    */
    [System.Serializable]
    public struct Level_VictimData
    {
        public List<VictimData> Victim_List; ///< list of VictimData struct
        public int VictimRemain; ///< number of Victim remain in the level to save


        /**
        *  @brief Update Victim List with the All_VictimData list to remove the saved victim
        *  
        *  @return null
        */
        public void UpdateVictimList()
        {
            VictimRemain = 0;

            for (int i = 0; i < All_VictimData.Count; i++)
            {
                for (int n = 0; n < Victim_List.Count; n++)
                {

                    if (All_VictimData[i].Victim_ID == Victim_List[n].Victim_ID)
                    {
                        if(Victim_List[n].Saved || Victim_List[n].theVictim == null)
                        {
                            All_VictimData[i].gotSave();
                            break;
                        }

                        if (All_VictimData[i].Saved)
                        {
                            Victim_List[n].gotSave();
                            GameObject.Destroy(Victim_List[n].theVictim);
                            break;
                        }

                        VictimRemain += 1;
                        break;
                    }
                }
            }
        }

    };

    public static List<VictimData> All_VictimData = null; ///< List of All the Victim Data in Game

    /**
    *  @brief Check Victim saved from the All_VictimData list
    *  
    *  @param List<VictimData> temp, List of VictimData to victim status to All_VictimData's victim status
    *  
    *  @return null
    */
    public static void Check_All_VictimData(List<VictimData> temp)
    {
        for (int i = 0; i < All_VictimData.Count; i++)
        {
            for (int n = 0; n < temp.Count; i++)
            {
                if(All_VictimData[i].Victim_ID == temp[n].Victim_ID)
                {
                    if (All_VictimData[i].Saved)
                    {
                        temp[n].gotSave();
                    }
                    break;
                }
            }
        }      
    }

    public static List<int> Level_RequireVictimSave = null; ///< List of number of Victim needed to save to unlock level

    /**
    *  @brief Check Requirement of how many victim still need to save for this level
    *  
    *  @param int level, the level to check for Requirement
    *  
    *  @return null
    */
    public static int Check_Level_RequireVictimSave(int level)
    {
        int Requirement = Level_RequireVictimSave[level - 1];
            Requirement -= Num_VictimSaved;

        if (Requirement < 0)
            Requirement = 0;

        return Requirement;
    }

    public static List<int> VictimRemain_Level; ///< list of number of the unsave victim remain in level

    public static int Num_VictimSaved; ///< number of total Victim saved

    /**
    *  @brief Update VictimRemain_Level list and Number of Victim Saved
    *  
    *  @return null
    */
    public static void Check_VictimRemain()
    {
        Num_VictimSaved = 0;
        VictimRemain_Level = new List<int>((int)AppManager.GameScene.TotalScene);

        for (int i = 0; i < All_VictimData.Count; i++)
        {
            if (!All_VictimData[i].Saved)
                VictimRemain_Level[(int)All_VictimData[i].Level] += 1;
            else
                Num_VictimSaved += 1;
        }
    }
}
