using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookManager.Engine
{
    //문서를 지원하는 간단한 기능 제공
    //튜플이란? 하나이상의 값을 리턴 받을때
    // out은 async 를 적용받지 않는다.

    public class DocumentEngine
    {
        public (DateTime dateCreated, DateTime dateLastAccessed, string fileName, string fileExtension,long fileLength, bool error) 
            GetFileProperties(string filePath)
        {
            var returnTuple = (created: DateTime.MinValue, lastDataAccessed: DateTime.MinValue, name: "", ext: "", fileSize: 0L, error: false);
            try
            {
                FileInfo fi = new FileInfo(filePath);
                fi.Refresh();
                returnTuple = (fi.CreationTime, fi.LastAccessTime, fi.Name, fi.Extension, fi.Length, false);
            }
            catch
            {
                returnTuple.error = true;
            }
            return returnTuple;
        }
    }
}
