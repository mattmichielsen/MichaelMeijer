using MichaelMeijer;

var upcBarcodeClient = new HttpClient() { BaseAddress = new Uri("https://api.upcdatabase.org/") };
upcBarcodeClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("UPCBARCODE_APIKEY"));

var cache = new Dictionary<string, Product?>();
//var upc = "652682034127";
//var upc = "840006661771";
//var upc = "041771565459";
Console.Clear();
Console.WriteLine("Scan item");

while (true)
{
    try
    {
        var upc = Console.ReadLine();
        Console.Clear();
        if (!string.IsNullOrWhiteSpace(upc))
        {
            Product? product = null;
            if (cache.ContainsKey(upc))
            {
                product = cache[upc];
            }
            else
            {
                var result = await upcBarcodeClient.GetAsync($"product/{upc}");
                if (result.IsSuccessStatusCode)
                {
                    product = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(await result.Content.ReadAsStringAsync());
                    cache.Add(upc, product);
                }
                else if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    cache.Add(upc, null);
                }
                else
                {
                    Console.WriteLine($"Error getting product with UPC '{upc}': {await result.Content.ReadAsStringAsync()}");
                }
            }

            string? title;
            string? price;
            if (string.IsNullOrWhiteSpace(product?.Title))
            {
                title = "Unidentified product";
            }
            else
            {
                title = product.Title;
            }

            if (string.IsNullOrWhiteSpace(product?.Msrp) || product.Msrp.StartsWith("0"))
            {
                price = $"{Random.Shared.Next(1, 200)}.99";
            }
            else
            {
                price = product.Msrp;
            }

            Console.WriteLine($"{title}{Environment.NewLine}${price}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
}
