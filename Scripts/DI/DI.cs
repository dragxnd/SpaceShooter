using System;
using System.Collections.Generic;
using UnityEngine;

public static class DI
{

    public static T Resolve<T>()
    {
        return (T)((object)DI.Resolve(typeof(T)));
    }

    private static object AddCreate(Type type)
    {
        DI.m_instancesMap[type] = DI.m_placeholder;
        DI.m_instancesMap[type] = DI.CreateInstance(type);
        return DI.m_instancesMap[type];
    }

    private static object Resolve(Type type)
    {
        object obj;
        if (!DI.m_instancesMap.TryGetValue(type, out obj))
        {
            return DI.AddCreate(type);
        }
        if (DI.m_placeholder == obj)
        {
            throw new Exception("Cyclic dependency " + type);
        }
        return obj;
    }

    private static object CreateInstance(Type type)
    {
        if (type.IsSubclassOf(typeof(UnityEngine.Object)))
        {
            return Resources.FindObjectsOfTypeAll(type)[0];
        }
        return Activator.CreateInstance(type);
    }

    private static readonly Dictionary<Type, object> m_instancesMap = new Dictionary<Type, object>();
    private static readonly object m_placeholder = new object();

}

