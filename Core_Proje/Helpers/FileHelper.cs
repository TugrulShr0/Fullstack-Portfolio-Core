using System;
using System.IO;

namespace Core_Proje.Helpers
{
    public static class FileHelper
    {
        public static void DeleteFile(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return;

            // Varsayılan profil veya özel sistem resimlerini silmekten korur
            if (relativePath.Contains("default-avatar") || 
                relativePath.Contains("default-profile") || 
                relativePath.Contains("face1.jpg") || 
                relativePath.Contains("people.svg") || 
                relativePath.Contains("9c758b1a-b534-43c4-b006-ffaf797d7c44.jpeg"))
                return;

            try
            {
                string cleanPath = relativePath.TrimStart('/', '\\');
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", cleanPath);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }
            catch
            {
                // Dosya silme hatasında uygulamanın çökmesini engeller
            }
        }
    }
}
