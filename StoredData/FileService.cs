using System.IO;

namespace Notepad.Functions;

public class FileService
{
    
private string _filePath;

public string Downloads
{
    get => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
    }

public string Filepath
{
  
    get => _filePath;
   set => _filePath = value;
}
    

  

/// <summary>
/// Since File Name can be obtained from file path no need for redudent data being intlized. 
/// </summary>
public string FileName => string.IsNullOrEmpty(_filePath) ? "Untitled" : Path.GetFileName(_filePath);


public string FileType => string.IsNullOrEmpty(_filePath) ? "Text Document" : Path.GetExtension(_filePath);


public string Downlaods
{
    get => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
}


public bool HasFile => !string.IsNullOrEmpty(_filePath); 


}


    


