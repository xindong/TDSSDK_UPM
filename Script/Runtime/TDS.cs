using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public class TDS
    {
        public static void Init(string clientID,bool regionType)
        {
            TDSSDKImpl.GetInstance().Init(clientID,regionType);
        }

        public static void RegisterTDSSDKLoginResultCallback(TDSLoginResultCallback callback)
        {
            TDSSDKImpl.GetInstance().RegisterTDSSDKLoginResultCallback(callback);
        }

        public static void RegisterTDSSDKUserStatusCallback(TDSUserStateChagneCallback callback)
        {
            TDSSDKImpl.GetInstance().RegisterTDSSDKUserStatusCallback(callback);
        }

        public static void LoginbyTapTap(string[] permission)
        {
            TDSSDKImpl.GetInstance().LoginbyTapTap(permission);
        }

        public static void LoginbyGuest()
        {
            TDSSDKImpl.GetInstance().LoginbyGuest();
        }

        public static void LoginbyApple()
        {
            TDSSDKImpl.GetInstance().LoginbyApple();
        }
    
        public static void BindWithTapTap(string[] permissions)
        {
            TDSSDKImpl.GetInstance().BindWithTapTap(permissions);
        }

        public static void GetUserInfo(Action<TDSUserInfo> callback)
        {
            TDSSDKImpl.GetInstance().GetUserInfo(callback);
        }

        public static void GetUserDetailInfo(Action<TDSUserDetailInfo> callback)
        {
            TDSSDKImpl.GetInstance().GetUserDetailInfo(callback);
        }

        public static void GetCurrentToken(Action<TDSToken> callback)
        {
            TDSSDKImpl.GetInstance().GetCurrentToken(callback);
        }

        public static void Logout()
        {   
            TDSSDKImpl.GetInstance().Logout();
        }

    }

}