using LMS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS .Core.Entities
{
    public class ResultMsg
    {
        public string? MessageType { get; set; }
        public ResultCodes? ResultCode { get; set; }
        public string? Message { get; set; }
    }

   
}