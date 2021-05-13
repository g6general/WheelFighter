using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace sp
{
    public delegate void CallBack(Server.RequestStatus result, string data = "");

    public static class Server
    {
        private static string mMainServerURL = "https://api.main.server.com";
        private static string mTestServerURL = "https://api.test.server.com";

        public static string ServerUrl;

        private static bool mIsInitialized = false;

        private static WebRequest mWebRequest;

        public enum ReportComands
        {
            StartSession,
            EndSession,
            SuspendSession,
            ResumeSession,
            LevelLoaded,
            LevelFinished,
            NextLevel,
            NextLevelByMoney,
            RestartLevel,
            BlockFinished,
            NotEnoughMoney,
            SoundSwitched,
            Purchase,
            BulletSelected,
            ShopVisit
        }

        public enum RequestComands
        {
            LoginPlayer,
            LogoutPlayer,
            CreatePlayer,
            GetPlayerInfo,
            RegisterPurchase
        }

        public enum RequestStatus
        {
            ResultOk,
            ResultNetworkError,
            ResultHttpError
        }

        public static void InitManager()
        {
            AddScriptComponent();

            if (!mIsInitialized)
            {
                if (Debug.isDebugBuild)
                    ServerUrl = mTestServerURL;
                else
                    ServerUrl = mMainServerURL;

                ReportSessionParameters();

                mIsInitialized = true;

                Debug.Log("Server manager loaded");
            }
        }

        public static void ResetManager()
        {
            mIsInitialized = false;
        }

        private static void AddScriptComponent()
        {
            var MainObject = GameObject.Find("Main");
            MainObject.AddComponent<WebRequest>();
            mWebRequest = MainObject.GetComponent<WebRequest>();
        }

        private static void ReportSessionParameters()
        {
            var args = new Dictionary<string, string>();

            args.Add("ScreenOrientation", Screen.orientation.ToString());
            args.Add("ScreenDPI",         Screen.dpi.ToString());
            args.Add("ScreenWidth",       Screen.width.ToString());
            args.Add("ScreenHeight",      Screen.height.ToString());

            args.Add("DeviceModel",    SystemInfo.deviceModel);
            args.Add("DeviceName",     SystemInfo.deviceName);
            args.Add("DeviceType",     SystemInfo.deviceType.ToString());
            args.Add("DeviceUniqueId", SystemInfo.deviceUniqueIdentifier);
            args.Add("DeviceOS",       SystemInfo.operatingSystem);

            args.Add("SystemLanguage", Application.systemLanguage.ToString());
            args.Add("GameVersion",    Application.version);
            args.Add("EngineVersion",  Application.unityVersion);

            CallBack callback = (RequestStatus result, string data) => {
                if (result != RequestStatus.ResultOk)
                {
                    Debug.LogError(data);
                }
            };

            Report(ReportComands.StartSession, args, callback);
        }

        public static void Report(ReportComands commandType, Dictionary<string, string> data = null, CallBack callback = null)
        {
            var commandData = new ReportCommand();

            commandData.Command = commandType.ToString();
            commandData.UnixTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
            commandData.TimeSinceStart = (long)Time.realtimeSinceStartup;

            if (data != null)
            {
                foreach (KeyValuePair<string, string> entry in data)
                    commandData.Arguments.Add(new CommandArgument(entry.Key, entry.Value));
            }

            var jsonString = JsonUtility.ToJson(commandData);

            mWebRequest.StartPostRequest(jsonString, callback);

            Debug.Log("[server report]");
            Debug.Log(jsonString);
        }

        public static void Request(RequestComands commandType, Dictionary<string, string> data, CallBack callback)
        {
            var requestString = ServerUrl + "/" + commandType.ToString() + "?";

            foreach (KeyValuePair<string, string> entry in data)
                requestString += (entry.Key + "=" + entry.Value + "&");

            requestString = requestString.Substring(0, requestString.Length - 1);

            mWebRequest.StartGetRequest(requestString, callback);

            Debug.Log("[server request]");
            Debug.Log(requestString);
        }
    }

    public class WebRequest : MonoBehaviour
    {
        public void StartPostRequest(string data, CallBack callback = null)
        {
            StartCoroutine(PostRequest(data, callback));
        }

        IEnumerator PostRequest(string data, CallBack callback = null)
        {
            UnityWebRequest webRequest = UnityWebRequest.Post(Server.ServerUrl, data);
            yield return webRequest.SendWebRequest();

            if (callback != null)
            {
                if (webRequest.isNetworkError)
                    callback(Server.RequestStatus.ResultNetworkError, webRequest.error);
                else if (webRequest.isHttpError)
                    callback(Server.RequestStatus.ResultHttpError, webRequest.error);
                else
                    callback(Server.RequestStatus.ResultOk);
            }
        }

        public void StartGetRequest(string data, CallBack callback)
        {
            StartCoroutine(GetRequest(data, callback));
        }

        IEnumerator GetRequest(string data, CallBack callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(data))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                    callback(Server.RequestStatus.ResultNetworkError, webRequest.error);
                else if (webRequest.isHttpError)
                    callback(Server.RequestStatus.ResultHttpError, webRequest.error);
                else
                    callback(Server.RequestStatus.ResultOk, webRequest.downloadHandler.text);
            }
        }
    }

    [Serializable]
    public class ReportCommand
    {
        public string Command;

        public List<CommandArgument> Arguments;

        public long UnixTime;

        public long TimeSinceStart;

        public ReportCommand()
        {
            Arguments = new List<CommandArgument>();
        }
    }

    [Serializable]
    public class CommandArgument
    {
        public string Name;
        public string Value;

        public CommandArgument(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}