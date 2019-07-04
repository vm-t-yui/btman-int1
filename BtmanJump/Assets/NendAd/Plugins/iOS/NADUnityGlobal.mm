//
//  NADUnityGlobal.m
//  Unity-iPhone
//

#import "NADUnityGlobal.h"

static NSMutableDictionary *objectCache;

NSString* NADUnityCreateNSString(const char* string)
{
    if (string) {
        return @(string);
    } else {
        return @"";
    }
}

char* NADUnityMakeStringCopy(NSString* string)
{
    if (!string || 0 == string.length) {
        return NULL;
    }
    
    const char* utf8String = [string UTF8String];
    if (NULL == utf8String) {
        return NULL;
    }
    
    char* res = (char*)malloc(strlen(utf8String) + 1);
    strcpy(res, utf8String);
    return res;
}

void NADUnityCacheObject(NSObject *object)
{
    if (!objectCache) {
        objectCache = [NSMutableDictionary new];
    }
    objectCache[@(object.hash)] = object;
}

void NADUnityRemoveObject(NSObject *object)
{
    if (objectCache) {
        [objectCache removeObjectForKey:@(object.hash)];
    }
}
