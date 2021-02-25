#if UNITY_EDITOR && UNITY_IOS
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using TDSEditor;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif
using UnityEngine;

namespace TDSSDK.Editor
{
     public class TDSIOSPostBuildProcessor : MonoBehaviour
    {
#if UNITY_IOS
        // 添加标签，unity导出工程后自动执行该函数
        [PostProcessBuild(1)]
        /* 
            2020-11-20 Jiang Jiahao
            该脚本中参数为DEMO参数，项目组根据实际参数修改
            导出工程后核对配置或依赖是否正确，根据需要修改脚本
        */
        public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)
        {
            
            if (BuildTarget == BuildTarget.iOS)
            {   
                // 获得工程路径
                string projPath = PBXProject.GetPBXProjectPath(path);
                UnityEditor.iOS.Xcode.PBXProject proj = new PBXProject();
                proj.ReadFromString(File.ReadAllText(projPath));

                // 2019.3以上有多个target
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
                
                // 添加资源文件，注意文件路径
                var resourcePath = Path.Combine(path, "TDSResource");

                // capabilities 
                string fileName = "Unity-iPhone" + ".entitlements";
                string entitleFilePath = path + "/" + fileName;
                PlistDocument tempEntitlements = new PlistDocument();

                string key_associatedDomains = "com.apple.developer.associated-domains";
                string key_signinWithApple = "com.apple.developer.applesignin";

                string isNeedAppleSignIn = GetValueFromPlist(resourcePath + "/TDS-Info.plist","apple-Sign-In");
                string domain = GetValueFromPlist(resourcePath + "/TDS-Info.plist","game-domain");
                if(isNeedAppleSignIn!=null && isNeedAppleSignIn.Equals("true"))
                {
                    var arr_signinWithApple = (tempEntitlements.root[key_signinWithApple] = new PlistElementArray()) as PlistElementArray;
                    arr_signinWithApple.values.Add(new PlistElementString("Default"));
                    // Sign In With Apple
                    proj.AddCapability (target, PBXCapabilityType.SignInWithApple,entitleFilePath);
                }
                if(domain!=null)
                {
                    var arr_associateDomains = (tempEntitlements.root[key_associatedDomains] = new PlistElementArray()) as PlistElementArray;
                    // www.xd.com 需要替换成游戏自己官网域名
                    arr_associateDomains.values.Add(new PlistElementString("applinks:"+domain));
                    proj.AddCapability(target, PBXCapabilityType.AssociatedDomains, entitleFilePath);
                }

                tempEntitlements.WriteToFile(entitleFilePath);
                
                // rewrite to file  
                File.WriteAllText(projPath, proj.WriteToString());

                Debug.Log("TDSSDK Pro change Script compile Finish!");

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
