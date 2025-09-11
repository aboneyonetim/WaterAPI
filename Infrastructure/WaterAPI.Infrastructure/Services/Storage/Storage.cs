using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Infrastructure.StaticService;

namespace WaterAPI.Infrastructure.Services.Storage
{
    public  class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);

        //protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool isFirst = true)
        //{
        //    string newFileName = await Task.Run<string>(async () =>
        //    {
        //        string extention = Path.GetExtension(fileName);
        //        string newFileName = string.Empty;
        //        if (isFirst)
        //        {
        //            string oldFileName = Path.GetFileNameWithoutExtension(fileName);
        //            newFileName = $"{NameOperation.CharacterRegulatory(oldFileName)}{extention}";
        //        }
        //        else
        //        {
        //            newFileName = fileName;
        //            int indexNo1 = newFileName.IndexOf("-");
        //            if (indexNo1 == -1)
        //            {

        //                newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extention}";
        //            }
        //            else
        //            {
        //                int lastIndex = 0;
        //                while (true)
        //                {
        //                    lastIndex = indexNo1;
        //                    indexNo1 = newFileName.IndexOf("_", indexNo1 + 1);
        //                    if (indexNo1 == -1)
        //                    {
        //                        indexNo1 = lastIndex;
        //                        break;
        //                    }
        //                }

        //                int indexNo2 = newFileName.IndexOf(".");
        //                string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

        //                if (int.TryParse(fileNo, out int _fileNo))
        //                {
        //                    _fileNo++;
        //                    newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
        //                                        .Insert(indexNo1 + 1, fileNo.ToString());
        //                }
        //                else
        //                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extention}";


        //            }
        //        }

        //        //if (File.Exists($"{path}\\{newFileName}"))
        //        if (hasFileMethod(pathOrContainerName, newFileName))
        //            return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);
        //        else
        //            return newFileName;
        //    });
        //    return newFileName;

        //}
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool isFirst = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName); // uzantı boş olabilir
                string newFileName = string.Empty;

                if (isFirst)
                {
                    string oldFileName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldFileName)}{extension}";
                }
                else
                {
                    newFileName = fileName;
                    int indexNo1 = newFileName.LastIndexOf("-"); // son '-' karakterini al

                    if (indexNo1 == -1)
                    {
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }
                    else
                    {
                        int indexNo2 = newFileName.IndexOf(".", indexNo1); // . varsa
                        if (indexNo2 == -1) indexNo2 = newFileName.Length; // yoksa string sonu

                        int length = indexNo2 - indexNo1 - 1;
                        if (length < 0) length = 0;

                        string fileNo = newFileName.Substring(indexNo1 + 1, length);
                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indexNo1 + 1, length)
                                                     .Insert(indexNo1 + 1, _fileNo.ToString());
                        }
                        else
                        {
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                        }
                    }
                }

                if (hasFileMethod(pathOrContainerName, newFileName))
                    return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);
                else
                    return newFileName;
            });

            return newFileName;
        }

    }
}
