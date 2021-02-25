# TDSSDK

### 前提条件

* 安装Unity **Unity 2018.3**或更高版本

* IOS **10**或更高版本

* Android 目标为**API21**或更高版本

### 1.添加TDSSDK

使用TDSSDK必须同时依赖于TapSDK。

* 使用Unity Pacakge Manager

```json
//在YourProjectPath/Packages/manifest.json中添加以下代码
"dependencies":{
        "com.taptap.sdk":"https://github.com/xindong/TAPSDK_UPM.git#{version_name}",
        "com.tds.sdk":"https://github.com/xindong/TAPSDK_UPM.git#{version_name}",
    }
```

### 2.配置TDSSDK


#### 2.1 Android 配置

编辑Assets/Plugins/Android/AndroidManifest.xml文件,在Application Tag下添加以下代码。
```xml
    <activity
        android:name="com.taptap.sdk.TapTapActivity"
        android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
        android:exported="false"
        android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen" />
```

#### 2.2 IOS 配置

在Assets/Plugins/IOS/Resource目录下创建TDS-Info.plist文件,复制以下代码并且替换其中的ClientI、申请权限时的文案以及是否需要Apple登陆，applinks。

* taptap : 用于taptap登陆的ClientId
* apple-Sign-In : true 需要使用Apple登陆 false 不需要
* game-domain : applinks 


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
    
    <key>apple-Sign-In</key>
    <string>true</string>
    <key>game-domain</key>
    <string>www.xd.com</string>
</dict>
</plist>
```