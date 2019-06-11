//
//  NADUnityBannerAd.h
//  Unity-iPhone
//

extern "C" {    
    void _TryCreateBanner(const char* paramString);
    void _ShowBanner(const char* gameObject);
    void _HideBanner(const char* gameObject);
    void _ResumeBanner(const char* gameObject);
    void _PauseBanner(const char* gameObject);
    void _DestroyBanner(const char* gameObject);
    void _LayoutBanner(const char* gameObject, const char* paramString);
}