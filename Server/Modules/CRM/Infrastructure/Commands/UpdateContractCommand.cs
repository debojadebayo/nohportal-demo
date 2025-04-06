using AutoMapper;
using MediatR;
using Server.Modules.CRM.Infrastructure.Database;
using Shared.DTOs.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modules.CRM.Infrastructure.Commands
{
	public class UpdateContractCommand : IRequest<bool>
	{
		public int ContractId { get; set; }
		public ContractDto? UpdatedContract { get; set; }
	}

	public class UpdateContractHandler : IRequestHandler<UpdateContractCommand, bool>
	{
		private readonly CRMDbContext _dbContext;
		private readonly IMapper _mapper;

		public UpdateContractHandler(CRMDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<bool> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
		{
			var contract = await _dbContext.Contracts.FindAsync(request.ContractId);

			if (contract == null)
				return false;

			_mapper.Map(request.UpdatedContract, contract);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return true;
		}
	}

}
