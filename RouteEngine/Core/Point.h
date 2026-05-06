
#pragma once

// Sadece veri taşıyan basit bir yapı (struct)
struct Point {
    int X;
    int Y;


    // Constructor:Nokta oluşturulurken X ve Y değerlerini başlangıçta atamamızı sağlar.
    Point(int _x = 0, int _y = 0) {// Eğer değer verilmezse varsayılan olarak (0,0) kabul eder.
        X = _x;
        Y = _y;
    }
};//aynı anda fonk sadece tek sey dondurebilir hem x hem y alabilmek icin
//kolaylik olsun diye point kullandık