using System;

namespace license_viewer
{
    class Program
    {
        static void Main(string[] args)
        {
     
            Console.WriteLine("Для сбора информации по лицензиям на компьютере должно быть установлено:");
            Console.WriteLine("1. Java sdk (Используется для работы утилиты ring). После установки нужно добавить переменную среды JAVA_HOME");
            Console.WriteLine("2. .Net framework (Используется для работы монитора)");
            Console.WriteLine("3. 1c_enterprise_license_tools (Устанавливаем после java)");
            Console.WriteLine("*********************************************************");

            Console.WriteLine("Укажите вариант поиска лицензий (цифры 1-2):");
            Console.WriteLine("1. Поиск активированных лицензий:");
            Console.WriteLine("2. Проверка лицензий из каталога монитора (License)");
            
            string serchTypeSTR = Console.ReadLine();

            int serchType = 1;

            try
            {
                serchType = Int32.Parse(serchTypeSTR);
            }
            catch         
            {
                serchType = 1;
                Console.WriteLine("Некорректная команда, по умолчанию будет запущена 1 команда");
            }


            switch (serchType)
            {
                case 1:{Console.WriteLine("Поиск активированных лицензий");} break;
                case 2:{Console.WriteLine("Поиск лицензий в каталоге монитора license");} break;
            }

            Cmd MyCmd = new Cmd(serchType);

            
            Console.WriteLine($"Результат обработки будет выведен в файл: log.txt");
            
            
            int licenseCount = MyCmd.GetLicenseList();
         
            if (licenseCount>0) 
            {
                
                Console.WriteLine($"Найдено файлов с лицензиями: {licenseCount}");
                Console.WriteLine($"Идет сбор информации по лицензиям. Это может занять некоторое время.");
               
                MyCmd.ShowLicense();    
                string licenseInfo = MyCmd.GetAllLicenseInfo();
                Console.WriteLine("Информация по лицензиям записана в файл: log.txt");

            } else Console.WriteLine("Не найдено установленных лицензий, проверьте пункты 1-3");
          
            MyCmd.OpenFile();
            Console.ReadKey();
           
        }
    }
}
