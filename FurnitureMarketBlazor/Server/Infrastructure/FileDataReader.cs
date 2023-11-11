namespace FurnitureMarketBlazor.Server.Infrastructure
{
    public static class FileDataReader
    {
        public static readonly string[] Data = ReadDataFromFile();

        private static string[] ReadDataFromFile()
        {
            string filePath = "C:\\Users\\bous0\\OneDrive\\Desktop\\API.txt";

            return File.ReadAllLines(filePath);
        }
    }
}
