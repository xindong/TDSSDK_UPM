//
//  TDSSDKConfig.h
//  TDSSDK
//
//  Created by Bottle K on 2021/2/24.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef NS_ENUM (NSInteger, TDSSDKRegionType) {
    TDSSDKRegionTypeCN,
    TDSSDKRegionTypeIO
};

@interface TDSSDKConfig : NSObject
@property (nonatomic, copy) NSString *clientId;
@property (nonatomic, assign) TDSSDKRegionType region;
@end

NS_ASSUME_NONNULL_END
