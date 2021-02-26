using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public class TDSLoginWrapper
    {
        public string wrapper;

        public int loginCallbackCode;

        public TDSLoginWrapper(string json)
        {
            Dictionary<string,object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = TDSCommon.SafeDictionary.GetValue<string>(dic,"wrapper");
            this.loginCallbackCode = TDSCommon.SafeDictionary.GetValue<int>(dic,"loginCallbackCode");
        }

    }

    public class TDSUserStatusWrapper
    {
        public string wrapper;

        public int userStatusCallbackCode;
    
        public TDSUserStatusWrapper(string json)
        {
            Dictionary<string,object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = TDSCommon.SafeDictionary.GetValue<string>(dic,"wrapper");
            this.userStatusCallbackCode = TDSCommon.SafeDictionary.GetValue<int>(dic,"userStatusCallbackCode");
        }

    }

    public class TDSUserInfoWrapper
    {
        public string wrapper;

        public int getUserInfoCode;

        public TDSUserInfoWrapper(string json)
        {
            Dictionary<string,object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = TDSCommon.SafeDictionary.GetValue<string>(dic,"wrapper");
            this.getUserInfoCode = TDSCommon.SafeDictionary.GetValue<int>(dic,"getUserInfoCode");
        }
    }

    public class TDSUserDetailInfoWrapper
    {
        public string wrapper;

        public int getUserDetailInfoCode;

        public TDSUserDetailInfoWrapper(string json)
        {
            Dictionary<string,object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.wrapper = TDSCommon.SafeDictionary.GetValue<string>(dic,"wrapper");
            this.getUserDetailInfoCode = TDSCommon.SafeDictionary.GetValue<int>(dic,"getUserDetailInfoCode");
        }
    }

}