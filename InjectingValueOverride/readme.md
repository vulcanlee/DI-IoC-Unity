# 摘要說明

這裡的範例程式碼，將會體驗在這個練習中，當要進行手動解析注入物件的時候，可以選擇使用不同的物件作為注入依據。在在呼叫 Resolve 方法的時候，使用 ParameterOverride , PropertyOverride 來覆寫我們要注入的常數。

在這個目錄下共有兩個目錄

## Starter

這裡將是要大家進行練習前置專案，請各位從這個專案開始進行練習

* 在這裡，ConsoleMessage 類別，實作了 介面 IMessage
* 在 ConsoleMessage 類別內，成員
  * 屬性 Cost(double) : 將會透過屬性注入的方式來取得
  * 屬性(欄位) Name(字串) , Age(整數) ： 要透過建構式注入
  * 屬性 _AnotherClass(IMyInterface介面) : 這個欄位值將會由建構式來注入來設定
  * 屬性 _MethodInjection(IMyInterface介面) : 這個欄位值將會由方法注入方式來設定

## Solution

這個專案將要呈現，當我們在宣告型別定義的時候，指定了要注入的常數，不過，當進行解析的時候，想要覆寫這些常識的 最後練習結果

* 在這裡，我們將會在呼叫 Resolve 方法的時候，使用 ParameterOverride , PropertyOverride 來覆寫我們要注入的常數
