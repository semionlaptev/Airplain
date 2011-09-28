using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Airplane
{

    public class Force
    {
        Vector2 force_;

        public Force()
        {
            force_ = Vector2.Zero;
        }

        public Force(Vector2 force)
        {
            force_ = force;
        }

        public Vector2 Value
        {
            get { return force_; }
            set { force_ = value; }
        }
    }

    class Physx:  IGameDictionary
    {
        //Храним ссылку на объект и результирующий вектор постоянно действующих сил
        Dictionary<PositionedObject, List<Force>> objectslist_ = new Dictionary<PositionedObject, List<Force>>();

        //Тут можно было бы хранить различные физические константы
        //Можно запилить динамическую генерацию полей

        public void addImpulse(PositionedObject obj, Vector2 impulse)
        {
            //Единичный импульс, добавляется к скорости объекта
            obj.Speed += impulse;
        }

        public void addForce(PositionedObject obj, Force force)
        {
            //Добавляем/изменяем постоянную силу, действующую на объект
            if (objectslist_.ContainsKey(obj) == false)
            {
                objectslist_[obj] = new List<Force>() { force };
            }
            else
            {
                objectslist_[obj].Add(force);
            }
        }

        /*public void setForce(PositionedObject obj, Vector2 vector)
        {
            //Возможность напрямую установить значение постоянной силы
            //Может пригодиться т.к. добавление и удаление сил производится математически
            //И в случае возникновения остатка/математической ошибки вектор можно обнулить
            //Бонус - телепортация
            //Кстати, общение через каментарии в коде заставляет его читать >_<
            objectslist_[obj] = vector;
        }*/

        public IGameList GetObjectForces(PositionedObject obj)
        {
            if (objectslist_.ContainsKey(obj))
            {
                return (IGameList)objectslist_[obj];
            }
            else
            {
                throw new Exception("No object found.");
            }
        }

        public void doPhysix()
        {
            //Считаем физику
            foreach (KeyValuePair<PositionedObject, List<Force>> obj in objectslist_)
            {
                foreach(Force force in obj.Value)
                    obj.Key.Speed += force.Value;
            }
        }

        public long Count()
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
