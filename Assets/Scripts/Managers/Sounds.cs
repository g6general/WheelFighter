using System.Collections.Generic;
using UnityEngine;

namespace sp
{
    public static class Sounds
    {
        private static bool mIsInitialized = false;

        public static void InitManager()
        {
            if (!mIsInitialized)
            {
                if (!Profile.Settings.IsSoundOn)
                    AudioListener.pause = true;

                Debug.Log("Sounds manager loaded");
            }
        }

        public static void ResetManager()
        {
            mIsInitialized = false;
        }

        public static void SwitchOn()
        {
            Profile.Settings.IsSoundOn = true;                             
            AudioListener.pause = false;

            var args = new Dictionary<string, string>();
            args.Add("isSoundOn", "true");
            Server.Report(Server.ReportComands.SoundSwitched, args);
        }

        public static void SwitchOff()
        {
            Profile.Settings.IsSoundOn = false;
            AudioListener.pause = true;

            var args = new Dictionary<string, string>();
            args.Add("isSoundOn", "false");
            Server.Report(Server.ReportComands.SoundSwitched, args);
        }

        public static void Switch()
        {
            if (Profile.Settings.IsSoundOn)
                SwitchOff();
            else
                SwitchOn();
        }

        public static bool IsSoundOn()
        {
            return Profile.Settings.IsSoundOn;
        }
    }

}