//
//  NADUnityUserFeature.mm
//  Unity-iPhone
//
//
#import "NADUnityUserFeature.h"
#import "NADUnityGlobal.h"


///-----------------------------------------------
/// @name C Interfaces
///-----------------------------------------------

NendUnityUserFeaturePtr _InitNendUserFeatureUserFeatureObj ()
{
    NADUserFeature *IOSUserFeature = [NADUserFeature new];
    NADUnityCacheObject(IOSUserFeature);
    return (__bridge NendUnityUserFeaturePtr)IOSUserFeature;
}

void _SetNendUserFeatureGender (NendUnityUserFeaturePtr iOSPtr, int gender)
{
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSPtr;
    switch (gender) {
        case NADGenderFemale:
            IOSUserFeature.gender = NADGenderFemale;
            break;
        case NADGenderMale:
            IOSUserFeature.gender = NADGenderMale;
            break;
        default:
            break;
    }
}

void _SetNendUserFeatureBirthday (NendUnityUserFeaturePtr iOSPtr, int yearOfBirth, int monthOfBirth, int dayOfBirth)
{
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSPtr;
    [IOSUserFeature setBirthdayWithYear:yearOfBirth month:monthOfBirth day:dayOfBirth];
}

void _SetNendUserFeatureAge (NendUnityUserFeaturePtr iOSPtr, int age)
{
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSPtr;
    IOSUserFeature.age = age;
}

void _AddNendUserFeatureCustomFeatureInt (NendUnityUserFeaturePtr iOSPtr, const char* key, int value)
{
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSPtr;
    [IOSUserFeature addCustomIntegerValue:value forKey:NADUnityCreateNSString(key)];
}

void _AddNendUserFeatureCustomFeatureDouble (NendUnityUserFeaturePtr iOSPtr, const char* key, double value)
{
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSPtr;
    [IOSUserFeature addCustomDoubleValue:value forKey:NADUnityCreateNSString(key)];
}

void _AddNendUserFeatureCustomFeatureString (NendUnityUserFeaturePtr iOSPtr, const char* key, const char* value)
{
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSPtr;
    [IOSUserFeature addCustomStringValue:NADUnityCreateNSString(value) forKey:NADUnityCreateNSString(key)];
}

void _AddNendUserFeatureCustomFeatureBool (NendUnityUserFeaturePtr iOSPtr, const char* key, BOOL value)
{
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSPtr;
    [IOSUserFeature addCustomBoolValue:value forKey:NADUnityCreateNSString(key)];
}

void _DisposeNendUserFeaturePtr(NendUnityUserFeaturePtr iOSPtr)
{
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSPtr;
    NADUnityRemoveObject(IOSUserFeature);
}

