using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ContactsList.ImageUploader
{
    public static class ImageUploader
    {
        // Hata Numara Kontrolü
        // 0 => Dosya Bulunamadı
        // 1 => Dosya zaten var
        // 2 => Uzantı desteklenmemektedir.
        public static string UploadImage(string serverPath, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var uniqueName = Guid.NewGuid();

                serverPath = serverPath.Replace("~", string.Empty);

                var fileArray = file.FileName.Split('.');
                string extension = fileArray[fileArray.Length - 1].ToLower();


                var fileName = uniqueName + "." + extension;

                if (extension == "jpg" || extension == "png" || extension == "gif" || extension == "jpeg")
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {
                        return "1";
                    }
                    else
                    {
                        var filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }
                }
                else
                {
                    return "2";
                }

            }
            return "0";

        }
    }
}