using System;
using MongoDB.Bson;

namespace sciences_nation_back.Models.Dto
{
	public class ProductDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Price { get; set; }
        public required string Img { get; set; }
    }
}