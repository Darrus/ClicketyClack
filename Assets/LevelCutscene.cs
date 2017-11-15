/** 
 *  @file    nameSort.cpp
 *  @author  Nomed Nocaed (nnocae10)
 *  @date    1/18/2014  
 *  @version 1.0 
 *  
 *  @brief CSC 112, Lab 1, sorts strings using insertion sort
 *
 *  @section DESCRIPTION
 *  
 *  This is a little program that reads a list of names from
 *  a specified file or from standard input and then sorts
 *  the names in ascending order and prints them to standard
 *  output.
 *  
 *  Command line arguments are used to specify where the
 *  list of names should be read from.  If the program
 *  doesn't receive any command line arguments then it reads
 *  the names from standard input. If the program receives
 *  a single command line argument then it reads the names
 *  from the corresponding file.  If more than one command
 *  line argument is specified the program prints a usage
 *  message and terminates.
 *
 */

using UnityEngine;

/**
  *  @brief Class that maintains a variable-sized stack of integers.  
  */
public class LevelCutscene : MonoBehaviour {
    public GameObject train;
    public HumanController[] victims;

    private void Start()
    {
        ExecuteCutscene();
    }

    public void ExecuteCutscene()
    {
        foreach(HumanController victim in victims)
        {
            victim.RunTo(train.transform.position);
        }
    }
}
