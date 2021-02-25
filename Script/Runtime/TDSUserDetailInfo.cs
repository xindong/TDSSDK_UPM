using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TapSDK
{
    public class TDSUserDetailInfo
    {
        public string userId;

        public string name;

        public string avatar;

        public string taptapUserId;

        public bool isGuest;

        public long gender;

        public TDSUserCenterEntry userCenterEntry;

        public TDSUserDetailInfo()
        {

        }

        public TDSUserDetailInfo(string json)
        {   
            Dictionary<string,object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string,object>;
            this.userId = TDSCommon.SafeDictionary.GetValue<string>(dic,"userId");
            this.name = TDSCommon.SafeDictionary.GetValue<string>(dic,"name");
            this.avatar = TDSCommon.SafeDictionary.GetValue<string>(dic,"avatar");
            this.taptapUserId = TDSCommon.SafeDictionary.GetValue<string>(dic,"taptapUserId");
            this.isGuest = TDSCommon.SafeDictionary.GetValue<bool>(dic,"isGuest");
            this.gender = TDSCommon.SafeDictionary.GetValue<long>(dic,"gender");
            this.userCenterEntry = new TDSUserCenterEntry(TDSCommon.SafeDictionary.GetValue<object>(dic,"userCenterEntry") as Dictionary<string,object>);
        }

        public string ToJSON()
        {
            return TDSCommon.Json.Serialize(this);
        }

    }

    public class TDSUserCenterEntry
    {
        public bool isMomentEnabled;

        public TDSUserCenterEntry(Dictionary<string,object> dic)
        {
            this.isMomentEnabled = TDSCommon.SafeDictionary.GetValue<bool>(dic,"isMomentEnabled");
        }

        public string ToJSON()
        {
            return TDSCommon.Json.Serialize(this);
        }

    }

}