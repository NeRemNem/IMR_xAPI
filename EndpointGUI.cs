﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using IMR;
public class EndpointGUI : MonoBehaviour
{
    public string temp ="plz input endpoint here";
    public static EndpointGUI current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    private void OnGUI()
    {    
        temp = GUI.TextField(new Rect(10, 10, 200, 20), temp, 100);
        GUI.Label(new Rect(30,90,300,200), StatementSender.EndPoint);
        GUI.Label(new Rect(30, 130, 300, 200), StatementSender.debug_msg);
        if(StatementSender.lrs_response != null)
            GUI.Label(new Rect(30, 160, 300, 200), StatementSender.lrs_response.ToString());
        else
            GUI.Label(new Rect(30, 160, 300, 200), "NULL");

      
        if (GUI.Button(new Rect(10, 55, 100, 30), "Set Endpoint"))
        {
            StatementSender.EndPoint = temp;
            Debug.Log(StatementSender.EndPoint);
        }
    }
}