//
//  NADUnityUserFeature.h
//  Unity-iPhone
//
#import "NADUnityVideoAd.h"

typedef const void *NendUnityUserFeaturePtr;

extern "C" {
    NendUnityUserFeaturePtr _InitNendUserFeatureUserFeatureObj ();
    void _SetNendUserFeatureGender (NendUnityUserFeaturePtr iOSPtr, int gender);
    void _SetNendUserFeatureBirthday (NendUnityUserFeaturePtr iOSPtr, int yearOfBirth, int monthOfBirth, int dayOfBirth);
    void _SetNendUserFeatureAge (NendUnityUserFeaturePtr iOSPtr, int age);
    void _AddNendUserFeatureCustomFeatureInt (NendUnityUserFeaturePtr iOSPtr, const char* key, int value);
    void _AddNendUserFeatureCustomFeatureDouble (NendUnityUserFeaturePtr iOSPtr, const char* key, double value);
    void _AddNendUserFeatureCustomFeatureString(NendUnityUserFeaturePtr iOSPtr, const char* key, const char* value);
    void _AddNendUserFeatureCustomFeatureBool (NendUnityUserFeaturePtr iOSPtr, const char* key, BOOL value);
    void _DisposeNendUserFeaturePtr(NendUnityUserFeaturePtr iOSPtr);
}


