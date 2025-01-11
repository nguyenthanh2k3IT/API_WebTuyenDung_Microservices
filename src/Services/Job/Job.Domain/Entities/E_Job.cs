using BuildingBlock.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job.Domain.Entities;

[Table("tb_jobs")]
public class E_Job : BaseEntity<Guid>
{
}
