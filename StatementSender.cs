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
        private static string _endpoint = null;
        private static string _key = "fbaabcea7a969befabfb7a4b7cf11b59a007d129";
        private static string _secret_key = "5af60b9abcaef1eb2cc68d191c634fccaaee0688";
    
        public static string debug_msg = "";
        public static StatementLRSResponse lrs_response = null;
    
        private static RemoteLRS lrs;

        public static void Init()
        {
            lrs = new RemoteLRS(_endpoint,_key,_secret_key);
        }

        public static string EndPoint
        {
            get => _endpoint;
            set => _endpoint = value;
        }
        public static string UserName
        {
            get => _key;
            set => _key = value;
        }
        public static string Password
        {
            get => _secret_key;
            set => _secret_key = value;
        }
        public static void SendStatement()
        {
            if (_endpoint != null)
            {
                Init();
                lrs_response = lrs.SaveStatement(IMRStatement.GetStatement());
                if (lrs_response.success) //Success
                {
                    debug_msg = "Save statement: " + lrs_response.content.id;
                    Debug.Log(debug_msg);
                }
                else //Failure
                {
                    debug_msg = "Statement Failed: " + lrs_response.errMsg;
                    Debug.Log(debug_msg);
                }
            }
        }
    }

}