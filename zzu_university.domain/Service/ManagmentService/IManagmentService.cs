using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzu_university.domain.DTOS;

namespace zzu_university.domain.Service.ManagmentService
{
    public interface IManagmentService
    {
        Task<ManagmentDto> GetManagmentAsync();
        Task UpdateManagmentAsync(ManagmentDto managmentDto);
    }
}
