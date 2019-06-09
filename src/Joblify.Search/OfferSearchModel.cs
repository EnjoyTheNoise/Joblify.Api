using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Joblify.Search.Models
{
    [SerializePropertyNamesAsCamelCase]
    public class OfferSearchModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable, IsSearchable]
        public string Id { get; set; }

        [IsFilterable, IsSortable, IsSearchable]
        public string Title { get; set; }

        [IsFilterable, IsSearchable]
        public string Description { get; set; }

        [IsFilterable, IsSortable]
        public double Price { get; set; }

        [IsFilterable, IsSortable, IsSearchable, IsFacetable]
        public string Category { get; set; }

        [IsFilterable, IsSortable, IsSearchable, IsFacetable]
        public string Trade { get; set; }
        
        [IsSearchable]
        public string AvailableTime { get; set; }

        [IsFilterable]
        public int UserId { get; set; }

        [IsSortable, IsSearchable]
        public string FirstName { get; set; }

        [IsSortable, IsSearchable]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
