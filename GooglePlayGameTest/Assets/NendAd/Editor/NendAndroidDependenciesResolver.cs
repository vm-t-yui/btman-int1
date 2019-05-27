using System;
using System.Collections.Generic;
using UnityEditor;

[InitializeOnLoad]
public static class NendAndroidDependenciesResolver {

    private const string GmsIdentifierArtifactName = "play-services-ads-identifier";
	private const string GmsLocationArtifactName = "play-services-location";
	private const string ConstraintLayoutArtifactName = "constraint-layout";

	private const string AndroidLibraryTypeGoogle = "com.google.android.gms";
	private const string AndroidLibraryTypeConstraint = "com.android.support.constraint";

	static NendAndroidDependenciesResolver () {
#if UNITY_ANDROID
        Type playServicesSupport = Google.VersionHandler.FindClass("Google.JarResolver", "Google.JarResolver.PlayServicesSupport");
        if (playServicesSupport == null)
        {
            return;
        }

        object svcSupport = Google.VersionHandler.InvokeStaticMethod(
          playServicesSupport, "CreateInstance",
          new object[] {
            "NendUnityPlugin",
            EditorPrefs.GetString("AndroidSdkRoot"),
            "ProjectSettings"
        });

        //Require
        DependOn(svcSupport, AndroidLibraryTypeGoogle, GmsIdentifierArtifactName, "15.0.1");
		DependOn(svcSupport, AndroidLibraryTypeConstraint, ConstraintLayoutArtifactName, "1.1.3");

		//Optional
		DependOn(svcSupport, AndroidLibraryTypeGoogle, GmsLocationArtifactName, "15.0.1");
		#endif
	}

    private static void DependOn(object svcSupport, string type, string name, string version) {
        Google.VersionHandler.InvokeInstanceMethod(svcSupport, "DependOn", new object[] {
            type, name, version
        }, namedArgs: new Dictionary<string, object>() {
            {"packageIds", new string[] { "extra-google-m2repository" }
            }
        });
    }
}
