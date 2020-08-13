using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace SpaceShip
{
    public static class DataController
    {
        public static void SaveUserData(string _fileName, List<Level> _levels)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "/" + _fileName + ".txt";
            FileStream stream = new FileStream(path, FileMode.Create);

            UserData data = new UserData(_levels);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static UserData LoadUserData(string _fileName)
        {
            string path = Application.persistentDataPath + "/" + _fileName + ".txt";
            Debug.Log($"Save PATH: {path}");


            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                UserData data = formatter.Deserialize(stream) as UserData;

                stream.Close();
                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found in " + path);
                return null;
            }
        }
    }
}