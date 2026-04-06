using System;
using MedicalServicesManagement.BLL.Interfaces;

namespace MedicalServicesManagement.BLL.Dto
{
    public class BaseDTO : IDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
