//
// Created by Efe on 5.05.2026.
//

#ifndef ROUTEENGINE_SET_H
#define ROUTEENGINE_SET_H

#pragma once

class Set {
private:
    bool* items;
    //dinamik dizi *
    int capacity;

public:
    Set(int maxCapacity);
    ~Set();

    void Add(int id);
    bool Contains(int id) const;
    void Clear();
};

#endif //ROUTEENGINE_SET_H//okuma bitti buraya kadar
