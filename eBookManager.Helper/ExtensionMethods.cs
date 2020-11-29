using eBookManager.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookManager.Helper
{
    //자주 사용되는 메서드들은 분리 해놓자.
    public static class ExtensionMethods
    {
        //ToInt를 추가하는 이유. Convert.ToInt32를 다 쓰기 귀찮아서
        public static int ToInt(this string value, int defaultInteger = 0)
        {
            // 문자열 -> 정수 변환
            try
            {
                if (int.TryParse(value, out int validInteger)) //Out 변수
                    return validInteger;
                else
                    return defaultInteger;
            }
            catch
            {
                return defaultInteger;
            }
        }

        public static double ToMegabytes(this long bytes)
        {
            //바이트 -> 메가바이트로 변환
            return (bytes > 0) ? (bytes / 1024f) / 1024f : bytes;
        }

        public static bool StorageSpaceExists(this List<StorageSpace> space, string nameValueToCheck, out int storageSpaceId)
        {
            //저장소 공간 확인
            bool exists = false;
            storageSpaceId = 0;

            if(space.Count() != 0)
            {
                int count = (from r in space where r.Name.Equals(nameValueToCheck) select r).Count();

                if (count > 0)
                    exists = true;

                storageSpaceId = (from r in space select r.ID).Max() + 1;
            }
            return exists;
        }

        public static void WriteToDataStore(this List<StorageSpace> value, string storagePath, bool appendToExistingFile = false)
        {
            //Json을 이용한 쓰기
            //데이터를 JSON으로 변환 후 파일에 기록함.
            JsonSerializer json = new JsonSerializer();
            json.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(storagePath, appendToExistingFile))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    json.Serialize(writer, value);
                }
            }
        }

        public static List<StorageSpace> ReadFromDataStore (this List<StorageSpace> value, string storagePath)
        {
            //저장한 데이터를 객체로 읽어오고 코드를 반환함
            JsonSerializer json  = new JsonSerializer();
            if (!File.Exists(storagePath)) //파일 확인
            {
                var newFile = File.Create(storagePath);
                newFile.Close();
            }
            using (StreamReader sr = new StreamReader(storagePath))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    var retVal = json.Deserialize<List<StorageSpace>>(reader);
                    if (retVal is null)
                        retVal = new List<StorageSpace>();
                    return retVal;
                }
            }
        }

    }
}
