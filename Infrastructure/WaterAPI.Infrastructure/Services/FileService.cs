using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterAPI.Application;  
using WaterAPI.Infrastructure.StaticService;

namespace WaterAPI.Infrastructure.Services
{
    public class FileService 
    {
            readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write,
                FileShare.None, 1024 * 1024, false);

                //await fileStream.CopyToAsync(fileStream);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;

            }
            catch (Exception )
            {
                // todo log
                throw ;
            }

        }



        async Task<string> FileRenameAsync(string path, string fileName, bool isFirst = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extention = Path.GetExtension(fileName);

                string newFileName = string.Empty;
                if (isFirst)
                {
                    string oldFileName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldFileName)}{extention}";
                }
                else
                {
                    newFileName = fileName;
                    int indexNo1 = newFileName.IndexOf("-");
                    if (indexNo1 == -1)
                    {

                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extention}";
                    }
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = indexNo1;
                            indexNo1 = newFileName.IndexOf("_", indexNo1 + 1);
                            if (indexNo1 == -1)
                            {
                                indexNo1 = lastIndex;
                                break;
                            }
                        }

                        int indexNo2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);
                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
                            .Insert(indexNo1 + 1, fileNo.ToString());
                        }
                        else
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extention}";


                    }
                }

                if (File.Exists($"{path}\\{newFileName}"))
                    return await FileRenameAsync(path, newFileName, false);
                else
                    return newFileName;
            });
            return newFileName;

        }

        async Task<string> FileRenameAsync(string path, string fileName)
        {
            // 1. Dosya adını "isim" ve "uzantı" olarak ayır.
            string extension = Path.GetExtension(fileName);
            string oldFileName = Path.GetFileNameWithoutExtension(fileName);

            // 2. Dosya adını URL ve dosya sistemi için güvenli hale getir.
            string regulatedFileName = NameOperation.CharacterRegulatory(oldFileName);
            string newFileName = $"{regulatedFileName}{extension}";

            // 3. Eğer bu isimde bir dosya yoksa, doğrudan bu ismi döndür.
            if (!File.Exists(Path.Combine(path, newFileName)))
            {
                return newFileName;
            }

            // 4. Eğer dosya varsa, sonuna "-2", "-3", ... ekleyerek uygun bir isim bul.
            int counter = 2;
            while (File.Exists(Path.Combine(path, newFileName)))
            {
                newFileName = $"{regulatedFileName}-{counter}{extension}";
                counter++;
            }

            return newFileName;
            // Not: Bu metodun artık "async" olmasına gerek yok çünkü içinde await edilen bir işlem kalmadı.
            // İmzasını "string FileRenameAsync(...)" olarak değiştirebilirsiniz, ancak async kalması da sorun yaratmaz.
        }



        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,path);//https://localhost:7023/api/ + path

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files) 
            {
               string fileNewName = await FileRenameAsync(uploadPath,file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{path}\\{fileNewName}"));
                results.Add(result);
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;

            return null; //şimdilik
            //todo Eğer ki yukarıdaki if geçerli değilse burada dosyaların sunucuda yüklenirken
            //hata alındığına dair uyarıcı bir exception oluşturulup fırlatılması gerekiyor
        }
    }
}
