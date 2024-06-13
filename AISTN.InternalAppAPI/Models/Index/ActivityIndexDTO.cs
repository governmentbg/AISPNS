﻿namespace AISTN.InternalAppAPI.Models.Index;

public class ActivityIndexDTO
{
    public Guid? Id { get; set; }

    public Guid? ActivityKindId { get; set; }

    public string? ActivityKindName { get; set; }

    public DateTime? ActivityDate { get; set; }

    public string? SerialNumber { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public string? Notes { get; set; }

    public Guid? CaseId { get; set; }

    public Guid? SyndicId { get; set; }

    public string? SyndicName { get; set; }

    public Guid? ReportId { get; set; }

    public Guid? DocumentCollectionId { get; set; }
}