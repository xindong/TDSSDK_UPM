using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public class TDSToken
    {
        public string kid;

        public string accessToken;

        public string macAlgorithm;

        public string tokenType;

        public string macKey;

        public long expireIn;

        public TDSToken(string json)
        {
            Dictionary<string,object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.kid = TDSCommon.SafeDictionary.GetValue<string>(dic,"kid");
            this.accessToken = TDSCommon.SafeDictionary.GetValue<string>(dic,"accessToken");
            this.macAlgorithm = TDSCommon.SafeDictionary.GetValue<string>(dic,"macAlgorithm");
            this.tokenType = TDSCommon.SafeDictionary.GetValue<string>(dic,"tokenType");
            this.macKey = TDSCommon.SafeDictionary.GetValue<string>(dic,"macKey");
            this.expireIn = TDSCommon.SafeDictionary.GetValue<long>(dic,"expireIn");
        }

        public string ToJSON()
        {
            return TDSCommon.Json.Serialize(this);
        }

    }
}