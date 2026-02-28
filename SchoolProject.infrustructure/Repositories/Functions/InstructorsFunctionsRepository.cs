using Microsoft.EntityFrameworkCore;
using SchoolProject.infrustructure.Abstracts.Functions;
using SchoolProject.infrustructure.Data;
using System.Data;

namespace SchoolProject.infrustructure.Repositories.Functions
{
    public class InstructorsFunctionsRepository : IInstructorsFunctionsRepository
    {
        private readonly AppBDContext _dbContext;
        public InstructorsFunctionsRepository(AppBDContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal GetSalaryInstructorOfSummation(string query)
        {
            using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //read From List

                //  var reader = await cmd.ExecuteReaderAsync();
                // var value = await reader.ToListAsync<GetInstructorFunctionResult>();

                decimal response = 0;
                cmd.CommandText = query;
                var value = cmd.ExecuteScalar();
                var result = value.ToString();
                if (decimal.TryParse(result, out decimal d))
                {
                    response = d;
                }
                cmd.Connection.Close();
                return response;
            }
        }

    }
}
