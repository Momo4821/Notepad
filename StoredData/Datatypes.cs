using System.IO;

namespace Notepad.Functions;

public class Datatypes
{
    
 private string Filename;
 
 private string Filetype;
 
 private string FilePath;
 
 
 private string Downloads = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
 
 
 
 public string _Downloads
 {
     get => Downloads;
     
 }
 
 
 public string _Filename
 {
  get => Filename;
  
  set 
  {
    Filename = value;
  }
  }
  
 
  public string  _Filetype
 {
     
     get =>  Filetype;
     
     set
     {
         Filetype = value;
         }
         
       
     
     
 }
  
 public string _Filepath
 {
     get => FilePath;
     set => FilePath = value;
 }
}
    
    


