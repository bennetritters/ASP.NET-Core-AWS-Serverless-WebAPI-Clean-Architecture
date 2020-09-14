using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string UserId { get; set; }
        public string DisplayName { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Check if user already exists in Context
            if (_context.Users.Any(x => x.UserId == request.UserId)) return "User already Exists";
            
            //Create User
            var entity = new User
            {
                UserId = request.UserId,
                DisplayName = request.DisplayName
            };

            //Add User to Context
            _context.Users.Add(entity);

            //Saves Changes to Context
            await _context.SaveChangesAsync(cancellationToken);

            //Returns the UserId
            return entity.UserId;
        }
    }
}