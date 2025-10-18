// lr3.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>
#include <time.h>
#include <cmath>

using namespace std;

class Object {
protected:
    int smth;
public:
    Object() {
        smth = 0;
    }
    Object(int smth) {
        this->smth = smth;
    }
    Object(const Object& o) {
        this->smth = o.smth;
    }
    virtual ~Object() {

    }

    virtual void Setsmth(int smth) {
        this->smth = smth;
    }
    virtual int GetSmth() {
        return smth;
    }

private:

};

class ObjectX : public Object {
protected:
public:
    ObjectX() : Object() {
    }
    ObjectX(int smth) : Object(smth) {
        
    }
    ObjectX(const ObjectX& o) : Object(o) {
    
    }
    ~ObjectX() {

    }

};

class MyStorage {
protected:
    int size;
    int count;
    Object** objects;

public:
    MyStorage() {
        size = 1;
        count = 0;
        objects = new Object*[size];
        objects[0] = nullptr;

    }
    MyStorage(int size) {
        if (size <= 0) size = 1;
        this->size = size;
        count = 0;
        objects = new Object * [size]; 
        for (int i = 0; i < size; i++) {
            objects[i] = nullptr;
        }
    }
    MyStorage(const MyStorage& m) {
        this->size = m.size;
        this->count = m.count;
        objects = new Object * [size];
        for (int i = 0; i < size; i++) {
            if (m.objects[i] != nullptr) {
                objects[i] = new Object(*m.objects[i]);
            }
            else {
                objects[i] = nullptr;
            }
        }

    }
    ~MyStorage() {
        for (int i = 0; i < size; i++) {
            if (objects[i] != nullptr) {
                delete objects[i];
                objects[i] = nullptr;
            }
        }
        delete[] objects;
    }
   
    void AddStart(Object* newElem) { //добавление объектов (в начало, в конец)
        Object** tmp = new Object * [size + 1];
        tmp[0] = newElem;
        for (int i = 0; i < size; i++) {
            tmp[i + 1] = objects[i];
        }

        delete[] objects;
        objects = tmp;

        count = count + 1;
        size = size + 1;

    }
    void AddEnd(Object* newElem) {
        Object** tmp = new Object * [size + 1];
        for (int i = 0; i < size; i++) {
            tmp[i] = objects[i];
        }
        tmp[size] = newElem;

        delete[] objects;
        objects = tmp;

        count = count + 1;
        size = size + 1;
    }
    void AddByIndex(int index, Object* newElem) {
        if (index < 0) return;
        else {
            if (index < size) {

                Object** tmp = new Object * [size + 1];
                for (int i = 0; i < index; i++) {
                    tmp[i] = objects[i];
                }
                tmp[index] = newElem;
                for (int i = index; i < size; i++) {
                    tmp[i + 1] = objects[i];
                }

                delete[] objects;
                objects = tmp;
                count = count + 1;
                size = size + 1;
            }
            else {
                Object** tmp = new Object * [index + 1];
                for (int i = 0; i < size; i++) {
                    tmp[i] = objects[i];
                }
                for (int i = size; i < index; i++) {
                    tmp[i] = nullptr;
                }
                tmp[index] = newElem;
                delete[] objects;
                objects = tmp;
                count = count + 1;
                size = index + 1;
            }
        }
    }
    void AddMid(Object* newElem) {   //вставка объектов (в середину) by mat round
        AddByIndex(round(getSize() / 2), newElem);
    }
    void RemoveObject(int index) { //изъятие объектов (с удалением самого объекта)
        if (!IsEmpty(index)){
            delete objects[index];
            objects[index] = nullptr;
            count = count - 1;
        }
    }
    void Remove(int index) { //изъятие объектов ( без удалением самого объекта )
        if (!IsEmpty(index)) {
           // delete objects[index];
            objects[index] = nullptr;
            count = count - 1;
        }
    }
    Object* GetByIndex(int index) {  //получение очередного объекта из контейнера или объекта по индексу
        if (index >= 0 && index < size)
            return objects[index];
        return nullptr;
    }
    void SetObject(int index, Object* o) {
        if (index >= 0 && index < size) {
            if (objects[index] != nullptr) {
                delete objects[index];
                
            }else count = count + 1;
            objects[index] = o;
        }
    } 
    int getSize() {
        return size;
    }
    int getCount() {
        return count;
    }
    bool IsEmpty(int index) {
        if (index < 0 || index >= size) return true;
        else return objects[index] == nullptr;
        
    }

};



int main()
{

    srand(time(0));

    clock_t start = clock();
    MyStorage storage;
    int iterations = 100000;
    for (int i = 0; i < iterations; i++) {
        int action = rand() % 3;
        int x = rand() % iterations;
        switch (action) {
            //Действия должны случайным образом выбираться из списка: создание и вставка в случайное место 
            // контейнера нового объекта, удаление и уничтожение случайного объекта, запуск любого метода у случайного объекта из контейнера.
        case 0: // создание и вставка в случайное место 
        {
            Object* newObj;
            if (rand() % 2 == 0) {
                newObj = new Object(i);
            }
            else {
                newObj = new ObjectX(i);
            }
            
            int action1 = rand() % 4;
            if (action1 == 0) {
                storage.AddStart(newObj);
            }
            else if (action1 == 1) {
                storage.AddEnd(newObj);
            }
            else if (action1 == 2) {
                storage.AddByIndex(rand() % iterations, newObj);
            }
            else {
                storage.AddMid(newObj);
            }

            break;
        }
        case 1:  // удаление и уничтожение случайного объекта
        {
            if (rand() % 2 == 0) {
                storage.RemoveObject(rand() % iterations);
            }
            else {
                storage.Remove(rand() % iterations);
            }
            break;
        }
        case 2:  //запуск любого метода у случайного объекта
        {
            if (rand() % 2 == 0) {
                if (!storage.IsEmpty(x)) {
                        storage.GetByIndex(x)->GetSmth();
                    
                }
            }
            else {
                if (!storage.IsEmpty(x)) {
                     storage.GetByIndex(x)->Setsmth(rand() % iterations);
                    
                }
            }
            break;
        }
        }
    }
    clock_t end = clock();
    double duration = double(end - start) / CLOCKS_PER_SEC;
    cout << "Iterations: " << iterations << "  Time(sec): " << duration;
}

/*
Определение и реализация класса-контейнера разнообразных объектов (принадлежащих различным классам, имеющим общего предка), и написание программы, иллюстрирующей использование контейнера. 
Контейнер должен быть максимально универсальным, это не должен быть контейнер фигур, животных или любых других конкретных объектов; вы должны иметь возможность использовать его для хранения любых объектов, 
которые у вас в будущем могут появиться. Если умеете пользоваться шаблонами (дженериками и т.д.), используйте их; если не умеете – создавайте контейнер указателей на какой-то самый базовый класс, но этот класс должен быть максимально абстрактный.
Предназначение контейнера – хранить объекты, которые в него помещаются. Для создаваемого контейнера вы должны тщательно продумать, как вы будете его использовать для хранения объектов, какие типовые действия вы будете с контейнером производить.

•	Функции контейнера объектов
o	добавление объектов (в начало, в конец), вставка объектов (в середину)
o	изъятие объектов (с удалением самого объекта и без)
o	переход по объектам, если это применимо (текущий, предыдущий, последующий, проверка наличия)
o	получение очередного объекта из контейнера или объекта по индексу
o	поочередное обращение к каждому объекту контейнера; вызов функций, реализуемых всеми классами объектов контейнера
•	Функции основной программы: код, который в случайном порядке:
o	создаёт объекты
o	добавляет в контейнер
o	использует (для упрощённого вывода)
o	удаляет из контейнера

Контейнер должен представлять собой объект, создаваемый и используемый в основной программе. Контейнер должно вести себя (снаружи) или как массив, или как список (на выбор студента). 
Контейнер должно быть организовано внутри или как массив, или как список (на выбор студента). Студент должен понимать отличие двух предыдущих предложений. 
Основная программа должна демонстрировать использование основных функций контейнера и запускать цикл из 100, 1000 и 10000 случайных действий и подсчитывать время работы. 
Действия должны случайным образом выбираться из списка: создание и вставка в случайное место контейнера нового объекта, удаление и уничтожение случайного объекта, запуск любого метода у случайного объекта из контейнера.

Если контейнер представляет собой массив, то работа с ним в основной программе должна выглядеть примерно так:

// создаем контейнер
MyStorage storage(10);
// добавляем в него объекты
for (int i=0; i<storage.getCount(); i++)
    storage.setObject(i, new SomeObject());
// обращаемся поочередно ко всем
for (int i=0; i<storage.getCount(); i++)
    storage.getObject(i).someMethod();

Контейнер должен позволять добавлять, удалять объекты в случайной последовательности, корректно обрабатывать подсчет текущего количества объектов в контейнере 
(с учетом возможных «пустых мест» после удаления каких-то объектов) и динамически увеличивать свой размер, если добавляется больше объектов, чем было предусмотрено изначально.

Если вы затрудняетесь работать с одно- и двусвязными списками, не используйте их: цель работы в создании контейнера, а не в работе со списками. Если вы не работаете уверенно с template, не используйте их.

В контейнере не разрешается использование контейнеров STL, так как создание аналогичных классов и является основной задачей данной лабораторной работы.



*/