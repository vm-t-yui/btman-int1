//
//  NADUnityRewardedVideoAd.m
//  Unity-iPhone
//

#import <NendAd/NADLogger.h>
#import "NADUnityVideoAd.h"
#import "NADUnityRewardedVideoAd.h"
#import "NADUnityGlobal.h"

extern UIViewController* UnityGetGLViewController();

//==============================================================================

@interface NendIOSRewardedVideoAd : NendIOSVideoAd <NADRewardedVideoDelegate>

@property (nonatomic) NADRewardedVideo *rewardedVideo;
@property (nonatomic) NendUnityRewardedVideoAdCallback rewardedCallback;

@end

//==============================================================================

@implementation NendIOSRewardedVideoAd

#pragma mark - NADRewardedVideoDelegate

- (void)nadRewardVideoAdDidReceiveAd:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackLoadSuccess);
    }
}

- (void)nadRewardVideoAd:(NADRewardedVideo *)nadRewardedVideoAd didFailToLoadWithError:(NSError *)error
{
    NSLog(@"%s : error = %@", __FUNCTION__, error);
    if (self.errorCallback) {
        self.errorCallback(self.unityPtr, VideoAdCallbackFailedToLoad, error.code);
    }
}

- (void)nadRewardVideoAdDidFailedToPlay:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackFailedToPlay);
    }
}

- (void)nadRewardVideoAdDidOpen:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdShown);
    }
}

- (void)nadRewardVideoAdDidClose:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdClosed);
    }
}

- (void)nadRewardVideoAdDidStartPlaying:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdStarted);
    }
}

- (void)nadRewardVideoAdDidStopPlaying:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdStopped);
    }
}

- (void)nadRewardVideoAdDidCompletePlaying:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdCompleted);
    }
}

- (void)nadRewardVideoAdDidClickAd:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackAdClicked);
    }
}

- (void)nadRewardVideoAdDidClickInformation:(NADRewardedVideo *)nadRewardedVideoAd
{
    NSLog(@"%s", __FUNCTION__);
    if (self.callback) {
        self.callback(self.unityPtr, VideoAdCallbackInformationClicked);
    }
}

- (void)nadRewardVideoAd:(NADRewardedVideo *)nadRewardedVideoAd didReward:(NADReward *)reward
{
    NSLog(@"%s", __FUNCTION__);
    if (self.rewardedCallback) {
        const char* currencyName = (reward.name != nil ? reward.name.UTF8String : "");
        self.rewardedCallback(self.unityPtr, VideoAdCallbackInformationRewarded, currencyName, reward.amount);
    }
}


@end

///-----------------------------------------------
/// @name C Interfaces
///-----------------------------------------------

NendIOSVideoAdPtr _CreateRewardedVideoAd(const char* spotId, const char* apiKey)
{
    NendIOSRewardedVideoAd *IOSVideoAd = [[NendIOSRewardedVideoAd alloc] init];
    IOSVideoAd.rewardedVideo = [[NADRewardedVideo alloc] initWithSpotId:NADUnityCreateNSString(spotId) apiKey:NADUnityCreateNSString(apiKey)];
    IOSVideoAd.rewardedVideo.delegate = IOSVideoAd;
    NADUnityCacheObject(IOSVideoAd);
    return (__bridge NendIOSVideoAdPtr)IOSVideoAd;
}

void _LoadRewardedVideoAd(NendUnityVideoAdPtr unityPtr, NendIOSVideoAdPtr iOSPtr, NendUnityVideoAdNormalCallback callback, NendUnityVideoAdErrorCallback errorCallback)
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    IOSVideoAd.callback = callback;
    IOSVideoAd.errorCallback = errorCallback;
    IOSVideoAd.unityPtr = unityPtr;
    [IOSVideoAd.rewardedVideo loadAd];
}

bool _IsLoadedRewarded(NendIOSVideoAdPtr iOSPtr)
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    return [IOSVideoAd.rewardedVideo isReady];
}

void _ShowRewardedVideoAd(NendIOSVideoAdPtr iOSPtr, NendUnityRewardedVideoAdCallback rewardCallback)
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    IOSVideoAd.rewardedCallback = rewardCallback;
    if (IOSVideoAd.rewardedVideo.isReady) {
        [IOSVideoAd.rewardedVideo showAdFromViewController:UnityGetGLViewController()];
    }
}

void _ReleaseRewardedVideoAd(NendIOSVideoAdPtr iOSPtr)
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    [IOSVideoAd.rewardedVideo releaseVideoAd];
}

void _SetRewardedMediationName (NendIOSVideoAdPtr iOSPtr, const char* mediationName)
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    [IOSVideoAd.rewardedVideo setMediationName:NADUnityCreateNSString(mediationName)];
}

void _SetRewardedUserId (NendIOSVideoAdPtr iOSPtr, const char* userId)
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    [IOSVideoAd.rewardedVideo setUserId:NADUnityCreateNSString(userId)];
}

void _SetRewardedUserFeature (NendIOSVideoAdPtr iOSPtr, NendUnityUserFeaturePtr iOSUserFeaturePtr )
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    NADUserFeature *IOSUserFeature = (__bridge NADUserFeature *)iOSUserFeaturePtr;
    
    IOSVideoAd.rewardedVideo.userFeature = IOSUserFeature;
}

void _SetRewardedLocationEnabled (NendIOSVideoAdPtr iOSPtr, BOOL enabled)
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    IOSVideoAd.rewardedVideo.isLocationEnabled = enabled;
}

void _SetRewardedOutputLog (BOOL isOutputLog)
{
    [NADLogger setLogLevel:(isOutputLog ? NADLogLevelDebug : NADLogLevelOff)];
}

void _DisposeRewardedVideoAd(NendIOSVideoAdPtr iOSPtr)
{
    NendIOSRewardedVideoAd *IOSVideoAd = (__bridge NendIOSRewardedVideoAd *)iOSPtr;
    NADUnityRemoveObject(IOSVideoAd);
}
