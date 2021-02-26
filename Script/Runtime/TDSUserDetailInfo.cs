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
            Dictionary<string, object> dic = TDSCommon.Json.Deserialize(json) as Dictionary<string, object>;
            this.userId = TDSCommon.SafeDictionary.GetValue<string>(dic, "user_id");
            this.name = TDSCommon.SafeDictionary.GetValue<string>(dic, "name");
            this.avatar = TDSCommon.SafeDictionary.GetValue<string>(dic, "avatar");
            this.taptapUserId = TDSCommon.SafeDictionary.GetValue<string>(dic, "taptap_user_id");
            this.isGuest = TDSCommon.SafeDictionary.GetValue<bool>(dic, "is_guest");
            this.gender = TDSCommon.SafeDictionary.GetValue<long>(dic, "gender");

            Dictionary<string,object> entry = TDSCommon.SafeDictionary.GetValue<Dictionary<string,object>>(dic, "user_center_entries") as Dictionary<string,object>;
            if (entry != null)
            {
                this.userCenterEntry = new TDSUserCenterEntry(entry);
            }
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }

    }

    public class TDSUserCenterEntry
    {
        public bool isMomentEnabled;

        public TDSUserCenterEntry(Dictionary<string, object> dic)
        {
            this.isMomentEnabled = TDSCommon.SafeDictionary.GetValue<bool>(dic, "is_moment_enabled");
        }

        public string ToJSON()
        {
            return JsonUtility.ToJson(this);
        }

    }

}