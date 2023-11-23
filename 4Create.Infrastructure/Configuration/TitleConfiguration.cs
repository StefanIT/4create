using _4Create.Domain.Enums;
using _4Create.Domain.Models;
using _4Create.Infrastructure.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _4Create.Infrastructure.Configuration
{
	public class TitleConfiguration : IGenerateData<Title>, IEntityTypeConfiguration<Title>
	{
		public void Configure(EntityTypeBuilder<Title> builder)
		{
			builder.HasData(Generate());
		}

		public IEnumerable<Title> Generate()
		{
			return Enum.GetValues(typeof(TitleTypeEnum))
					   .Cast<TitleTypeEnum>()
				       .Select(x => new Title
				       {
						   Id = (int)x,
						   Name = x.GetDescription()
				       })
				       .ToList();
		}
	}
}
