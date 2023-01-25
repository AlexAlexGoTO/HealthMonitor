using AutoMapper;
using MediatR;
using MedixineMonitor.Application.Common.Dto;
using MedixineMonitor.Application.Common.Interfaces;
using MedixineMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedixineMonitor.Application.Patients.Queries;
public class GetPatientsQuery : IRequest<IList<PatientDto>>
{

}

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, IList<PatientDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPatientsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IList<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //place for pagination
            var patients = await _context.VW_Patients.ToListAsync();

            return patients;
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }
}
