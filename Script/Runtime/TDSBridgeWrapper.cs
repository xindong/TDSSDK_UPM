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

}