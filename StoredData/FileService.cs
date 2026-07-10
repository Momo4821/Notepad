using System.IO;

namespace Notepad.Functions;

public class FileService
{
    
public string FilePath;




    

  

/// <summary>
/// Since File Name can be obtained from file path no need for redudent data being intlized. 
/// </summary>
public string FileName => string.IsNullOrEmpty(FilePath) ? "Untitled" : Path.GetFileName(FilePath);


public string FileType => string.IsNullOrEmpty(FilePath) ? "Text Document" : Path.GetExtension(FilePath);


public string Downloads => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";


public bool HasFile => !string.IsNullOrEmpty(FilePath); 


}


    


