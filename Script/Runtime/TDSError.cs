using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public class TDSSDKError
    {
        public int code;

        public string errorDescription;

        public TDSSDKError(string json)
        {
            Dictionary<string, object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string, object>;
            this.code = TDSCommon.SafeDictionary.GetValue<int>(dic, "code");
            this.errorDescription = TDSCommon.SafeDictionary.GetValue<string>(dic, "error_description");
        }

        public TDSSDKError(int code,string errorDescription)
        {
            this.code = code;
            this.errorDescription = errorDescription;
        }

    }
}