using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public interface ITDS
    {
        void Init(string clientID, bool regionType);

        void RegisterTDSSDKLoginResultCallback(TDSLoginResultCallback callback);

        void RegisterTDSSDKUserStatusCallback(TDSUserStateChagneCallback callback);

        void LoginbyTapTap(string[] permission);

        void LoginbyGuest();

        void LoginbyApple();

        void BindWithTapTap(string[] permissions);

        void GetUserInfo(Action<TDSUserInfo, TDSSDKError> callback);

        void GetUserDetailInfo(Action<TDSUserDetailInfo, TDSSDKError> callback);

        void GetCurrentToken(Action<TDSToken> callback);

        void OpenUserCenter();

        void PreferLang(TDSLanguage lang);

        void Logout();
    }

    public interface TDSLoginResultCallback
    {
        void OnLoginSuccess(TDSToken token);

        void OnLoginCancel();

        void OnLoginError(TDSSDKError error);
    }

    public interface TDSUserStateChagneCallback
    {
        void OnLogout(TDSSDKError error);

        void OnBind(TDSSDKError error);
    }

    public class TDSConstants
    {
        public static string TDS_SERVICE = "TDSSDKService";

        public static string TDS_CLZ = "com.tapsdk.core.wrapper.TDSSDKService";

        public static string TDS_IMPL = "com.tapsdk.core.wrapper.TDSSDKServiceImpl";

    }

    public enum TDSLanguage
    {
        LANG_AUTO = 0,

        LANG_ZH_HANS = 1,

        LANG_EN = 2
    }

}