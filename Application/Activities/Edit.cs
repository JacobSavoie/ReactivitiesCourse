using MediatR;
using Persistence;
using Domain;
using AutoMapper;


namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity {get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IMapper _mapper;
            private readonly DataContext _context;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                _mapper.Map(request.Activity, activity);

                await _context.SaveChangesAsync(); 
            }
        }
    }
}