//
//  NADUnityNativeAd.h
//  Unity-iPhone
//

typedef const void *NADNativeClientPtr;
typedef const void *NendUnityNativeAdClientPtr;
typedef const void *NendUnityNativeAdPtr;
typedef void (*NendUnityNativeAdCallback)(NendUnityNativeAdClientPtr unityNativeAdClient, NendUnityNativeAdPtr nativeAd, int code, const char* message);

extern "C" {
    NADNativeClientPtr _CreateNativeAdClient(const char* apiKey, const char* spotId);
    void _ReleaseNativeAdClient(NADNativeClientPtr nativeAdClient);
    void _LoadNativeAd(NendUnityNativeAdClientPtr unityNativeAdClient, NADNativeClientPtr nativeAdClient, NendUnityNativeAdCallback callback);
    const char* _GetShortText(NendUnityNativeAdPtr nativeAd);
    const char* _GetLongText(NendUnityNativeAdPtr nativeAd);
    const char* _GetPromotionName(NendUnityNativeAdPtr nativeAd);
    const char* _GetPromotionUrl(NendUnityNativeAdPtr nativeAd);
    const char* _GetActionButtonText(NendUnityNativeAdPtr nativeAd);
    const char* _GetAdImageUrl(NendUnityNativeAdPtr nativeAd);
    const char* _GetLogoImageUrl(NendUnityNativeAdPtr nativeAd);
    const char* _GetAdvertisingExplicitlyText(NendUnityNativeAdPtr nativeAd, int unityAdvertisingExplicitly);
    void _PerformAdClick(NendUnityNativeAdPtr nativeAd);
    void _PerformInformationClick(NendUnityNativeAdPtr nativeAd);
    void _SendImpression(NendUnityNativeAdPtr nativeAd);
    void _ReleaseNativeAd(NendUnityNativeAdPtr nativeAd);
}