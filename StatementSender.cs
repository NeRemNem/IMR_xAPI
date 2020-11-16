using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinCan;
using TinCan.LRSResponses;
using System;
using Newtonsoft.Json.Linq;

namespace IMR
{
    public static class StatementSender
    {
        private static string endpoint = null;
        private static string key = "fbaabcea7a969befabfb7a4b7cf11b59a007d129";
        private static string secret_key = "5af60b9abcaef1eb2cc68d191c634fccaaee0688";
    
        public static string debug_msg = "";
        public static StatementLRSResponse lrs_res = null;
    
        private static RemoteLRS lrs;

        public static void Init()
        {
            lrs = new RemoteLRS(endpoint,key,secret_key);
        }

        public static string EndPoint
        {
            get => endpoint;
            set => endpoint = value;
        }
        public static string UserName
        {
            get => key;
            set => key = value;
        }
        public static string Password
        {
            get => secret_key;
            set => secret_key = value;
        }
        public static void SendStatement()
        {
            if (endpoint != null)
            {
                Init();
                lrs_res = lrs.SaveStatement(IMRStatement.GetStatement());
                if (lrs_res.success) //Success
                {
                    debug_msg = "Save statement: " + lrs_res.content.id;
                    Debug.Log(debug_msg);
                }
                else //Failure
                {
                    debug_msg = "Statement Failed: " + lrs_res.errMsg;
                    Debug.Log(debug_msg);
                }
            }
        }
    }

}