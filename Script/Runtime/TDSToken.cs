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

        public TDSToken()
        {
            
        }

        public TDSToken(string json)
        {
            Dictionary<string,object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.kid = TDSCommon.SafeDictionary.GetValue<string>(dic,"kid");
            this.accessToken = TDSCommon.SafeDictionary.GetValue<string>(dic,"access_token");
            this.macAlgorithm = TDSCommon.SafeDictionary.GetValue<string>(dic,"mac_algorithm");
            this.tokenType = TDSCommon.SafeDictionary.GetValue<string>(dic,"token_type");
            this.macKey = TDSCommon.SafeDictionary.GetValue<string>(dic,"mac_key");
            this.expireIn = TDSCommon.SafeDictionary.GetValue<long>(dic,"expire_in");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }

    }
}