using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public class TDSSDKError
    {
        public ErrorCode code;

        public string errorDescription;

        public TDSSDKError(string json)
        {
            Dictionary<string, object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string, object>;
            int parseCode = TDSCommon.SafeDictionary.GetValue<int>(dic, "code");
            this.code = ParseCode(parseCode);
            this.errorDescription = TDSCommon.SafeDictionary.GetValue<string>(dic, "error_description");
        }

        public TDSSDKError(int code,string errorDescription)
        {
            this.code = ParseCode(code);
            this.errorDescription = errorDescription;
        }

        public ErrorCode ParseCode(int parseCode)
        {
            return Enum.IsDefined(typeof(ErrorCode),parseCode) ? (ErrorCode) Enum.ToObject(typeof(ErrorCode),parseCode) : ErrorCode.ERROR_CODE_UNDEFINED;
        }

        public TDSSDKError(ErrorCode code,string errorDescription)
        {
            this.code = code;
            this.errorDescription = errorDescription;
        }

    }
}