#pragma once

struct Point {
    int X;
    int Y;

    // Constructor
    Point(int _x = 0, int _y = 0) { // Eğer parametre girilmezse varsayılan olarak (0,0) kabul eder.
        X = _x;
        Y = _y;
    }
};
// C++'ta fonksiyonlar yalnızca tek bir değer döndürebildiği için,
// X ve Y koordinatlarını paketleyip tek seferde döndürebilmek amacıyla bu yapıyı kullandık.