using MedicalServicesManagement.BLL.Interfaces;
using System;

namespace MedicalServicesManagement.BLL.Dto
{
    public class BaseDTO : IDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
