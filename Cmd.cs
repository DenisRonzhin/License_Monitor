using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace license_viewer
{
    public class Cmd
    {
        public Encoding win1251 {get;set;}
        public List<string> licenseList {get;set;}
        string folderLicense;

        public Cmd(int serchType)
        {
            //кодировки доступные в netFrameWork
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            win1251 = Encoding.GetEncoding(1251);

            using (StreamWriter sw = new StreamWriter("log.txt", false, win1251))
            {
                  sw.WriteLine("License list:");
            }
            
            // serchType = 1 Поиск лицензий на компьютере
            // serchType = 2 Поиск лицензий в каталоге монитора \license
            
            switch (serchType)
            {
               case 1: folderLicense = ""; break;
               case 2: folderLicense = " --path " + Directory.GetCurrentDirectory() + "\\License\\"; break;
            }    
  
            //Console.WriteLine(folderLicense);

        }
        
        //Метод возвращает результат вывополнения команды CMD в виде строки
        //Входной параметр <command> - команда, которую необходимо выполнить
        string RunCommand(List<string> commandList)
        {


            string result = "";

            Process cmdProcess = new Process();

            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.RedirectStandardInput = true;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.StandardInputEncoding = win1251;
            cmdProcess.StartInfo.StandardOutputEncoding = win1251;


            cmdProcess.Start();

            foreach (var command in commandList)
            {
                cmdProcess.StandardInput.WriteLine(command);  
            }

            cmdProcess.StandardInput.Flush();
            cmdProcess.StandardInput.Close();

            result = cmdProcess.StandardOutput.ReadToEnd();

          
            // String line = result;
            //    Encoding cp866 = Encoding.GetEncoding(866);
            //    Encoding win1251 = Encoding.GetEncoding(1251);
            //    Byte[] cp866Bytes = cp866.GetBytes(result);
            //    result = win1251.GetString(cp866Bytes);
            //   Console.WriteLine(result);
          
           return result;
        }


        //Метод для получения списка установленных лицензий. 
        //Для получения списка лицензий выполняется CMD команда ring license list
        public int GetLicenseList()
        {
            List<string> _licenseList = new List<string>();
            List<string>  commandList = new List<string>();

            //ring license list [--path <хранилище>]
            //path – указывает путь к хранилищу конфигурации, если оно отличается от пути по умолчанию.
            
            commandList.Add($"ring license list{folderLicense}");

            //onsole.WriteLine($"ring license list{folderLicense}");
           
            string licenseStringList = RunCommand(commandList);

            Regex regex = new Regex(@"\d{15}-\d{10}");
            MatchCollection matches = regex.Matches(licenseStringList);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                   _licenseList.Add(match.Value);
                }
                   
            }
        
            licenseList = _licenseList;

            return licenseList.Count;
        }

        public void ShowLicense()
        {
            
            using (StreamWriter sw = new StreamWriter("log.txt", false, win1251))
            {
        
                
                foreach (var item in licenseList)
                {
                    sw.WriteLine(item);
                }
            }


        }

        public string GetAllLicenseInfo()
        {
            List<string> commanList = new List<string>();
            //commanList.Add($"chcp 866 && cmd");
   
            foreach (var License in licenseList)
            {
                commanList.Add($"ring license info --name {License}{folderLicense}");
                commanList.Add($"ring license validate --name {License}{folderLicense}");
            }

            string licenseInfo = RunCommand(commanList);
           
            using (StreamWriter sw = new StreamWriter("log.txt", true, win1251))
            {    
                sw.WriteLine("Detailed information on licenses:");
                sw.WriteLine(licenseInfo);
            }

            return licenseInfo;
        }

        public void OpenFile()
        {
            Process txt = new System.Diagnostics.Process();
            txt.StartInfo.FileName = "notepad.exe";
            txt.StartInfo.Arguments = @"log.txt";
            txt.Start();
        }


    }    

        
}
