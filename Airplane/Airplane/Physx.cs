using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Microsoft.Xna.Framework;

namespace Airplane
{
    class Physx:  IGameDictionary
    {
        //Храним ссылку на объект и результирующий вектор постоянно действующих сил
        Dictionary<PositionedObject, Vector2> objectslist_ = new Dictionary<PositionedObject, Vector2>();

        //Тут можно было бы хранить различные физические константы
        //Можно запилить динамическую генерацию полей

        public void addImpulse(PositionedObject obj, Vector2 impulse)
        {
            //Единичный импульс, добавляется к скорости объекта
            obj.Speed += impulse;
        }

        public void addForce(PositionedObject obj, Vector2 force)
        {
            //Добавляем/изменяем постоянную силу, действующую на объект
            if (objectslist_.ContainsKey(obj) == false)
            {
                objectslist_[obj] = new Vector2(force.X, force.Y);
            }
            else
            {
                objectslist_[obj] += force;
            }
        }

        public void setForce(PositionedObject obj, Vector2 vector)
        {
            //Возможность напрямую установить значение постоянной силы
            //Может пригодиться т.к. добавление и удаление сил производится математически
            //И в случае возникновения остатка/математической ошибки вектор можно обнулить
            //Бонус - телепортация
            //Кстати, общение через каментарии в коде заставляет его читать >_<
            objectslist_[obj] = vector;
        }

        public void doPhysix()
        {
            //Считаем физику
            foreach (KeyValuePair<PositionedObject, Vector2> obj in objectslist_)
            {
                obj.Key.Speed += obj.Value;
            }
        }

        public double Count()
        {
            return objectslist_.Count();
        }

        public void removeObject(PositionedObject obj)
        {
            //Удаляем объект и все силы, связанные с ним
            if (objectslist_.ContainsKey(obj))
            {
                objectslist_.Remove(obj);
            }
        }

        public void RemoveByKey(GameObject key)
        {
            removeObject((PositionedObject)key);
        }

        public void AddKeyVal(GameObject obj, object val)
        {
            throw new NotSupportedException();
        }


    }
}
