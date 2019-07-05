#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Collections.Generic;

public class NendIOSXcodeSetup : MonoBehaviour
{
	[PostProcessBuild]
	static void OnPostprocessBuild (BuildTarget buildTarget, string path)
	{
		if (buildTarget != BuildTarget.iOS) {
			return;
		}
			
		string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

		PBXProject proj = new PBXProject();
		proj.ReadFromFile(projPath);

		string target = proj.TargetGuidByName ("Unity-iPhone");

		proj.AddFrameworkToProject (target, "AdSupport.framework", false);
		proj.AddFrameworkToProject (target, "Security.framework", false);
		proj.AddFrameworkToProject (target, "ImageIO.framework", false);
		proj.AddFrameworkToProject (target, "WebKit.framework", false);

		proj.WriteToFile(projPath);
	}
}
#endif