using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TDSCommon;

namespace TapSDK
{
    public class TDSSDKImpl : ITDS
    {   
        private TDSSDKImpl()
        {
            EngineBridge.GetInstance().Register(TDSConstants.TDS_CLZ, TDSConstants.TDS_IMPL);
        }

        private volatile static TDSSDKImpl sInstance;

        private static readonly object locker = new object();

        public static TDSSDKImpl GetInstance()
        {
            lock (locker)
            {
                if (sInstance == null)
                {
                    sInstance = new TDSSDKImpl();
                }
            }
            return sInstance;
        }


        public void Init(string clientID, bool regionType)
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("init")
                .Args("clientID",clientID)
                .Args("regionType",regionType)
                .CommandBuilder();

            EngineBridge.GetInstance().CallHandler(command);
        }

        public void RegisterTDSSDKLoginResultCallback(TDSLoginResultCallback callback)
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("registerTDSSDKLoginResultCallback")
                .Callback(true)
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command,(result)=>
             {
                 if(!CheckBridgeResult(result))
                 {
                     callback.OnLoginError("Bridge execute RegisterTDSSDKLoginResultCallback Error!");
                     return;
                 }
                 TDSLoginWrapper loginWrapper = new TDSLoginWrapper(result.content);
                 if(loginWrapper.loginCallbackCode == 0)
                 {
                     callback.OnLoginSuccess(new TDSToken(loginWrapper.wrapper));
                     return;
                 }
                 if(loginWrapper.loginCallbackCode == 1)
                 {
                     callback.OnLoginCancel();
                     return;
                 }
                 callback.OnLoginError(loginWrapper.wrapper);
             });
        }

        public void RegisterTDSSDKUserStatusCallback(TDSUserStateChagneCallback callback)
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("registerTDSSDKUserStatusCallback")
                .Callback(true)
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command,(result)=>
             {
                 if(!CheckBridgeResult(result))
                 {
                     return;
                 }
                TDSUserStatusWrapper wrapper = new TDSUserStatusWrapper(result.content);
                if(wrapper.userStatusCallbackCode == 1)
                {
                    callback.OnLogout(wrapper.wrapper);
                    return;
                }
                callback.OnBind(wrapper.wrapper);
             });
        }

        public void LoginbyTapTap(string[] permission)
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("loginbyTapTap")
                .Args("permissions",permission)
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command);
        }

        public void LoginbyGuest()
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("loginbyGuest")
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command);
        }

        public void LoginbyApple()
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("loginbyApple")
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command);
        }

        public void BindWithTapTap(string[] permissions)
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("bindWithTapTap")
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command);
        }

        public void GetUserInfo(Action<TDSUserInfo> callback)
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("getUserInfo")
                .Callback(true)
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command,(result)=>
             {
                if(!CheckBridgeResult(result))
                {
                    return;
                }
                callback(new TDSUserInfo(result.content));
             });
        }

        public void GetUserDetailInfo(Action<TDSUserDetailInfo> callback)
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("getUserDetailInfo")
                .Callback(true)
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command,(result)=>
             {
                if(!CheckBridgeResult(result))
                {
                    return;
                }
                callback(new TDSUserDetailInfo(result.content));
             });
        }

        public void GetCurrentToken(Action<TDSToken> callback)
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("getCurrentToken")
                .Callback(true)
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command,(result)=>
             {
                 if(!CheckBridgeResult(result))
                {
                    return;
                }
                callback(new TDSToken(result.content));
             });
        }

        public void Logout()
        {
            Command command = new Command.Builder()
                .Service(TDSConstants.TDS_SERVICE)
                .Method("logout")
                .CommandBuilder();
             EngineBridge.GetInstance().CallHandler(command);
        }

        public bool CheckBridgeResult(Result result)
        {
            if(result == null)
            {
                return false;
            }

            if(result.code != 0)
            {
                return false;
            }

            return result.content != null && result.content.Length != 0;
        }
    }

}