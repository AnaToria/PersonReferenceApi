using Application.Common.Wrappers.Query;
using Application.Reports.Models;

namespace Application.Reports.GetRelationships;

public class GetRelationshipsReportQuery : Query<IEnumerable<PersonReportListItemDto>>;