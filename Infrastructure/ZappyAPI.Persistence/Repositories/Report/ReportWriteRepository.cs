using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZappyAPI.Application.Repositories;
using ZappyAPI.Domain.Entities;
using ZappyAPI.Persistence.Contexts;

namespace ZappyAPI.Persistence.Repositories
{
    public class ReportWriteRepository : WriteRepository<Report>, IReportWriteRepository
    {
        private readonly ZappyAPIDbContext _context;
        public ReportWriteRepository(ZappyAPIDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
