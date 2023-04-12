
using System.Text;
using LilaChecker;

var checkedItems = new List<CheckedItem>();
checkedItems.Add(new CheckedItem { Url = "http://lila-11.ru/" });
checkedItems.Add(new CheckedItem { Url = "http://lila-11.ru/style.css" });
checkedItems.Add(new CheckedItem { Url = "http://lila-11.ru/script.js" });
checkedItems.Add(new CheckedItem { Url = "http://lila-11.ru/faq.html" });
checkedItems.Add(new CheckedItem { Url = "http://lila-11.ru/game.html" });
checkedItems.Add(new CheckedItem { Url = "http://lila-11.ru/script2.js" });
checkedItems.Add(new CheckedItem { Url = "http://lila-11.ru/key.html" });
checkedItems.Add(new CheckedItem { Url = "http://lila-11.ru/help.html" });
using var client = new HttpClient();
client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0)");


while (true)
{
    foreach (var item in checkedItems)
    {
        var currentValue = await client.GetStringAsync(item.Url);
        if (string.IsNullOrEmpty(item.LastValue))
        {
            item.LastValue = currentValue;
            Console.WriteLine($"[{DateTime.Now}] {item.Url} initial download");
            continue;
        }

        if (currentValue == item.LastValue)
        {
            Console.WriteLine($"[{DateTime.Now}] {item.Url} is OK");
        }
        else
        {
            var message = $"[{DateTime.Now}] {item.Url} is CHANGED from {item.LastValue} to {currentValue}";
            Console.WriteLine(message);
            Log(message);
        }
        item.LastValue = currentValue;
    }
    Thread.Sleep(60000);
}

static void Log(string message)
{
    var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
    using StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
    sw.WriteLine(message);
}