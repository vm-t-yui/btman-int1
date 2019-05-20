//
//  NADUnityInterstitialAd.m
//  Unity-iPhone
//

#import "NADUnityInterstitialAd.h"

#import <NendAd/NADLogger.h>
#import <NendAd/NADInterstitial.h>
#import "NADUnityGlobal.h"

extern UIViewController* UnityGetGLViewController();

static const char* UNITY_INTERSTITIAL_AD_GAME_OBJECT = "NendAdInterstitial";

//==============================================================================

@interface NADInterstitialEventDispatcher : NSObject <NADInterstitialDelegate>

+ (instancetype)sharedDispatcher;
- (void)dispatchShowResult:(NADInterstitialShowResult)result spotId:(NSString*)spotId;

@end

//==============================================================================

static NSInteger InterstitialStatusCodeToInteger(NADInterstitialStatusCode status)
{
    switch (status) {
        case SUCCESS:
            return 0;
        case INVALID_RESPONSE_TYPE:
            return 1;
        case FAILED_AD_REQUEST:
            return 2;
        case FAILED_AD_DOWNLOAD:
            return 3;
        default:
            return -1;
    }
}

static NSInteger InterstitialClickTypeToInteger(NADInterstitialClickType type)
{
    switch (type) {
        case DOWNLOAD:
            return 0;
        case CLOSE:
            return 1;
        case INFORMATION:
            return 2;
        default:
            return -1;
    }
}

//==============================================================================

@implementation NADInterstitialEventDispatcher

+ (instancetype)sharedDispatcher
{
    static NADInterstitialEventDispatcher* instance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[NADInterstitialEventDispatcher alloc] init];
    });
    return instance;
}

- (void)dispatchShowResult:(NADInterstitialShowResult)result spotId:(NSString*)spotId
{
    NSInteger value = -1;
    switch (result) {
        case AD_SHOW_SUCCESS:
            value = 0;
            break;
        case AD_LOAD_INCOMPLETE:
            value = 1;
            break;
        case AD_REQUEST_INCOMPLETE:
            value = 2;
            break;
        case AD_DOWNLOAD_INCOMPLETE:
            value = 3;
            break;
        case AD_FREQUENCY_NOT_REACHABLE:
            value = 4;
            break;
        case AD_SHOW_ALREADY:
            value = 5;
            break;
        case AD_CANNOT_DISPLAY:
            value = 6;
            break;
    }
    NSString* message = [NSString stringWithFormat:@"%ld:%@", (long)value, spotId];
    UnitySendMessage(UNITY_INTERSTITIAL_AD_GAME_OBJECT, "NendAdInterstitial_OnShowAd", (char *)[message UTF8String]);
}

- (void)didFinishLoadInterstitialAdWithStatus:(NADInterstitialStatusCode)status
{
    NSInteger value = InterstitialStatusCodeToInteger(status);
    NSString* message = [[NSNumber numberWithInteger:value] stringValue];
    UnitySendMessage(UNITY_INTERSTITIAL_AD_GAME_OBJECT, "NendAdInterstitial_OnFinishLoad", (char *)[message UTF8String]);
}

- (void)didFinishLoadInterstitialAdWithStatus:(NADInterstitialStatusCode)status spotId:(NSString*)spotId
{
    NSInteger value = InterstitialStatusCodeToInteger(status);
    NSString* message = [NSString stringWithFormat:@"%ld:%@", (long)value, spotId];
    UnitySendMessage(UNITY_INTERSTITIAL_AD_GAME_OBJECT, "NendAdInterstitial_OnFinishLoad", (char *)[message UTF8String]);
}

- (void)didClickWithType:(NADInterstitialClickType)type
{
    NSInteger value = InterstitialClickTypeToInteger(type);
    NSString* message = [[NSNumber numberWithInteger:value] stringValue];
    UnitySendMessage(UNITY_INTERSTITIAL_AD_GAME_OBJECT, "NendAdInterstitial_OnClickAd", (char *)[message UTF8String]);
}

- (void)didClickWithType:(NADInterstitialClickType)type spotId:(NSString*)spotId
{
    NSInteger value = InterstitialClickTypeToInteger(type);
    NSString* message = [NSString stringWithFormat:@"%ld:%@", (long)value, spotId];
    UnitySendMessage(UNITY_INTERSTITIAL_AD_GAME_OBJECT, "NendAdInterstitial_OnClickAd", (char *)[message UTF8String]);
}

@end

///-----------------------------------------------
/// @name C Interfaces
///-----------------------------------------------

void _LoadInterstitialAd(const char* apiKey, const char* spotId, BOOL isOutputLog)
{
    [NADLogger setLogLevel:(isOutputLog ? NADLogLevelDebug : NADLogLevelOff)];
    [NADInterstitial sharedInstance].delegate = [NADInterstitialEventDispatcher sharedDispatcher];
    
    [[NADInterstitial sharedInstance] loadAdWithApiKey:NADUnityCreateNSString(apiKey) spotId:NADUnityCreateNSString(spotId)];
}

void _ShowInterstitialAd(const char* spotId)
{
    NSString* spot = NADUnityCreateNSString(spotId);
    NADInterstitialShowResult result;
    if (spot && 0 < spot.length) {
        result = [[NADInterstitial sharedInstance] showAdFromViewController:UnityGetGLViewController() spotId:spot];
    } else {
        result = [[NADInterstitial sharedInstance] showAdFromViewController:UnityGetGLViewController()];
    }
    [[NADInterstitialEventDispatcher sharedDispatcher] dispatchShowResult:result spotId:spot];
}

void _DismissInterstitialAd()
{
    [[NADInterstitial sharedInstance] dismissAd];
}

void _SetAutoReloadEnabled(BOOL enabled)
{
    [NADInterstitial sharedInstance].enableAutoReload = enabled;
}
