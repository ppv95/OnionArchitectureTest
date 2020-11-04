using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Application.Interfaces;
using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeactures.Commands
{
   public class DeleteProductByIdCommand: IRequest<int>
    {
        public int Id { get; set; }
      public  class DeleteProductByIdCommandHandle : IRequestHandler<DeleteProductByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteProductByIdCommandHandle(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _context.Products.Where(a => a.Id == command.Id)
                                                     .FirstOrDefaultAsync();
                if (product == null) return default;
                _context.Products.Remove(product);
                await _context.SaveChanges();
                return product.Id;
            }
        }
        

        }
    }
}
