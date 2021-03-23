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

    public enum ErrorCode
    {
        /*
         * 未知错误
         */
        ERROR_CODE_UNDEFINED = 80000,

        /**
         * SDK 未初始化
         */
        ERROR_CODE_UNINITIALIZED = 80001,

        /**
         * 绑定取消
         */
        ERROR_CODE_BIND_CANCEL = 80002,
        /**
         * 绑定错误
         */ 
        ERROR_CODE_BIND_ERROR = 80003,
        
        /**
         * 登陆错误
         */
        ERROR_CODE_LOGOUT_INVALID_LOGIN_STATE = 80004,

        /**
         * 登陆被踢出
         */
        ERROR_CODE_LOGOUT_KICKED = 80007,

        /**
         * 桥接回调错误
         */
         ERROR_CODE_BRIDGE_EXECUTE = 80080

    }


}