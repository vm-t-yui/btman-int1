#if UNITY_ANDROID
namespace NendUnityPlugin.Platform.Android
{
    using UnityEngine;

    internal class AndroidLoggerInterface : NendAdLoggerInterface
    {
        public void SetLogLevel(int level)
        {
            string loggerAndroidJavaPackageName = "net.nend.android.NendAdLogger";
            string[] levelNames = {
                "DEBUG",
                "INFO",
                "WARN",
                "ERROR"
            };
            string levelEnumName = loggerAndroidJavaPackageName + "$LogLevel";
            string enumValueName = "OFF";
            int index = level - 1;
            if (index >= 0 && index < levelNames.Length)
            {
                enumValueName = levelNames[index];
            }
            AndroidJavaClass logger = new AndroidJavaClass(loggerAndroidJavaPackageName);
            logger.CallStatic("setLogLevel", new AndroidJavaClass(levelEnumName).GetStatic<AndroidJavaObject>(enumValueName));
        }
    }
}
#endif