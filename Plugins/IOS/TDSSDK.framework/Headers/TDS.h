//
//  TDS.h
//  TDSSDK
//
//  Created by Bottle K on 2021/2/18.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <TDSSDK/TDSLoginResultDelegate.h>
#import <TDSSDK/TDSUserStatusChangeDelegate.h>
#import <TDSSDK/TDSUserInfo.h>
#import <TDSSDK/TDSUserDetailInfo.h>
#import <TDSSDK/TDSToken.h>
#import <TDSSDK/TDSSDKConfig.h>
#import <TapSDK/TapSDK.h>

#define TDSSDK_VERSION_NUMBER @"1"
#define TDSSDK_VERSION        @"1.0.0"

NS_ASSUME_NONNULL_BEGIN

typedef void (^TDSInitHandler)(NSError *_Nullable error);
typedef void (^TDSUserInfoHandler)(TDSUserInfo *_Nullable userInfo, NSError *_Nullable error);
typedef void (^TDSUserDetailHandler)(TDSUserDetailInfo *_Nullable userDetail, NSError *_Nullable error);

@interface TDS : NSObject

+ (instancetype)new NS_UNAVAILABLE;
- (instancetype)init NS_UNAVAILABLE;

/// 统一初始化
/// @param config 配置项
+ (void)initWithConfig:(TDSSDKConfig *)config;

/// 注册登录回调
/// @param delegate 回调
+ (void)registerLoginResultDelegate:(id <TDSLoginResultDelegate>)delegate;

/// 注册用户状态变化回调
/// @param delegate 回调
+ (void)registerUserStatusChangeDelegate:(id <TDSUserStatusChangeDelegate>)delegate;

/// TapTap 登录
/// @param permissions TapTap 授权权限
+ (void)loginByTapTap:(NSArray *)permissions;

/// Apple 登录
+ (void)loginByApple;

/// 游客登录
+ (void)loginByGuest;

/// 绑定 TapTap 账号
/// @param permissions TapTap 授权权限
+ (void)bindWithTapTap:(NSArray *)permissions;

/// 获取用户基本信息
+ (void)getUserInfo:(TDSUserInfoHandler)handler;

/// 获取用户详细信息
+ (void)getUserDetailInfo:(TDSUserDetailHandler)handler;

/// 获取当前 token
+ (TDSToken *)getCurrentToken;

/// 退出登录
+ (void)logout;

/// 打开用户中心
+ (void)openUserCenter;

@end

NS_ASSUME_NONNULL_END
