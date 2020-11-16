using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinCan;
using TinCan.LRSResponses;
using System;
using Newtonsoft.Json.Linq;

namespace IMR
{
    public static class IMRStatement
    {
        private static Agent _actor;
        private static Verb _verb;
        private static Activity _activity;
        private static ActivityDefinition _definition;
        private static Result _result;
        private static DateTime? _timestamp;
        private static Agent _authority;
        private static bool _is_init = false;

        public static void Init()
        {
            if (_is_init == false)
            {
                _is_init = true;
                _actor = new Agent();
                _verb = new Verb();
                _activity = new Activity();
                _definition = new ActivityDefinition();
                _result = new Result();
                _timestamp = null;
                _authority = new Agent();
                SetAuthority();
            }
        }

        public static void SetActor(string actor = "korea_tech")
        {
            Init();
            _actor.mbox = "mailto:" + actor.Replace(" ", "") + "@google.com";
            _actor.name = actor;
        }

        public static void SetVerb(string verb)
        {
            Init();
            _verb.id = new Uri("http://imrxapi.com/" + verb.Replace(" ", ""));
            _verb.display = new LanguageMap();
            _verb.display.Add("en", verb);
        }

        public static void SetActivity(string activity)
        {
            Init();
            _activity.id = new Uri("https://www.koreatech.ac.kr/" + activity.Replace(" ", "")).ToString();
        }

        public static void SetDefinition(string def)
        {
            Init();
            _definition.description = new LanguageMap();
            _definition.name = new LanguageMap();
            _definition.name.Add("en", (def));
            _activity.definition = _definition;
        }

        public static void SetScore(int value)
        {            
            Init();
            _result.score.raw = value;
        }

        public static void SetTimestamp(DateTime t)
        {
            Init();
            _timestamp = t;
        }

        public static void SetAuthority()
        {
            Init();
            _authority.name = "KoreaTech";
            _authority.mbox = "mailto:KoreaTech@koreatech.ac.kr";
        }

        public static void SetAuthority(string name, string mbox)
        {
            Init();
            _authority.name = name;
            _authority.mbox = mbox;
        }

        public static Statement GetStatement()
        {
            Statement statement = new Statement();
            statement.actor = _actor;
            statement.verb = _verb;
            statement.target = _activity;
            statement.result = _result;
            statement.timestamp = DateTime.Now;
            statement.authority = _authority;
            
            return statement;
        }
    }
}
