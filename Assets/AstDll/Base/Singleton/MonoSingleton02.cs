using System;
using System.Reflection;
using UnityEngine;

public class MonoSingleton02<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _mutex = new object();

    public static T Instance
    {
        get
        {


            lock (_mutex)
            {
                if (_instance == null)
                {
                    var founds = FindObjectsOfType(typeof(T));
                    if (founds.Length > 1)
                    {
                        Debug.LogError("[Singleton] Singlton '" + typeof(T) +
                            "' should never be more than 1!");
                        return null;
                    }
                    else if (founds.Length > 0)
                    {
                        _instance = (T)founds[0];

                        // 不加这句会报错：DontDestroyOnLoad only works for root GameObjects or components on root GameObjects.
                        if (_instance.transform.parent == null) // 判断是否是根节点，
                            DontDestroyOnLoad(_instance.gameObject);
                    }
                    else
                    {
                        //Debug.LogError($"i am (Singleton) {typeof(T).ToString()}");
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(Singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }

                    AssignGuidField(_instance,$"{typeof(T).ToString()}-{++number}");
                }

                return _instance;
            }
        }
    }


    static int number = 0;


    private static void AssignGuidField(T instance, string idValue)
    {
        if (instance == null)
        {
            Debug.LogWarning("单例组件实例为空，跳过id字段赋值");
            return;
        }

        if (string.IsNullOrEmpty(idValue))
        {
            Debug.LogWarning("要赋值的id值为空字符串，跳过赋值");
            return;
        }

        // 获取id字段（包含public/非public、实例字段）
        FieldInfo idField = typeof(T).GetField("m_Guid",
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic);

        // 第一步：检查字段是否存在
        if (idField == null)
        {
            Debug.LogWarning($"{typeof(T).Name} 没有名为'm_Guid'的实例字段，跳过赋值");
            return;
        }

        // 第二步：关键！检查字段类型是否为string
        if (idField.FieldType != typeof(string))
        {
            Debug.LogWarning($"{typeof(T).Name} 的'm_Guid'字段不是字符串类型，实际类型：{idField.FieldType.Name}，跳过赋值");
            return;
        }

        // 第三步：为string类型的id字段赋值
        try
        {
            idField.SetValue(instance, idValue);
            Debug.Log($"成功为{typeof(T).Name}的string类型m_Guid字段赋值：{idValue}");
        }
        catch (FieldAccessException ex)
        {
            Debug.LogError($"访问id字段失败（可能是私有字段无访问权限）：{ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"为string类型id字段赋值时出错：{ex.Message}");
        }
    }


}
