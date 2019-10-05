using System;
using System.IO;
using System.Threading.Tasks;

namespace UnitTestGroceryStoreAPI
{
    internal class TestFileReader
    {
        private static string FOLDER = "TestFiles";

        public static async Task<string> ReadFileAsync(string fileName)
        {
            var file = Path.Combine(FOLDER, fileName);
            // Get the absolute path to the JSON file
            file = Path.IsPathRooted(file)
                ? file
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), file);

            if (!File.Exists(file))
            {
                throw new ArgumentException($"Could not find file at path: {file}");
            }

            using (var reader = File.OpenText(file))
            {
                var fileText = await reader.ReadToEndAsync();
                // Do something with fileText...
                return fileText;
            }
        }
    }
}