using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PageControll : MonoBehaviour
{
    static public int Num;
    public GameObject[] Page;

    void Start ()
    {
        Num = 0;
        for (int i = 1; i < Page.Length; i++)
        {
            Page[i].SetActive(false);
        }
        Page[Num].SetActive(true);
    }

    public void RightClick()
    {
        // Next Page
        Num++;
        Page[Num - 1].SetActive(false);
        if (Num >= Page.Length)
            Num = 0;
        Page[Num].SetActive(true);
    }

    public void LeftClick()
    {
        // Back Page
        Num--;
        Page[Num + 1].SetActive(false);
        if (Num <= -1)
            Num = 2;
        Page[Num].SetActive(true);
    }
}
