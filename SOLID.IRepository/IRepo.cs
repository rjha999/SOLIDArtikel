using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID.IRepository
{
    //Common Repository Interface is created to achieve 'Interface Segregation Principle'
    public interface IRepo<T>  
    {
        bool AddList(List<T> listData);

        T Add(T data);

        T Get(string name);
    }
}
