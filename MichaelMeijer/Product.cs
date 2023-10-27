namespace MichaelMeijer
{
    internal class Product
    {
        public string? Success { get; set; }

        public string? Barcode { get; set; }

        public string? Title { get; set; }

        public string? Alias { get; set; }

        public string? Description { get; set; }

        public string? Brand { get; set; }

        public string? Manufacturer { get; set; }

        /// <summary>
        /// Manufacturer part number
        /// </summary>
        public string? Mpn { get; set; }

        /// <summary>
        /// MSRP
        /// </summary>
        public string? Msrp { get; set; }

        /// <summary>
        /// Amazon ID
        /// </summary>
        public string? ASIN { get; set; }

        public string? Category { get; set; }
        
        public List<ProductStore>? Stores { get; set; }

        public List<string>? Images { get; set; }
    }
}
