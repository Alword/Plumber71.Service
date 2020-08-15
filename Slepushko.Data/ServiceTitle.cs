using System;
using System.ComponentModel.DataAnnotations;

namespace Slepushko.Data
{
    public class ServiceTitle
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)] public string Title { get; set; }
        [MaxLength(255)] public string Description { get; set; }
    }
}
