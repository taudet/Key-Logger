using (StreamWriter sw = new StreamWriter("keylog.txt", true))
{
    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    while (keyInfo.Key != ConsoleKey.Escape)
    {
        sw.Write(keyInfo.KeyChar);
        sw.Flush();
        keyInfo = Console.ReadKey();
    }
}