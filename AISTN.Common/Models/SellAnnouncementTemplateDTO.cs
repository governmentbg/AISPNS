﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Common.Models
{
    public class SellAnnouncementTemplateDTO
    {
        public Guid? Id { get; set; }

        public int? Number { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
