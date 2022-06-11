using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Data;
using UnityEditor;
using UnityEngine;

public class DataTransformer : EditorWindow
{
    private static readonly string TablePath = $"{Application.dataPath}/Resources/Data/Excel/";
    private static readonly string SavePath = $"{Application.dataPath}/Resources/Data/";
    private static readonly string AssetPath = $"Assets/Resources/Data/";
    private static readonly string[] TableNames = Enum.GetNames(typeof(Define.Table));

    private static readonly string CsvFormat = ".csv";

    [MenuItem("Parser/LoadAllData")]
    private static void LoadAllData()
    {
        LoadData<StatusData>(Define.Table.Status,1);
        LoadData<BossData>(Define.Table.Boss, 1);
        LoadData<MonsterData>(Define.Table.Monster, 1);
        LoadData<UpgradeData>(Define.Table.Upgrade, 1);
        LoadData<WeaponData>(Define.Table.Weapon, 1);
        LoadData<ShopData>(Define.Table.Shop, 1);
        LoadData<StringData>(Define.Table.String, 1);
        LoadData<PathData>(Define.Table.Path, 1);
    }


    private static void LoadData<T>(Define.Table table = Define.Table.None, int fieldLine = 2)
        where T : ScriptableObject, ITableSetter
    {
        CreatePath();

        string tableName = TableNames[(int) table];

        StreamReader reader = new StreamReader($"{TablePath}{tableName}{CsvFormat}");

        for (int i = 0; i < fieldLine - 1; i++)
            reader.ReadLine();

        string[] fieldNames = reader.ReadLine().Split('|');

        while (!reader.EndOfStream)
        {
            string[] data = reader.ReadLine().Split('|');
            T sO = ScriptableObject.CreateInstance<T>();
            SetData(sO, data, fieldNames);
            
            AssetDatabase.DeleteAsset($"{AssetPath}{tableName}/{sO.GetID().ToString()}.asset");
            AssetDatabase.CreateAsset(sO, $"{AssetPath}{tableName}/{sO.GetID().ToString()}.asset");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void CreatePath()
    {
        if (!Directory.Exists(TablePath))
            Directory.CreateDirectory(TablePath);

        if (!Directory.Exists(SavePath))
            Directory.CreateDirectory(SavePath);

        foreach (var name in TableNames)
            if (!Directory.Exists($"{SavePath}{name}"))
                Directory.CreateDirectory($"{SavePath}{name}");
    }

    private static void SetData(UnityEngine.Object obj, string[] data, string[] fieldNames)
    {
        for (int i = 0; i < fieldNames.Length; i++)
        {
            FieldInfo info = obj.GetType().GetField(fieldNames[i],
                BindingFlags.Public |
                BindingFlags.Instance);

            if (info == null)
                throw new Exception($"Field Name: {fieldNames[i]} / Data: {data[i]}");

            info.SetValue(obj,
                info.FieldType.IsEnum
                    ? Enum.Parse(info.FieldType, data[i])
                    : Convert.ChangeType(data[i], info.FieldType));
        }
    }
}