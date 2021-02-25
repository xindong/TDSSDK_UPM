using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public class TDSUserInfo
    {
        public string userId;

        public string name;

        public string avatar;

        public string taptapUserId;

        public bool isGuest;

        public long gender;

        public TDSUserInfo()
        {
            
        }

        public TDSUserInfo(string json)
        {   
            Dictionary<string,object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.userId = TDSCommon.SafeDictionary.GetValue<string>(dic,"userId");
            this.name = TDSCommon.SafeDictionary.GetValue<string>(dic,"name");
            this.avatar = TDSCommon.SafeDictionary.GetValue<string>(dic,"avatar");
            this.taptapUserId = TDSCommon.SafeDictionary.GetValue<string>(dic,"taptapUserId");
            this.isGuest = TDSCommon.SafeDictionary.GetValue<bool>(dic,"isGuest");
            this.gender = TDSCommon.SafeDictionary.GetValue<long>(dic,"gender");
        }

        public string ToJSON()
        {
            return TDSCommon.Json.Serialize(this);
        }

    }

}