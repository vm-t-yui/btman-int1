//
//  NADUnityLogger.mm
//  Unity-iPhone
//

#import "NADUnityLogger.h"

#import <NendAd/NADLogger.h>

///-----------------------------------------------
/// @name C Interfaces
///-----------------------------------------------

void _SetLogLevel(int level)
{
    if (level <= NADLogLevelError) {
        [NADLogger setLogLevel:(NADLogLevel)level];
    } else {
        [NADLogger setLogLevel:NADLogLevelOff];
    }
}
