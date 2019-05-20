//
//  NADUnityVideoAd.h
//  Unity-iPhone
//

#import <NendAd/NADInterstitialVideo.h>
#import <NendAd/NADRewardedVideo.h>
#import <NendAd/NADVideo.h>
#import "NADUnityUserFeature.h"

typedef const void *NendIOSVideoAdPtr;
typedef const void *NendUnityVideoAdPtr;
typedef void (*NendUnityVideoAdNormalCallback)(NendUnityVideoAdPtr unityPtr, NSInteger result);
typedef void (*NendUnityVideoAdErrorCallback)(NendUnityVideoAdPtr unityPtr, NSInteger result, NSInteger errorCode);

//==============================================================================
typedef NS_ENUM(NSInteger, VideoAdCallback) {
    VideoAdCallbackLoadSuccess = 0,
    VideoAdCallbackFailedToLoad = 1,
    VideoAdCallbackFailedToPlay = 2,
    VideoAdCallbackAdShown = 3,
    VideoAdCallbackAdClosed = 4,
    VideoAdCallbackAdStarted = 5,
    VideoAdCallbackAdStopped = 6,
    VideoAdCallbackAdCompleted = 7,
    VideoAdCallbackAdClicked = 8,
    VideoAdCallbackInformationClicked = 9,
    
    VideoAdCallbackInformationRewarded = 10,
};

@interface NendIOSVideoAd : NSObject

@property (nonatomic) NendUnityVideoAdNormalCallback callback;
@property (nonatomic) NendUnityVideoAdErrorCallback errorCallback;
@property (nonatomic) NendUnityVideoAdPtr unityPtr;

@end
//==============================================================================
