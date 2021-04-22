# TDSSDK-Unity快速开始

## 1. 登录TapTap开发者中心
请登录 [TapTap开发者中心](https://www.taptap.com/developer-center) 来创建应用或注册为开发者。

## 2. 下载 TapTap 应用
点击下载 [TapTap 应用](https://www.taptap.com/mobile)

## 3. 环境要求
- 安装Unity Unity 2018.3或更高版本
- iOS 10或更高版本
- Android 目标为API level 21或更高版本

## 4. 工程导入

**使用TDSSDK必须同时依赖于TapSDK，且版本号TDSSDK与TAPSDK强关联**  
[版本关系](#注意事项)

使用Unity Pacakge Manager

//在YourProjectPath/Packages/manifest.json中添加以下代码

```
"dependencies":{
        "com.tds.sdk.ultra":"https://github.com/xindong/TDSSDK_UPM.git#1.0.2",
        "com.tds.sdk":"https://github.com/xindong/TAPSDK_UPM.git#1.0.4",
    }
```

## 5. 配置TDSSDK

### Android 配置
#### 添加权限和Android10 配置
```
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />

//在application节点里面添加
<application
 	....
	android:requestLegacyExternalStorage="true"
>
```

#### 跳转配置
编辑Assets/Plugins/Android/AndroidManifest.xml文件,在Application Tag下添加以下代码。

```xml
    <activity
        android:name="com.taptap.sdk.TapTapActivity"
        android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
        android:exported="false"
        android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen" />
```


### iOS 配置

#### 配置TDS-Info.plist
在Assets/Plugins/iOS/Resource目录下创建TDS-Info.plist复制以下代码并且替换其中的ClientId、申请权限时的文案。

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>taptap</key>
    <dict>
        <key>client_id</key>
        <string>client-id</string>
    </dict>
    <key>NSPhotoLibraryUsageDescription</key>
    <string>App需要您的同意,才能访问相册</string>
    <key>NSCameraUsageDescription</key>
    <string>App需要您的同意,才能访问相机</string>
    <key>NSMicrophoneUsageDescription</key>
    <string>App需要您的同意,才能访问麦克风</string>
    <key>NSUserTrackingUsageDescription</key>
    <string>App需要追踪你的信息</string>
</dict>
</plist>
```

#### 配置TDS-Ultra-Info.plist
在Assets/Plugins/iOS/Resource目录下创建TDS-Ultra-Info.plist复制以下代码。

- Apple_SignIn_Enable:  true 开启Apple登陆 false 不开启
- Game_Domain:  配置applinks

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>Apple_SignIn_Enable</key>
    <string>false</string>
    <key>Game_Domain</key>
    <string></string>
</dict>
</plist>
```

## 6. 设置回调
#### API

```
public static void RegisterTDSSDKLoginResultCallback (TDSLoginResultCallback callback);
```

#### 示例代码

```
public class Login : MonoBehaviour, TDSLoginResultCallback{
    TDS.RegisterTDSSDKLoginResultCallback(this);

    public void OnLoginSuccess(TDSToken token) {
	Debug.Log("token:"+ token.accessToken);
    }

    public void OnLoginCancel() {
	Debug.Log("login cancel");
    }

    public void OnLoginError(TDSSDKError error) {
	Debug.Log("error:"+error.ToString());
    }
}
```

## 7. 初始化SDK
#### API

```
public static void Init (string clientID, bool regionType);
```

#### 示例代码

```
 TDS.Init("KFVWkkRJeb", false);//ture为国内，false为海外
```

## 8. 登录
**Android仅支持TapTap登录，iOS同时还支持游客登录和Apple登录**
### TapTap登录
#### API
```
public static void LoginbyTapTap (string[] permission);
```

#### 示例代码
```
 string[] permissions = { "public_profile" };//固定写法
 TDS.LoginbyTapTap(permissions);
```
### 游客登录
#### API
```
public static void LoginbyGuest ();
```

#### 示例代码
```
TDS.LoginbyGuest();
```
### Apple登录
#### API
```
public static void LoginbyApple ();
```
#### 示例代码

```
TDS.LoginbyApple();
```

## 9. 校验是否登录
#### API

```
public static void GetCurrentToken (Action<TDSToken> callback);
```
#### 示例代码

```
 TDS.GetCurrentToken((token) =>
{
    if (token == null)
    {
        //未登录，【调用登录】
    } else {
       //已经登录
    }
});
```

## 10. 登出
#### API
```
public static void Logout ();
```
#### 示例代码

```
TDS.Logout();
```

## 11.用户中心

#### API
```
public static void OpenUserCenter ();
```
#### 示例代码

```
TDS.OpenUserCenter();
```


## 服务端文档

## 登录验证

游戏服务端使用客户端获取的 token ，按照下面的方式获取用户信息。


`GET` `Mac Token` https://tds-account.intl.tapapis.com/api/v1/user/info

```
Request

client_id 游戏的 client_id

Response
{
	"success": true,
	"data": {
		"user_id": "4fcda21eda9d466eb52a5615a4193391",
		"name": "",
		"avatar": "",
		"gender": 0,
		"is_guest": true,
		"taptap_user_id": ""
	}
}

user_id 用户 id，类型为 string
name 用户昵称
avatar 头像
gender 性别
is_guest 是否为游客
taptap_user_id 用户的 TapTap 数字 ID 仅用于展示，游客时该值为空
```


### Mac Token 签算

注意：  
1.uri取除去域名以外的完整链接，例如：
请求地址
````
https://tds-account.intl.tapapis.com/api/v1/user/info?client_id=hn5RcJei2JxCYlS0
````
uri 应为
````
/api/v1/user/info?client_id=hn5RcJei2JxCYlS0
````

2.拼接待签名字符串时，每个字段后面都要跟上换行符

3.Header 上携带 Authorization，其内容的生成方式如下

```
//Shell 示例

#!/usr/bin/env bash

# 客户端 ID
CLIENT_ID="KFV9Pm9ojdmWkkRJeb"
# SDK 获取的  kid
KID="1/BeSVXhg1CAg9xHHlFFZc2Q6iHHxXnf8ysJMnXn9bXov7UHEOP9_J9iulUTU_T-jHcQzx-aWBN2YpNOwg_qyIazFVzjoTza9Ucf9sNOT-CNHNGAJA7rEQk-JwBscdyHvVlVHAXt14jtyczjYEETGJQFjIQ3JXJSJDH7QmKNfA7l2lVUgog2Fd4xbkibXkLE_sXGGUQxxm7U0E9HOko0ssgBKbeSKSjxnCgZV6ESYxk4JCrh0OzjmM43ft2T0e4M29TLXIsP5oNGbVxT0Aha9W6ngxieddId1_7p_kZtrAXzyP1L0cUjPc8bl689sB7dRmfrI9GTClgYHi1AbgShmfLg"
# SDK 获取的 mac_key
MAC_KEY="nFQFLzcvAXYyUZOo"

# 随机数，正式上线请替换
NONCE="abcdef"
# 当前时间戳
TS=$(date +%s)
# 请求方法
METHOD="GET"
# 请求地址 (带 query string)
REQUEST_URI="/api/v1/user/info?client_id=${CLIENT_ID}"
# 请求域名
REQUEST_HOST="tds-account.intl.tapapis.com"

MAC=$(printf "%s\n%s\n%s\n%s\n%s\n443\n\n" "${TS}" "${NONCE}" "${METHOD}" "${REQUEST_URI}" "${REQUEST_HOST}" | openssl dgst -binary -sha1 -hmac ${MAC_KEY} | base64)

AUTHORIZATION=$(printf 'MAC id="%s",ts="%s",nonce="%s",mac="%s"' "${KID}" "${TS}" "${NONCE}" "${MAC}")

curl -s -H"Authorization:${AUTHORIZATION}" "https://tds-account.intl.tapapis.com/api/v1/user/info?client_id=${CLIENT_ID}"

```

```
//POSTMAN Pre-request Script 示例
var sdk = require('postman-collection')
var acessToken = "1/BeSVXhg1CAg9xHHlFFZc2Q6iHHxXnf8ysJMnXn9bXov7UHEOP9_J9iulUTU_T-jHcQzx-aWBN2YpNOwg_qyIazFVzjoTza9Ucf9sNOT-CNHNGAJA7rEQk-JwBscdyHvVlVHAXt14jtyczjYEETGJQFjIQ3JXJSJDH7QmKNfA7l2lVUgog2Fd4xbkibXkLE_sXGGUQxxm7U0E9HOko0ssgBKbeSKSjxnCgZV6ESYxk4JCrh0OzjmM43ft2T0e4M29TLXIsP5oNGbVxT0Aha9W6ngxieddId1_7p_kZtrAXzyP1L0cUjPc8bl689sB7dRmfrI9GTClgYHi1AbgShmfLg"
var key = "nFQFLzcvAXYyUZOo"
var nonce = "3X0JE"
var ts = Date.now() /1000|0
var url = pm.request.url
var method = pm.request.method
var host = url.getHost()
var port = 443
var uri = "/api/v1/user/info?client_id=KFV9Pm9ojdmWkkRJeb"
var signBase = ts + "\n" + nonce + "\n" + method + "\n" + uri + "\n" + host + "\n" + port + "\n\n"
console.log(signBase)
var authorization = 
"MAC id=\"" + acessToken + "\",ts=\"" + ts + "\",nonce=\"" + nonce + "\",mac=\"" + CryptoJS.enc.Base64.stringify(CryptoJS.HmacSHA1(signBase, key)) + "\"";
pm.globals.set("authorization",authorization);
```

```
//C++ 示例
CMAKE

cmake_minimum_required(VERSION 3.17)
project(untitled)

set(CMAKE_CXX_STANDARD 14)

#设置头文件路径
set(INC_DIR /usr/local/include)

#设置链接库路径
set(LINK_DIR /usr/local/lib)

#引入头文件
include_directories(${INC_DIR})

#引入库文件
link_directories(${LINK_DIR})

add_executable(untitled main.cpp)

target_link_libraries(untitled libcrypto.a libssl.a pthread)


------


#include <iostream>
#include <openssl/hmac.h>
#include <string>

using namespace std;

static const std::string base64_chars =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        "abcdefghijklmnopqrstuvwxyz"
        "0123456789+/";

std::string base64_encode(unsigned char const* bytes_to_encode, unsigned int in_len) {
    std::string ret;
    int i = 0;
    int j = 0;
    unsigned char char_array_3[3];
    unsigned char char_array_4[4];

    while (in_len--) {
        char_array_3[i++] = *(bytes_to_encode++);
        if (i == 3) {
            char_array_4[0] = (char_array_3[0] & 0xfc) >> 2;
            char_array_4[1] = ((char_array_3[0] & 0x03) << 4) + ((char_array_3[1] & 0xf0) >> 4);
            char_array_4[2] = ((char_array_3[1] & 0x0f) << 2) + ((char_array_3[2] & 0xc0) >> 6);
            char_array_4[3] = char_array_3[2] & 0x3f;

            for(i = 0; (i <4) ; i++)
                ret += base64_chars[char_array_4[i]];
            i = 0;
        }
    }

    if (i)
    {
        for(j = i; j < 3; j++)
            char_array_3[j] = '\0';

        char_array_4[0] = (char_array_3[0] & 0xfc) >> 2;
        char_array_4[1] = ((char_array_3[0] & 0x03) << 4) + ((char_array_3[1] & 0xf0) >> 4);
        char_array_4[2] = ((char_array_3[1] & 0x0f) << 2) + ((char_array_3[2] & 0xc0) >> 6);
        char_array_4[3] = char_array_3[2] & 0x3f;

        for (j = 0; (j < i + 1); j++)
            ret += base64_chars[char_array_4[j]];

        while((i++ < 3))
            ret += '=';

    }

    return ret;

}


string buildSignature(string & baseString,string &macKey){

    unsigned char digest[EVP_MAX_MD_SIZE] = {'\0'};
    unsigned int digest_len = 0;
    HMAC(EVP_sha1(), macKey.c_str(), macKey.length(), (unsigned char*)baseString.c_str(), baseString.length(), digest, &digest_len);
    return base64_encode(digest, digest_len);

}

int main() {

    // 从客户端获取的 kid
    char kid[] = "1/BeSVXhg1CAg9xHHlFFZc2Q6iHHxXnf8ysJMnXn9bXov7UHEOP9_J9iulUTU_T-jHcQzx-aWBN2YpNOwg_qyIazFVzjoTza9Ucf9sNOT-CNHNGAJA7rEQk-JwBscdyHvVlVHAXt14jtyczjYEETGJQFjIQ3JXJSJDH7QmKNfA7l2lVUgog2Fd4xbkibXkLE_sXGGUQxxm7U0E9HOko0ssgBKbeSKSjxnCgZV6ESYxk4JCrh0OzjmM43ft2T0e4M29TLXIsP5oNGbVxT0Aha9W6ngxieddId1_7p_kZtrAXzyP1L0cUjPc8bl689sB7dRmfrI9GTClgYHi1AbgShmfLg"; // kid
    // 从客户端获取的 mac key
    string macKey = "nFQFLzcvAXYyUZOo"; // mac_key
    char nonce[] = "3X0JE"; // 随机字串，建议至少5位，必须每次随机生成
    time_t ts = time(NULL); // 当前时间戳，秒级
    string tsStr = to_string(ts);
    string signatureBaseString = "";
    signatureBaseString.append(tsStr);
    signatureBaseString.append("\n");
    signatureBaseString.append(nonce);
    signatureBaseString.append("\n");
    signatureBaseString.append("GET");
    signatureBaseString.append("\n");
    signatureBaseString.append("/api/v1/user/info?client_id=KFV9Pm9ojdmWkkRJeb");
    signatureBaseString.append("\n");
    signatureBaseString.append("tds-account.intl.tapapis.com");
    signatureBaseString.append("\n");
    signatureBaseString.append("443");
    signatureBaseString.append("\n\n");

    string mac = buildSignature(signatureBaseString, macKey);
    cout<<mac<<endl;

    string authorization = "";
    authorization.append("MAC id=\"").append(kid).append("\",");
    authorization.append("ts=\"").append(tsStr).append("\",");
    authorization.append("nonce=\"").append(nonce).append("\",");
    authorization.append("mac=\"").append(mac).append("\"");

    cout<<"Authorization: "<<authorization;

    return 0;
}

```


```
// PHP 语言示例

// 你要请求的资源地址
$url = 'https://tds-account.intl.tapapis.com/api/v1/user/info?client_id=KFV9Pm9ojdmWkkRJeb';

// 从客户端获取的 kid
$kid = '1/BeSVXhg1CAg9xHHlFFZc2Q6iHHxXnf8ysJMnXn9bXov7UHEOP9_J9iulUTU_T-jHcQzx-aWBN2YpNOwg_qyIazFVzjoTza9Ucf9sNOT-CNHNGAJA7rEQk-JwBscdyHvVlVHAXt14jtyczjYEETGJQFjIQ3JXJSJDH7QmKNfA7l2lVUgog2Fd4xbkibXkLE_sXGGUQxxm7U0E9HOko0ssgBKbeSKSjxnCgZV6ESYxk4JCrh0OzjmM43ft2T0e4M29TLXIsP5oNGbVxT0Aha9W6ngxieddId1_7p_kZtrAXzyP1L0cUjPc8bl689sB7dRmfrI9GTClgYHi1AbgShmfLg'; // kid
// 从客户端获取的 mac key
$mac_key = 'nFQFLzcvAXYyUZOo'; // mac_key
$nonce = 'aSefW'; // 随机字串，建议至少5位，必须每次随机生成
$ts = time(); // 当前时间戳，秒级

$signatureBaseArray = [];
$signatureBaseArray[] = $ts; // 当前时间戳，秒级
$signatureBaseArray[] = $nonce; // 随机字符串
$signatureBaseArray[] = 'GET'; // 请求方式, GET 或 POST
$signatureBaseArray[] = '/api/v1/user/info?client_id=KFV9Pm9ojdmWkkRJeb'; // uri
$signatureBaseArray[] = 'tds-account.intl.tapapis.com'; // 主机名
$signatureBaseArray[] = 443; // 端口 80 | 443
$signatureBaseArray[] = ""; // ext

$signatureBaseString = implode("\n", $signatureBaseArray) . "\n";

$mac = buildSignature($signatureBaseString, $mac_key);

$header = [
    "Authorization" => sprintf('MAC id="%s",ts="%d",nonce="%s",mac="%s"', $kid, $ts, $nonce, $mac)
];

$response = Requests::get($url, $header);

/**
 * 生成签名
 *
 * @param $signatureBaseString
 * @param $signatureSecret
 * @return string
 * @example buildSignature('abc', 'def') -> dYTuFEkwcs2NmuhQ4P8JBTgjD4w=
 */
function buildSignature($signatureBaseString, $signatureSecret) {
    return base64_encode ( hash_hmac ( 'sha1', $signatureBaseString, $signatureSecret, true ) );
}

```


## 注意事项

###  TapSDK 和 TDS 版本对应

|  TapSDK版本   | TDS版本  |
|  ----  | ----  |
| 1.0.3  | 1.0.0 |
| 1.0.4  | 1.0.1 |
| 1.0.4  | 1.0.2 |
| 1.1.0  | 1.1.0 |
| 1.1.2  | 1.1.2 |
