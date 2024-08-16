namespace Book_Store_MVC.FileUpload
{
    public static class UploadFile
    {


        public static string Upload(IFormFile file, string FolderName)
        {

            // define Path 

            string Folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", FolderName);


            // git file name 

           
          string FileName = $"{Guid.NewGuid()}{file.FileName}";
           // string FileName = file.FileName;

           //file path 
            string Filpath = Path.Combine(Folderpath, FileName);

            // 4 -open stream  

            using var FileStream = new FileStream(Filpath, FileMode.Create);
            file.CopyTo(FileStream);

            // 5- return file name 

            return FileName;

        }







        public static void DeleteFile(string file, string FolderName)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, file);


            if (File.Exists(filepath))
            {
                File.Delete(filepath);

            }



        }
    }
}
