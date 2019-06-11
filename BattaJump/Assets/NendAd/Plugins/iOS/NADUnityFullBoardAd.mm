//
//  NADUnityFullBoardAd.m
//  Unity-iPhone
//

#import "NADUnityFullBoardAd.h"
#import "NADUnityGlobal.h"

extern UIViewController* UnityGetGLViewController();

typedef NS_ENUM(NSInteger, FullBoardAdCallback) {
    FullBoardAdCallbackLoadSuccess = 0,
    FullBoardAdCallbackAdShown = 4,
    FullBoardAdCallbackAdClicked = 5,
    FullBoardAdCallbackAdDismissed = 6,
};

//==============================================================================

@interface NendIOSFullBoardAd : NSObject <NADFullBoardDelegate>

@property (nonatomic) NADFullBoardLoader *loader;
@property (nonatomic) NADFullBoard *ad;
@property (nonatomic) NendUnityFullBoardAdCallback callback;
@property (nonatomic) NendUnityFullBoardAdPtr unityPtr;

@end

//==============================================================================

@implementation NendIOSFullBoardAd

// 広告表示時に呼び出されます
- (void)NADFullBoardDidShowAd:(NADFullBoard *)ad
{
    if (self.callback) {
        self.callback(self.unityPtr, FullBoardAdCallbackAdShown);
    }
}

// 広告クリック時に呼び出されます
- (void)NADFullBoardDidClickAd:(NADFullBoard *)ad
{
    if (self.callback) {
        self.callback(self.unityPtr, FullBoardAdCallbackAdClicked);
    }
}

// 広告クローズ時に呼び出されます
- (void)NADFullBoardDidDismissAd:(NADFullBoard *)ad
{
    if (self.callback) {
        self.callback(self.unityPtr, FullBoardAdCallbackAdDismissed);
    }
}

@end

///-----------------------------------------------
/// @name C Interfaces
///-----------------------------------------------

NendIOSFullBoardAdPtr _CreateFullBoardAd(const char* spotId, const char* apiKey)
{
    NendIOSFullBoardAd *iOSFullBoardAd = [[NendIOSFullBoardAd alloc] init];
    iOSFullBoardAd.loader = [[NADFullBoardLoader alloc] initWithSpotId:NADUnityCreateNSString(spotId) apiKey:NADUnityCreateNSString(apiKey)];
    NADUnityCacheObject(iOSFullBoardAd);
    return (__bridge NendIOSFullBoardAdPtr)iOSFullBoardAd;
}

void _LoadFullBoardAd(NendUnityFullBoardAdPtr unityPtr, NendIOSFullBoardAdPtr iOSPtr, NendUnityFullBoardAdCallback callback)
{
    NendIOSFullBoardAd *iOSFullBoardAd = (__bridge NendIOSFullBoardAd *)iOSPtr;
    iOSFullBoardAd.callback = callback;
    
    [iOSFullBoardAd.loader loadAdWithCompletionHandler:^(NADFullBoard *ad, NADFullBoardLoaderError error) {
        if (ad) {
            iOSFullBoardAd.ad = ad;
            iOSFullBoardAd.ad.delegate = iOSFullBoardAd;
            if (iOSFullBoardAd.callback) {
                iOSFullBoardAd.callback(unityPtr, FullBoardAdCallbackLoadSuccess);
            }
        } else {
            if (iOSFullBoardAd.callback) {
                iOSFullBoardAd.callback(unityPtr, error);
            }
        }
    }];
}

void _ShowFullBoardAd(NendUnityFullBoardAdPtr unityPtr, NendIOSFullBoardAdPtr iOSPtr)
{
    NendIOSFullBoardAd *iOSFullBoardAd = (__bridge NendIOSFullBoardAd *)iOSPtr;
    iOSFullBoardAd.unityPtr = unityPtr;
    if (iOSFullBoardAd.ad) {
        [iOSFullBoardAd.ad showFromViewController:UnityGetGLViewController()];
    }
}

void _SetFullBoardAdBackgroundColor(NendIOSFullBoardAdPtr iOSPtr, float r, float g, float b, float a)
{
    NendIOSFullBoardAd *iOSFullBoardAd = (__bridge NendIOSFullBoardAd *)iOSPtr;
    if (iOSFullBoardAd.ad) {
        iOSFullBoardAd.ad.backgroundColor = [UIColor colorWithRed:r green:g blue:b alpha:a];
    }
}

void _ReleaseFullBoardAd(NendIOSFullBoardAdPtr iOSPtr)
{
    NendIOSFullBoardAd *iOSFullBoardAd = (__bridge NendIOSFullBoardAd *)iOSPtr;
    NADUnityRemoveObject(iOSFullBoardAd);
}

