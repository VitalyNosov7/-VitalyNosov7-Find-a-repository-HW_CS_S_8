using System.Text;

namespace HW_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            if (args.Length < 3)
            {
                Console.WriteLine("Для поиска файла(ов) с искомым текстом укажите следующие пераметры(через пробел):\n" +
                                  "1. Имя каталога(где искать файл);" +
                                  "2. Расширение файла(например: txt);" +
                                  "3. Текст, который может содержаться в файле.");

                return;
            }

            string searchDirectory = args[0];
            string fileName = "*." + args[1];
            string searchText = args[2];

            if (!Directory.Exists(searchDirectory))
            {
                Console.WriteLine("Указанной директории не существует!");

                return;
            }

            try
            {
                SerachRecursive(searchDirectory, fileName, searchText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Что-то пошло не так: {ex.Message}");
            }
        }

        static void SerachRecursive(string dirNAme, string fileName, string searchText)
        {
            string[] matchingFiles = Directory.GetFiles(dirNAme, fileName);

            foreach (string file in matchingFiles)
            {
                using (var reader = new StreamReader(File.Open(file, FileMode.Open), Encoding.UTF8))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine();

                        if (line.ToLower().Contains(searchText.ToLower()))
                        {
                            Console.WriteLine($"Заданный для поиска текст - \"{line}\" найден в файле:");
                            Console.WriteLine(file);
                        }
                    }
                }
            }

            string[] subDirectories = Directory.GetDirectories(dirNAme);

            foreach (string subDirectory in subDirectories)
            {
                SerachRecursive(subDirectory, fileName, searchText);
            }
        }
    }
}