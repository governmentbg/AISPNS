﻿using AISTN.Common.Helper;

namespace AISTN.InternalAppAPI.Models.Filter
{
    public class PropertyCaseFilter
    {
        public PropertyClassKind kind { get; set; }
        public Guid CaseId { get; set; }
    }
}
