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
        "com.tds.sdk.ultra":"https://github.com/xindong/TDSSDK_UPM.git#{version_name}",
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

##### 2.2.1 配置TDS-Info.plist

在Assets/Plugins/IOS/Resource目录下创建TDS-Info.plist复制以下代码并且替换其中的ClientId、申请权限时的文案。

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

##### 2.2.2 配置TDS-Ultra-Info.plist 

在Assets/Plugins/IOS/Resource目录下创建TDS-Ultra-Info.plist复制以下代码。

* Apple_SignIn_Enable:  true 开启Apple登陆 false 不开启
* Game_Domain:  配置applinks

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