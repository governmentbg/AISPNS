﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Common.Models.Save
{
    public class SavePersonDTO
    {
        public Guid? Id { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string? Egn { get; set; }
    }
}
