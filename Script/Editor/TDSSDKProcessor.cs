#if UNITY_EDITOR && UNITY_IOS
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif
using UnityEngine;

namespace TDSEditor
{
     public class TDSSDKProcessor
    {
#if UNITY_IOS
        [PostProcessBuildAttribute(99)]
        public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)
        {
            
            if (BuildTarget == BuildTarget.iOS)
            {   
                // 获得工程路径
                string projPath = PBXProject.GetPBXProjectPath(path);
                UnityEditor.iOS.Xcode.PBXProject proj = new PBXProject();
                proj.ReadFromString(File.ReadAllText(projPath));

#if UNITY_2019_3_OR_NEWER
                string unityFrameworkTarget = proj.GetUnityFrameworkTargetGuid();
                string target = proj.GetUnityMainTargetGuid();
#else
                string unityFrameworkTarget = proj.TargetGuidByName("Unity-iPhone");
                string target = proj.TargetGuidByName("Unity-iPhone");
#endif
                if (target == null)
                {
                    Debug.Log("target is null ?");
                    return;
                }
                
                var resourcePath = Path.Combine(path, "TDSUltraResource");

                string parentFolder = Directory.GetParent(Application.dataPath).FullName;

                if (Directory.Exists(resourcePath))
                {
                    Directory.Delete(resourcePath,true);
                }

                Directory.CreateDirectory(resourcePath);

                if(File.Exists(parentFolder + "/Assets/Plugins/IOS/Resource/TDS-Ultra-Info.plist")){
                    File.Copy(parentFolder + "/Assets/Plugins/IOS/Resource/TDS-Ultra-Info.plist", resourcePath + "/TDS-Ultra-Info.plist");
                }

                string fileName = "Unity-iPhone" + ".entitlements";
                string entitleFilePath = path + "/" + fileName;
                PlistDocument tempEntitlements = new PlistDocument();

                string key_associatedDomains = "com.apple.developer.associated-domains";
                string key_signinWithApple = "com.apple.developer.applesignin";

                if(!File.Exists(resourcePath + "/TDS-Ultra-Info.plist")){
                    Debug.Log("TDSSDK change Script compile Failed!");
                    return;
                }
                
                string isNeedAppleSignIn = GetValueFromPlist(resourcePath + "/TDS-Ultra-Info.plist","Apple_SignIn_Enable");
                string domain = GetValueFromPlist(resourcePath + "/TDS-Ultra-Info.plist","Game_Domain");
                if(isNeedAppleSignIn!=null && isNeedAppleSignIn.Equals("true"))
                {
                    var arr_signinWithApple = (tempEntitlements.root[key_signinWithApple] = new PlistElementArray()) as PlistElementArray;
                    arr_signinWithApple.values.Add(new PlistElementString("Default"));
                    proj.AddCapability (target, PBXCapabilityType.SignInWithApple,entitleFilePath);
                }
                if(domain!=null && domain.Length!=0)
                {
                    var arr_associateDomains = (tempEntitlements.root[key_associatedDomains] = new PlistElementArray()) as PlistElementArray;
                    arr_associateDomains.values.Add(new PlistElementString("applinks:"+domain));
                    proj.AddCapability(target, PBXCapabilityType.AssociatedDomains, entitleFilePath);
                }

                tempEntitlements.WriteToFile(entitleFilePath);
                
                File.WriteAllText(projPath, proj.WriteToString());

                Debug.Log("TDSSDK change Script compile Finish!");

                return;
            }

        }

        private static string GetValueFromPlist(string infoPlistPath,string key)
        {
            if(infoPlistPath==null)
            {
                return null;
            }
            Dictionary<string, object> dic = (Dictionary<string, object>)Plist.readPlist(infoPlistPath);
            foreach (var item in dic)
            {
                if(item.Key.Equals(key))
                {
                    return (string)item.Value;
                }
            }
            return null;
        }
    }
#endif
}
#endif
