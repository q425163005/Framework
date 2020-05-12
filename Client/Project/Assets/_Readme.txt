//The3rd\Google.Protobuf\ByteString.cs修改,注释掉Mongodb的相关引用

使用到的库
https://github.com/y85171642/UnityWebSocket

ILRuntime
https://github.com/Ourpalm/ILRuntimeU3D/

DOTween 
http://dotween.demigiant.com/

1.对于程序Item的修改 移除热更程序中根据类名创建Item代码，所有Item实例化必须手动添加对象
2.移除Icon图标预设，新增 PublicItem替换。命名改为ItemP结尾，表示多个模块可以共用此Item。
公有ItemP必须放在BundleRes-UI-PublicItem目录。

