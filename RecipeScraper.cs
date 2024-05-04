using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Linq;
using System.Text.RegularExpressions;
using BDO_Info_Scraper;

public class RecipeScraper
{
    private readonly HttpClient client = new HttpClient();
    private readonly string baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScrapedRecipes");

    public RecipeScraper()
    {
        Directory.CreateDirectory(baseDirectory);
    }

    public async Task RunScraping()
    {
        // Assuming we are scraping IDs 1 to 646
        for (int i = 1; i <= 646; i++)
        {
            string url = $"https://bdocodex.com/tip.php?id=recipe--{i}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // Parse and save the content here
                    File.WriteAllText(Path.Combine(baseDirectory, $"Recipe_{i}.json"), content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to fetch or save data for recipe ID {i}: {ex.Message}");
                // Optionally, log these errors or handle them accordingly
            }
        }
    }

    public async Task<JObject> FetchRecipeData(int recipeId)
    {

        string url = $"https://bdocodex.com/tip.php?id=recipe--{recipeId}";
        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var data = ParseHtml(content);
            if (data != null)
            {
                string filePath = Path.Combine(baseDirectory, $"Recipe_{recipeId}.json");
                File.WriteAllText(filePath, data.ToString(Newtonsoft.Json.Formatting.Indented));
                return data;
            }
        }
        return null;
    }

    private JObject ParseHtml(string htmlContent)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);
        var node = doc.DocumentNode;

        // Basic information
        string title = node.SelectSingleNode("//div[@id='item_name']/b")?.InnerText;
        string id = node.SelectSingleNode("//div[contains(text(), 'ID:')]")?.InnerText.Replace("ID: ", "").Trim();
        string category = node.SelectSingleNode("//span[@class='category_text']")?.InnerText;
        string skill = node.SelectSingleNode("//span[@class='yellow_text']")?.InnerText;
        string skillLevel = node.SelectSingleNode("//span[contains(text(), 'Skill level:')]").InnerText.Split(':').Last().Trim();
        
    
        // Extract grade from the title's class attribute
        string titleClass = node.SelectSingleNode("//div[@id='item_name']")?.GetAttributeValue("class", "");
        int grade = ExtractGrade(titleClass);

        // Parsing ingredients
        var ingredientsNodes = node.SelectNodes("//div[contains(@class, 'iconset_wrapper_medium')]/a[contains(@class, 'qtooltip')]");
        var ingredients = ParseItems(ingredientsNodes);

        // Parsing products
        var productsNodes = node.SelectNodes("//tr[td/span[contains(text(), 'Crafting Result')]]/following-sibling::tr[1]//a[contains(@class, 'qtooltip')]");
        var products = ParseItems(productsNodes);

        return new JObject
        {
            ["id"] = id,
            ["title"] = title,
            ["category"] = category,
            ["skill"] = skill,
            ["skillLevel"] = skillLevel,
           
            ["grade"] = grade,
            ["ingredients"] = ingredients,
            ["products"] = products
            
        };
    }

    private JArray ParseItems(HtmlNodeCollection nodes)
    {
        JArray items = new JArray();
        if (nodes == null) return items;  // Early return if no nodes

        foreach (var node in nodes)
        {
            var id = node.GetAttributeValue("data-id", "").Replace("item--", "");

            var nameNode = node.SelectSingleNode("//div[@class='iconset_wrapper_medium']/a[@class='qtooltip item_grade_0'][contains(@href, '/item/')]");
            string name = nameNode != null ? nameNode.InnerText.Trim() : "Unknown";



            var quantityNode = node.SelectSingleNode("./following-sibling::div[contains(@class, 'quantity_small nowrap')]");
            var quantity = quantityNode != null ? quantityNode.InnerText.Trim() : "1";  // Default to 1 if not found

            var grade = ExtractGrade(node.GetAttributeValue("class", ""));

            items.Add(new JObject
            {
                ["id"] = id,
                ["name"] = name,
                ["quantity"] = quantity,
                ["grade"] = grade
            });
        }
        return items;
    }







    private int ExtractGrade(string classValue)
    {
        // Extract the grade based on the presence of a class like 'item_grade_1'
        Match match = Regex.Match(classValue, "item_grade_(\\d+)");
        return match.Success ? int.Parse(match.Groups[1].Value) : 0;
    }


}

