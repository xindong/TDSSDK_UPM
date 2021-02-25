using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public interface ITDS
    {
        void Init(string clientID,bool regionType);

        void RegisterTDSSDKLoginResultCallback(TDSLoginResultCallback callback);

        void RegisterTDSSDKUserStatusCallback(TDSUserStateChagneCallback callback);

        void LoginbyTapTap(string[] permission);

        void LoginbyGuest();

        void LoginbyApple();
    
        void BindWithTapTap(string[] permissions);

        void GetUserInfo(Action<TDSUserInfo> callback);

        void GetUserDetailInfo(Action<TDSUserDetailInfo> callback);

        void GetCurrentToken(Action<TDSToken> callback);

        void Logout();
    }

    public interface TDSLoginResultCallback
    {
        void OnLoginSuccess(TDSToken token);

        void OnLoginCancel();

        void OnLoginError(string error);
    }

    public interface TDSUserStateChagneCallback
    {
        void OnLogout(string error);

        void OnBind(string error);
    }

    public class TDSConstants
    {
        public static string TDS_SERVICE = "TDSSDKService";

        public static string TDS_CLZ = "com.tapsdk.core.wrapper.TDSSDKService";

        public static string TDS_IMPL = "com.tapsdk.core.wrapper.TDSSDKServiceImpl";
    }   

}