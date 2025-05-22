using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzu_university.domain.Service.StudentRegisterService
{
    public class StudentRegisterService : IStudentRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentRegisterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateNextRegistrationCodeAsync()
        {
            var allRecords = await _unitOfWork.StudentRegister.GetAllAsync();

            var lastCode = allRecords
                            .OrderByDescending(x => x.Id)
                            .Select(x => x.RegistrationCode)
                            .FirstOrDefault();

            if (string.IsNullOrEmpty(lastCode))
                return "0001";

            if (!int.TryParse(lastCode, out int lastNumber))
                throw new Exception("Last RegistrationCode is in invalid format.");

            int nextNumber = lastNumber + 1;
            return nextNumber.ToString("D4");
        }
    }

}
