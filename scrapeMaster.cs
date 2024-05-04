using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;
using System.Linq;
using BDO_Info_Scraper;



public static class ScrapeMaster
{
    static HttpClient client = new HttpClient();
    public static Func<Task<bool>> ConfirmProceed;  // Delegate to prompt user confirmation from the UI thread
    static ConcurrentQueue<string> failureLogQueue = new ConcurrentQueue<string>(); 
    static ConcurrentQueue<string> successLogQueue = new ConcurrentQueue<string>();
    static CancellationTokenSource cts = new CancellationTokenSource();
    private static ManualResetEventSlim mre = new ManualResetEventSlim(true);  // Initially not paused


    public static void PauseScraping()
    {
        mre.Reset();  // Pauses the operation
    }

    public static void ResumeScraping()
    {
        mre.Set();  // Resumes the operation
    }

    private static ConcurrentQueue<string> logEntries = new ConcurrentQueue<string>();
    

    private static void FlushLogEntries()
    {
        using (var sw = new StreamWriter("failedfetch.txt", true))
        {
            string logEntry;
            while (logEntries.TryDequeue(out logEntry))
            {
                sw.WriteLine(logEntry);
            }
        }
    }


    public static async Task ScrapeData()
    {

        // Start the logging task
        var loggingTask = Task.Run(() => LogFailures(cts.Token));
        var successLoggingTask = Task.Run(() => LogSuccesses(cts.Token));


        // Define the range of item IDs and the size of each batch.
        int startId = 1;
        int endId = 820909;
        int batchSize = 100;
        int delayInMilliseconds = 200; // Delay of 200 milliseconds between requests

        for (int i = startId; i <= endId; i += batchSize)
        {
           
            var tasks = new List<Task<JObject>>();
            for (int j = i; j < i + batchSize && j <= endId; j++)
            {
                // Await an asynchronous pause check
                await PauseIfNeededAsync();
                tasks.Add(FetchItemData(j));
                await Task.Delay(delayInMilliseconds);  // Respect the API's rate limit
            }

            await Task.WhenAll(tasks);

            // Check if the user wants to proceed after each batch if the checkbox is checked
            if (ConfirmProceed != null && !await ConfirmProceed())
            {
                Console.WriteLine("Scraping paused by the user.");
                break;  // Stop scraping if the user decides not to proceed
            }
        }

        // Signaling cancellation after scraping is done
        cts.Cancel();
        await Task.WhenAll(loggingTask, successLoggingTask);  // Ensuring all logging tasks complete
    }

    private static Task PauseIfNeededAsync()
    {
        return Task.Run(() =>
        {
            mre.Wait();  // Wait will happen on a background thread
        });
    }

    public static async Task<JObject> FetchItemData(int itemId)
    {
        try
        {
            string url = $"https://apiv2.bdolytics.com/en/NA/db/recipe/{itemId}";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject jsonData = JObject.Parse(responseBody);

            // Save the fetched item data to a file
            await SaveItemToFile(jsonData, itemId);

            return jsonData;
        }
        catch (HttpRequestException httpEx)
        {
            LogFailure(itemId, $"HTTP Error: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            LogFailure(itemId, $"Exception: {ex.Message}");
        }
        return null;  // Return null if any exceptions are caught
    }

    private static async Task SaveItemToFile(JObject itemData, int itemId)
    {
        // Define the directory where files will be stored
        string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScrapedData");

        // Ensure that the directory exists
        Directory.CreateDirectory(directoryPath);

        // Create a path for the new file within the specified directory
        string filePath = Path.Combine(directoryPath, $"ItemData_{itemId}.json");

        // Use StreamWriter to create and write to the file
        using (StreamWriter file = File.CreateText(filePath))
        using (JsonTextWriter writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
        {
            itemData.WriteTo(writer);
        }

        Console.WriteLine($"Data for item ID {itemId} has been saved to {filePath}");
    }
    private static void LogFailure(int itemId, string reason)
    {
        string logMessage = $"Item ID {itemId}: {reason}";
        failureLogQueue.Enqueue(logMessage);
    }

    private static void LogFailures(CancellationToken token)
    {
        using (var sw = new StreamWriter("failedfetch.txt", true))
        {
            while (!token.IsCancellationRequested || !failureLogQueue.IsEmpty)
            {
                if (failureLogQueue.TryDequeue(out string logEntry))
                {
                    sw.WriteLine(logEntry);
                    sw.Flush();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }

    private static void LogSuccess(int itemId, string message)
    {
        string logMessage = $"Item ID {itemId}: {message}";
        successLogQueue.Enqueue(logMessage);
    }

    private static void LogSuccesses(CancellationToken token)
    {
        using (var sw = new StreamWriter("successlog.txt", true))
        {
            while (!token.IsCancellationRequested || !successLogQueue.IsEmpty)
            {
                if (successLogQueue.TryDequeue(out string itemId))
                {
                    sw.WriteLine($"Item ID {itemId}: Success");
                    sw.Flush();
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
    }

    private static void FlushLogEntries(string logFileName, ConcurrentQueue<string> logQueue)
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScrapedData", logFileName);
        List<string> entriesToWrite = new List<string>();

        while (logQueue.TryDequeue(out string logEntry))
        {
            entriesToWrite.Add(logEntry);
        }

        if (entriesToWrite.Count > 0)
        {
            try
            {
                File.AppendAllLines(filePath, entriesToWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
    }



    

   

    private static async Task WriteToJsonFile(JObject[] items, int startId, int endId)
    {
        string fileName = $"recipes_{startId}_to_{endId}.json";
        using (StreamWriter file = File.CreateText(fileName))
        using (JsonTextWriter writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
        {
            writer.WriteStartArray();
            foreach (var item in items)
            {
                if (item != null)
                {
                    item.WriteTo(writer);
                }
            }
            writer.WriteEndArray();
        }
        Console.WriteLine($"File {fileName} has been written.");
    }
}
