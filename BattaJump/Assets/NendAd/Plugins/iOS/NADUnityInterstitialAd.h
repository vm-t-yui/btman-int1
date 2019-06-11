//
//  NADUnityInterstitialAd.h
//  Unity-iPhone
//

extern "C" {
    void _LoadInterstitialAd(const char* apiKey, const char* spotId, BOOL isOutputLog);
    void _ShowInterstitialAd(const char* spotId);
    void _DismissInterstitialAd();
    void _SetAutoReloadEnabled(BOOL enabled);
}